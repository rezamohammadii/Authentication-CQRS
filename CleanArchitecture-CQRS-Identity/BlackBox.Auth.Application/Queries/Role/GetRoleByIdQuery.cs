﻿using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<RoleResponseDTO>
    {
        public string RoleId { get; set; }
    }

    public class GetRoleQueryByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetRoleQueryByIdHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<RoleResponseDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _identityService.GetRoleByIdAsync(request.RoleId);
            return new RoleResponseDTO() { Id = role.id, RoleName = role.roleName };
        }
    }
}
