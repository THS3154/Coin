using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Upbit.UpbitFunctions;

namespace Upbit.Functions
{
    public class JWT
    {
        public static string GetJWT(string parameter = "")
        {
            string queryHash = "";

            //파라미터 존재할경우
            if (parameter != "")
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                foreach (string value in parameter.Split(','))
                {
                    parameters.Add(value.Split(':')[0], value.Split(':')[1]);
                }

                StringBuilder builder = new StringBuilder();
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    builder.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
                }
                string queryString = builder.ToString().TrimEnd('&');

                SHA512 sha512 = SHA512.Create();
                byte[] queryHashByteArray = sha512.ComputeHash(Encoding.UTF8.GetBytes(queryString));
                queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();
            }

            var payload = new JwtPayload
            {
                { "access_key", Access.UpbitInstance.GetAccessKey() },
                { "nonce", Guid.NewGuid().ToString() },
                { "query_hash", queryHash },
                { "query_hash_alg", "SHA512" }
            };

            byte[] keyBytes = Encoding.Default.GetBytes(Access.UpbitInstance.GetSecretKey());
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);
            var authorizationToken = "Bearer " + jwtToken;

            return authorizationToken;
        }
    }
}
