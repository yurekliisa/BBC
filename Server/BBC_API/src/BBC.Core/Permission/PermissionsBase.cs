using BBC.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Permission
{
    public abstract class PermissionBase : IPermissions
    {
        public const string Permission = "Permission";
        public const string Visitor = "Visitor";
    }

    public interface IPermissions : ISingletonDI
    {
    }
}
