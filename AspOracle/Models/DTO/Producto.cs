using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models.DTO
{
    public class Producto
    {


        public int idProducto;
        public string nombreProducto;
        public int stockProducto;
        public DTO.Proveedor proveedor;
        public int estadoProveedor;


    }
}