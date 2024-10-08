﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string username, string roleId);
        string GetUserIdFromToken(string token);

    }
}
