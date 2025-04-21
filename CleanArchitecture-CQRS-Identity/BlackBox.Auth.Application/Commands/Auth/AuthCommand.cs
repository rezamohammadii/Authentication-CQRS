﻿using BlackBox.Auth.Application.Common.Exception;
using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Commands.Auth
{
    public class AuthCommand : IRequest<AuthResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        public AuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator; 
        }
        public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");

            }
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(
                await _identityService.GetUserIdAsync(request.UserName)
                );

            string token = _tokenGenerator.GenerateJWTToken((userId, userName, roles));

            return new AuthResponseDTO()
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }
}
