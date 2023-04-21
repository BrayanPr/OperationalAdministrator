using System.Security.Claims;

namespace OperationalAdministrator.Common
{
    public class JWT
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static string verifyToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0) return null;

                return identity.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            }
            catch (Exception e)
            { 
                return null;
            }
        }
        public static string checkId(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0) return null;

                return identity.Claims.FirstOrDefault(c => c.Type == "id").Value;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string? verifyToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
