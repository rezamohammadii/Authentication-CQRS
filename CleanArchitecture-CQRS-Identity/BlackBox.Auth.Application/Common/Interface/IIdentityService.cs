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
        #endregion

        #region User's Role Section
        Task<List<string>> GetUserRolesAsync(string userId);
    
        #endregion
    }
}
