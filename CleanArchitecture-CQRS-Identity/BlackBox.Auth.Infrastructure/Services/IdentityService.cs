using BlackBox.Auth.Application.Common.Exception;
using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleManager = roleManager;
        }
        #region UserSection
        public async ValueTask<(bool isSucceed, string userId)> CreateUserAsync(string username, string password, string email, string fullName, List<string> roles)
        {
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, roles);
            if (!addUserRole.Succeeded)
            {
                throw new ValidationException(addUserRole.Errors);
            }
            return (result.Succeeded, user.Id);
        }

       

        public async ValueTask<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.Email,
                x.FullName,
            }).ToListAsync(cancellationToken);
            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }

        public async ValueTask<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");

            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }
        public async ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }
        public async ValueTask<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {
            var  user = await _userManager.FindByIdAsync(id);
            user.FullName = fullName;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        #endregion

        #region User's Role Section
        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");

            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async ValueTask<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user is null)
            {
                throw new NotFoundException("User not found");

            }
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async ValueTask<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);
            return result.Succeeded;
        }

       


        #endregion

    }
}
