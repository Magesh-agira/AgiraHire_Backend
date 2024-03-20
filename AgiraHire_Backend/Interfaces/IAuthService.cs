﻿using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;

namespace AgiraHire_Backend.Interfaces
{
    public interface IAuthService
    {
        OperationResult<string> Login(LoginRequest loginRequest);
        Role AddRole(Role role);
        OperationResult<bool> AssignRoleToUser(AddUserRole obj);
        List<Role> GetRoles(); // Method signature for getting roles
    }
}

