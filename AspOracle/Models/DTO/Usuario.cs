using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class Usuario
    {
        public int idUsuario;
        public string nombreUsuario;
        public string apelldoUsuario;
        public string userName;
        public string passUsuario;
        public string estadoPass;
        public int idPerfil;
        public DTO.Empresa empresa;
        public DTO.Perfil perfil; 

    }
}