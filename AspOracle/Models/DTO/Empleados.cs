using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class Empleados
    {
        public int empleadoId;
        public string nombreEmpleado;
        public string apellidoEmpleado;
        public int telefonoEmpleado;
        public string correoEmpleado;
        public int edadEmpleado;
        public string direccionEmpleado;
        public DTO.Usuario usuario;
        public DTO.Perfil perfil;


    }
}