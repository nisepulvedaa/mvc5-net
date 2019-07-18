using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class OrdenPedido
    {
        public int ordenPedidoID;
        public DTO.Proveedor proveedor;
        public string productos;
        public int cantidad;
        public int codigoOrdenPedido;
    }
}