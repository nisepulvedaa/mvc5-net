using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class HabitacionDetalle
    {
        public int idHabitacion;
        public string nombreHabitacion;
        public int estadoHabitacion;
        public int estadoUsado;
        public string nombreHuesped;
        public string apellidoHuesped;
        public string nombreEmpresa;
        public DateTime fechaInicio;
    }
}