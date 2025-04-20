using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Common.Interface
{
    public interface IIdentityService
    {

        #region UserSection
        ValueTask<(bool isSucceed, string userId)> CreateUserAsync(string username, string password, string email,
            string fullName, List<string> roles);
        ValueTask<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync(CancellationToken cancellationToken);
        ValueTask<bool> DeleteUserAsync(string userId);
        ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        ValueTask<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);
        #endregion

        #region User's Role Section
        Task<List<string>> GetUserRolesAsync(string userId);
        ValueTask<bool> AssignUserToRole(string userName, IList<string> roles);
        ValueTask<bool> UpdateUsersRole(string userName, IList<string> usersRole);

        #endregion
    }
}
