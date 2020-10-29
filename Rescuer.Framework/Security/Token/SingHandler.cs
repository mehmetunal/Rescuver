using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Rescuer.Framework.Security.Token
{
    public static class SingHandler
    {
        public static SecurityKey GetSecurityKey(string securityKey) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}