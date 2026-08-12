using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IJwtTokenJenerator
    {
        public string GenerateToken(Users user);
    }
}
