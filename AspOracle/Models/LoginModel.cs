using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models
{
    public class LoginModel
    {
        private readonly Models.Resources.Entities1 db = new Models.Resources.Entities1();

        public DTO.Usuario login(string user, string pass)
        {

            DTO.Usuario respuesta = new DTO.Usuario();

            try
            {
                Resources.USUARIOS usuario = db.USUARIOS.FirstOrDefault(u => u.USERNAME == user && u.PASSWORD == pass);
                return DTOBuilder.Usuario(usuario);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (login): " + ex.Message);
                respuesta = new DTO.Usuario();
            }
            return respuesta;
        }

    }
}