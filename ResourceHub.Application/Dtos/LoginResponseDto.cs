using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Dtos
{
    public class LoginResponseDto
    {
        public string userName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
