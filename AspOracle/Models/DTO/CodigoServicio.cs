using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class CodigoServicio
    {
        public int codigoServicio;
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public int diasDif;
        public int huespedId;
        public string huespedNombre;
        public int empresaId;
        public string empresaNombre;
        public int valorServicio;
        public int habitacionId;
        public string habitacionNombre;
        public DateTime fechaIngresoHabitacion;
    }
}