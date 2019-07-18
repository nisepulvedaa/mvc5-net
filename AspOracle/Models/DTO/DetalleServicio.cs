using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class DetalleServicio
    {

        public int detalleServicioId;
        public int detalleServicioValor;
        public int servicioId;
        public int huespedId;
        public int empresaId;
        public string nombreHuesped;
        public string apellidoHuesped;
        public string correoHuesped;
        public string nombreEmpresa;

    }
}