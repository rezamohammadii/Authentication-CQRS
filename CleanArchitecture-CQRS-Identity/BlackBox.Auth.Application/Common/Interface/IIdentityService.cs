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

        #endregion
    }
}
