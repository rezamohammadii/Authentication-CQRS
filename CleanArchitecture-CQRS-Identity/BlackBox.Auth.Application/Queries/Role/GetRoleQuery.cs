using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Queries.Role
{
    public class GetRoleQuery : IRequest<IList<RoleResponseDTO>>
    {

    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IList<RoleResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        public GetRoleQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<IList<RoleResponseDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles.Select(r => new RoleResponseDTO() { Id= r.id, RoleName = r.roleName,}).ToList();

        }
    }
}
