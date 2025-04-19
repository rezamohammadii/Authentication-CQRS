using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Queries.User
{
    public class GetAllUsersDetailsQuery: IRequest<List<UserDetailsResponseDTO>>
    {

    }

    public class GetAllUsersDetailsQueryHandler : IRequestHandler<GetAllUsersDetailsQuery, List<UserDetailsResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        public GetAllUsersDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<List<UserDetailsResponseDTO>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync(cancellationToken);
            var userDetails = users.Select(x => new UserDetailsResponseDTO()
            {
                Id = x.id,
                Email = x.email,
                FullName = x.fullName,
                UserName = x.userName
            }).ToList();

            foreach (var user in userDetails)
            {
                user.Roles = await _identityService.GetUserRolesAsync(user.Id);
            }
            return userDetails;
        }
    }
}
