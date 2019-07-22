using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UGOCPBackEnd2019.Services;
using UGOCPBackEnd2019.Services.Models;

namespace UGOCPBackEnd2019.Helpers
{
    public class Tokens
    {
        public static async Task<JwtModel> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new JwtModel
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Auth_Token = await jwtFactory.GenerateEncodedToken(userName, identity),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return response;
            //return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public class JwtModel
        {
            public  string Id { get; set; }
            public string Auth_Token { get; set; }
            public int ExpiresIn { get; set; }


        }
    }
}