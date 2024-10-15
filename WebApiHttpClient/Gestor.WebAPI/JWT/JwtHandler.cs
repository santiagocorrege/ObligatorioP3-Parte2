using Microsoft.IdentityModel.Tokens;
using SistemaAutenticacion.ValueObject;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gestor.WebAPI.JWT
{
    public class JwtHandler
    {
        /// <summary>
        /// Método para generar el token JWT usando una función estática (no es necesario tener instancias)
        /// </summary>

        /// <remarks> Creación del "payload" con tiene la información del usuario que se logueó (subject)
        /// El usuario tiene "claims", que son pares nombre/valor que se utilizan para guardar
        /// en el cliente. No pueden ser sensibles
        /// Se le debe setear el periodo temporal de validez (Expires)
        ///Se utiliza un algoritmo de clave simétrica para generar el token</remarks>

        public static string GenerarToken(string email, string rol)
        {
            var claveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Xyw2T+gs5Gm8Wd3U3fpGnqBkmyUbwVx8MLL0zj+zD12+4zwW32gNlElHEwqP2+KrEmWFx5H7tqvG1BdFhCqS0w==\r\n"));
            //Claim : Es una pieza de informacion sobre le token, Informacion que vamos a agregar al token y que va a viajar en el mismo (Payroll?)
            List<Claim> claims = [
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol)
                ];

            var credenciales = new SigningCredentials(claveEncriptada, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }


    }
}
