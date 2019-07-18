using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AspOracle.Models
{
    public class DTOBuilder
    {
        public static DTO.Empresa Empresa(Resources.EMPRESA recEmpresa)
        {
            DTO.Empresa empresa = new DTO.Empresa();

            empresa.idEmpresa = Convert.ToInt32(recEmpresa.ID_EMPRESA);
            empresa.razonSocial = recEmpresa.RAZON_SOCIAL;
            empresa.rutEmpresa = recEmpresa.RUT_EMPRESA;
            empresa.dvRutEmpresa = Convert.ToChar(recEmpresa.DV_RUT_EMPRESA);
            empresa.correoEmpresa = recEmpresa.CORREO_EMPRESA;
            empresa.direccionEmpresa = recEmpresa.DIRECCION_EMPRESA;
            empresa.telefonoEmpresa = Convert.ToInt32(recEmpresa.TELEFONO);
            empresa.nombreRepEmpresa = recEmpresa.NOMBRE_REPRESENTANTE;
            empresa.apellidoRepEmpresa = recEmpresa.APELLIDO_REPRESENTANTE;

            return empresa;
        }

        public static DTO.Usuario Usuario(Resources.USUARIOS recUsuario)
        {
            DTO.Usuario usuario = new DTO.Usuario();

            usuario.idUsuario = Convert.ToInt32(recUsuario.ID_USUARIO);
            usuario.nombreUsuario = recUsuario.NOMBRE_USUARIO;
            usuario.apelldoUsuario = recUsuario.APELLIDO_USUARIO;
            usuario.userName = recUsuario.USERNAME;
            usuario.passUsuario = recUsuario.PASSWORD;
            usuario.estadoPass = recUsuario.ESTADO_PASSWORD;
            usuario.idPerfil = Convert.ToInt32(recUsuario.ID_PERFIL);

            DTO.Perfil perfil = new DTO.Perfil();

            perfil.idPerfil = recUsuario.PERFILES_USUARIOS.ID_PERFIL;
            perfil.nombrePerfil = recUsuario.PERFILES_USUARIOS.NOMBRE_PERFIL;

            usuario.perfil = perfil;

            DTO.Empresa empresa = new DTO.Empresa();

            empresa.idEmpresa = recUsuario.EMPRESA.ID_EMPRESA;
            empresa.razonSocial = recUsuario.EMPRESA.RAZON_SOCIAL;
            empresa.rutEmpresa = recUsuario.EMPRESA.RUT_EMPRESA;
            empresa.dvRutEmpresa = Convert.ToChar(recUsuario.EMPRESA.DV_RUT_EMPRESA);
            empresa.correoEmpresa = recUsuario.EMPRESA.CORREO_EMPRESA;
            empresa.direccionEmpresa = recUsuario.EMPRESA.DIRECCION_EMPRESA;
            empresa.telefonoEmpresa = Convert.ToInt32(recUsuario.EMPRESA.TELEFONO);
            empresa.nombreRepEmpresa = recUsuario.EMPRESA.NOMBRE_REPRESENTANTE;
            empresa.apellidoRepEmpresa = recUsuario.EMPRESA.APELLIDO_REPRESENTANTE;

            usuario.empresa = empresa;

            return usuario;
        }

        //HUESPED
        public static DTO.Huesped Huesped(Resources.HUESPED recHuesped)
        {
            DTO.Huesped huesped = new DTO.Huesped();

            huesped.idHuesped = Convert.ToInt32(recHuesped.ID_HUESPED);
            huesped.nombreHuesped = recHuesped.NOMBRE_HUESPED;
            huesped.apellidoHuesped = recHuesped.APELLIDO_HUESPED;
            huesped.rutHuesped =Convert.ToInt32(recHuesped.RUT_HUESPED);
            huesped.dvRutHuesped = Convert.ToChar(recHuesped.DV_RUT_HUESPED);           
            huesped.telefonoHuesped = Convert.ToInt32(recHuesped.TELEFONO_HUESPED);
            huesped.correoHuesped = recHuesped.CORREO_HUESPED;

            DTO.Empresa empresa = new DTO.Empresa();

            empresa.idEmpresa = recHuesped.EMPRESA.ID_EMPRESA;
            empresa.razonSocial = recHuesped.EMPRESA.RAZON_SOCIAL;
            empresa.rutEmpresa = recHuesped.EMPRESA.RUT_EMPRESA;
            empresa.dvRutEmpresa = Convert.ToChar(recHuesped.EMPRESA.DV_RUT_EMPRESA);
            empresa.correoEmpresa = recHuesped.EMPRESA.CORREO_EMPRESA;
            empresa.direccionEmpresa = recHuesped.EMPRESA.DIRECCION_EMPRESA;
            empresa.telefonoEmpresa = Convert.ToInt32(recHuesped.EMPRESA.TELEFONO);
            empresa.nombreRepEmpresa = recHuesped.EMPRESA.NOMBRE_REPRESENTANTE;
            empresa.apellidoRepEmpresa = recHuesped.EMPRESA.APELLIDO_REPRESENTANTE;

            huesped.empresa = empresa;

            return huesped;
        }
        //EMPLEADO
        public static DTO.Empleados Empleado(Resources.EMPLEADOS recEmpleado)
        {
            DTO.Empleados empleado = new DTO.Empleados();

            empleado.empleadoId = Convert.ToInt32(recEmpleado.ID_EMPLEADO);
            empleado.nombreEmpleado = recEmpleado.NOMBRE_EMPLEADO;
            empleado.apellidoEmpleado = recEmpleado.APELLIDO_EMPLEADO;
            empleado.telefonoEmpleado = Convert.ToInt32(recEmpleado.TELEFONO_EMPLEADO);
            empleado.correoEmpleado = recEmpleado.CORREO_EMPLEADO;
            empleado.edadEmpleado = Convert.ToInt32(recEmpleado.EDAD_EMPLEADO);
            empleado.direccionEmpleado = recEmpleado.DIRECCION_EMPLEADO;

            DTO.Usuario usuario = new DTO.Usuario();
            

            usuario.idUsuario = Convert.ToInt32(recEmpleado.ID_USUARIO);

            empleado.usuario = usuario;

            DTO.Perfil perfil = new DTO.Perfil();

            perfil.idPerfil = Convert.ToInt32(recEmpleado.PERFILES_USUARIOS_ID_PERFIL);

            empleado.perfil = perfil;


            return empleado;
        }

        //PROVEEDOR
        public static DTO.Proveedor Proveedor(Resources.PROVEEDOR recProveedor)
        {
            DTO.Proveedor proveedor = new DTO.Proveedor();

            proveedor.proveedorId = Convert.ToInt32(recProveedor.ID_PROVEEDOR);
            proveedor.nombreProveedor = recProveedor.NOMBRE_PROVEEDOR;
            proveedor.contactoProveedor = Convert.ToInt32(recProveedor.CONTACTO_PROVEEDOR) ;
            proveedor.rubroProveedor = recProveedor.RUBRO_PROVEEDOR;
            proveedor.direccionProveedor = recProveedor.DIRECCION_PROVEEDOR;
            proveedor.correoProveedor =recProveedor.CORREO_PROVEEDOR;
            
            DTO.Perfil perfil = new DTO.Perfil();

            perfil.idPerfil = Convert.ToInt32(recProveedor.PERFILES_USUARIOS_ID_PERFIL);

            proveedor.perfil = perfil;


            return proveedor;
        }

        //HABITACIONES

        public static DTO.Habitacion Habitacion(Resources.HABITACION recHabitacion)
        {
            DTO.Habitacion habitacion = new DTO.Habitacion();

            habitacion.idHabitacion = Convert.ToInt32(recHabitacion.ID_HABITACION);
            habitacion.nombreHabitacion = recHabitacion.NOMBRE_HABITACION;
            habitacion.tipoCamaHabitacion = recHabitacion.TIPO_CAMA_HABITACION;
            habitacion.accesoriosHabitacion = recHabitacion.ACCESORIOS_HABITACION;
            habitacion.valorHabitacion = Convert.ToInt32(recHabitacion.VALOR_HABITACION);
            habitacion.estadoHabitacion = Convert.ToInt32(recHabitacion.E_ID_ESTADO_HABITACION);
            return habitacion;
        }

       // COMEDORES
        public static DTO.Comedor Comedor(Resources.COMEDORES recComedor)
        {
            DTO.Comedor comedor = new DTO.Comedor();
            comedor.idComedor = Convert.ToInt32(recComedor.ID_COMEDOR);
            comedor.nombreComedor = recComedor.NOMBRE_COMEDOR;
            comedor.capacidadComedor = Convert.ToInt32(recComedor.CAPACIDAD_COMEDOR);
            comedor.estadoComedor = Convert.ToInt32(recComedor.ESTADO_COMEDOR);
         
            return comedor;
        }

        //MENU

        public static DTO.Menu Menu(Resources.MENU recMenu)
        {
            DTO.Menu menu = new DTO.Menu();
            menu.idMenu = Convert.ToInt32(recMenu.ID_MENU);
            menu.nombreMenu = recMenu.NOMBRE_MENU;
            menu.tipoMenu = Convert.ToInt32(recMenu.TIPO_MENU);
            menu.estiloMenu = Convert.ToInt32(recMenu.ESTILO_MENU);
            menu.descripcionMenu = recMenu.DESCRIPCION_MENU;
            menu.valorMenu = Convert.ToInt32(recMenu.VALOR_MENU);
            menu.cantidadMenu = Convert.ToInt32(recMenu.CANTIDAD_MENU);
            menu.estadoMenu = Convert.ToInt32(recMenu.ESTADO_MENU);

            return menu;
        }

        //Producto

        public static DTO.Producto Producto(Resources.PRODUCTO recProducto)
        {
            DTO.Producto producto = new DTO.Producto();
            producto.idProducto = Convert.ToInt32(recProducto.ID_PRODUCTO);
            producto.nombreProducto = recProducto.NOMBRE_PRODUCTO;
            producto.stockProducto = Convert.ToInt32(recProducto.STOCK_PRODUCTO);

            DTO.Proveedor proveedor = new DTO.Proveedor();
            proveedor.proveedorId = Convert.ToInt32(recProducto.PROVEEDOR.ID_PROVEEDOR);
            proveedor.nombreProveedor = recProducto.PROVEEDOR.NOMBRE_PROVEEDOR;

            producto.proveedor = proveedor;

            return producto;
        }

        //ORDEN DE PEDIDO
        public static DTO.OrdenPedido OrdenPedido(Resources.ORDEN_PEDIDO recOrdenPedido)
        {
            DTO.OrdenPedido ordenPedido = new DTO.OrdenPedido();
            ordenPedido.ordenPedidoID = Convert.ToInt32(recOrdenPedido.ID_ORDEN_PEDIDO);

            DTO.Proveedor proveedor = new DTO.Proveedor();
            proveedor.proveedorId = Convert.ToInt32(recOrdenPedido.PROVEEDOR.ID_PROVEEDOR);
            proveedor.nombreProveedor = recOrdenPedido.PROVEEDOR.NOMBRE_PROVEEDOR;
            ordenPedido.proveedor = proveedor;
            ordenPedido.productos = recOrdenPedido.PRODUCTOS_CODIGOS;
            ordenPedido.cantidad = Convert.ToInt32(recOrdenPedido.CANTIDAD_PRODUCTO);
            ordenPedido.codigoOrdenPedido = Convert.ToInt32(recOrdenPedido.CODIGO_ORDEN_PEDIDO);

            return ordenPedido;
        }


    }
}