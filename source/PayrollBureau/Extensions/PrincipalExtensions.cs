using System.Linq;
using System.Security.Principal;
using PayrollBureau.Common.Enum.User;

namespace PayrollBureau.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool IsInAllRoles(this IPrincipal principal, params string[] roles)
        {
            return roles.All(r => principal.IsInRole(r));
        }

        public static bool IsInAnyRoles(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(r => principal.IsInRole(r));
        }

        public static bool IsSuperUser(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.SuperUser));
        }
        public static bool IsBureau(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Bureau));
        }

        public static bool IsEmployer(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Employer));
        }

        public static bool IsEmployee(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Employee));
        }
    }
}