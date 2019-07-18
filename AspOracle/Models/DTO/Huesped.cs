using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class Huesped

    {
        public int idHuesped;
        public string nombreHuesped;
        public string apellidoHuesped;
        public int rutHuesped;
        public char dvRutHuesped;
        public int telefonoHuesped;
        public string correoHuesped;
        public DTO.Empresa empresa;

    }
}
