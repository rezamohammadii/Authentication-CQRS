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
        #endregion

    }
}
