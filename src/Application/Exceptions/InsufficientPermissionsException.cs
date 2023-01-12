using System;

namespace Tivix.BudgetPlanner.Application.Exceptions;

public class InsufficientPermissionsException : Exception
{
    public string PermissionName { get; }

    public InsufficientPermissionsException(string permissionName)
        : base($"User has insufficient permissions to perform this operation ({permissionName})")
    {
        PermissionName = permissionName;
    }
}