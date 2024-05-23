using ChildHDT.Domain.ValueObjects;
using System;
using System.Linq;
using System.Reflection;

public static class RoleConverterHelper
{
    private static readonly string Namespace = "ChildHDT.Domain.ValueObjects";

    public static string SerializeRole(Role role)
    {
        return role.GetType().Name;
    }

    public static Role DeserializeRole(string roleName)
    {
        Type roleType = Type.GetType($"{Namespace}.{roleName}");
        if (roleType == null)
        {
            throw new InvalidOperationException($"Unknown role type: {roleName}");
        }

        return (Role)Activator.CreateInstance(roleType);
    }
}
