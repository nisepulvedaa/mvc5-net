using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class ComedorDetalle
    {
        public int idComedor;
        public string nombreComedor;
        public int estadoComedor;
        public int capacidadComedor;
        public int estadoUsado;
        public string nombreHuesped;
        public string apellidoHuesped;
        public string nombreEmpresa;
        public DateTime fechaInicio;
    }
}