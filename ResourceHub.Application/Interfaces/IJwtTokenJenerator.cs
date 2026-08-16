using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
         string GenerateToken(Users user);
    }
}
