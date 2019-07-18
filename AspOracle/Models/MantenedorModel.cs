using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle.Models
{
    public class MantenedorModel
    {
        private readonly Models.Resources.Entities1 db = new Models.Resources.Entities1();

        //EMPRESAS

        public bool ExisteEmpresa(string razon_social, string rut_empresa = "")
        {
            try
            {
                if (razon_social != "")
                {
                    var empresa = db.EMPRESA.Where(e => e.RAZON_SOCIAL == razon_social);
                    if (empresa.Count() > 0)
                    {
                        return true;
                    }
                }
                if (rut_empresa != "")
                {
                    var empresa = db.EMPRESA.Where(e => e.RUT_EMPRESA == rut_empresa);
                    if (empresa.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteEmpresa): " + ex.Message);
                return false;
            }
        }

        public bool IngresarEmpresas(string razon_social, string rut_empresa, char dv_rut_empresa, string correo, string direccion, int telefono, string nombreRep, string apellidoRep)
        {
            try
            {

                
                db.INSERTAR_EMPRESA(razon_social, rut_empresa, dv_rut_empresa.ToString(), correo, direccion, telefono, nombreRep, apellidoRep);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarEmpresas): " + ex.Message);
                return false;
            }
        }

        public List<DTO.Empresa> obtenerEmpresas()
        {
            List<DTO.Empresa> lista = new List<DTO.Empresa>();
            try
            {

                var empresas = db.EMPRESA.ToList();
                foreach (var empresa in empresas)
                {

                    lista.Add(DTOBuilder.Empresa(empresa));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresas): " + ex.Message);
                lista = new List<DTO.Empresa>();
            }
            return lista;
        }
        
        public DTO.Empresa obtenerEmpresaPorRut(string rut_empresa)
        {

            DTO.Empresa respuesta = new DTO.Empresa();

            try
            {
                Resources.EMPRESA empresa = db.EMPRESA.FirstOrDefault(e => e.RUT_EMPRESA == rut_empresa);
                return DTOBuilder.Empresa(empresa);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresaPorRut): " + ex.Message);
                respuesta = new DTO.Empresa();
            }
            return respuesta;
        }

        public DTO.Empresa obtenerEmpresaPorId(int empresaId)
        {

            DTO.Empresa respuesta = new DTO.Empresa();

            try
            {
                Resources.EMPRESA empresa = db.EMPRESA.FirstOrDefault(e => e.ID_EMPRESA == empresaId);
                return DTOBuilder.Empresa(empresa);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresaPorId): " + ex.Message);
                respuesta = new DTO.Empresa();
            }
            return respuesta;
        }
        
        public List<DTO.Huesped> obtenerHuespedPorEmpresaId(int empresaId)
        {

            List<DTO.Huesped> lista = new List<DTO.Huesped>();
            try
            {

                var huesped = db.HUESPED.Where(e => e.EMPRESA_ID_EMPRESA == empresaId && (e.ID_ESTADO_HUESPED == 1 || e.ID_ESTADO_HUESPED == 2));   // db.HUESPED.ToList();
                foreach (var Huesped in huesped)
                {

                    lista.Add(DTOBuilder.Huesped(Huesped));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuespedPorEmpresaId): " + ex.Message);
                lista = new List<DTO.Huesped>();
            }
            return lista;
        }

        public List<DTO.Huesped> obtenerHuespedPorId(int huespedId)
        {

            List<DTO.Huesped> lista = new List<DTO.Huesped>();
            try
            {

                var huesped = db.HUESPED.Where(h => h.ID_HUESPED == huespedId);   // db.HUESPED.ToList();
                foreach (var Huesped in huesped)
                {

                    lista.Add(DTOBuilder.Huesped(Huesped));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuespedPorId): " + ex.Message);
                lista = new List<DTO.Huesped>();
            }
            return lista;
        }


        public bool ExisteCorreoUsuario(string correo)
        {

            try
            {

                if (correo != "")
                {
                    var usuario = db.EMPRESA.Where(e => e.CORREO_EMPRESA == correo);
                    if (usuario.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteCorreoUsuario): " + ex.Message);
                return false;
            }
        }

        public bool EditarEmpresas(int empresaId, string razon_social, string rut_empresa, char dv_rut_empresa, string correo, string direccion, int telefono, string nombreRep, string apellidoRep)
        {
            try
            {
                // db.EditarEmpresa(empresaId, rut, razonSocial, giro, logo, usuarioId);
                db.ACTUALIZAR_EMPRESA(empresaId, razon_social, rut_empresa, dv_rut_empresa.ToString(), correo, direccion, telefono, nombreRep, apellidoRep);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarEmpresas): " + ex.Message);
                return false;
            }
        }

        public bool EliminarEmpresas(int empresaId)
        {
            try
            {
                db.ELIMINAR_EMPRESA(empresaId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarEmpresas): " + ex.Message);
                return false;
            }
        }

        public bool EmpresaEsEliminable(int empresaId)
        {

            try
            {

                Resources.EMPRESA empresa = db.EMPRESA.Where(e => e.ID_EMPRESA == empresaId).FirstOrDefault();
                if (empresa.USUARIOS.Count() > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EmpresaEsEliminable): " + ex.Message);
                return false;
            }
        }

        public DTO.Empleados obtenerEmpleadoPorUsuarioId(int usuarioId)
        {

            DTO.Empleados respuesta = new DTO.Empleados();

            try
            {
                Resources.EMPLEADOS empleado = db.EMPLEADOS.FirstOrDefault(e => e.ID_USUARIO == usuarioId);
                return DTOBuilder.Empleado(empleado);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpleadoPorUsuarioId): " + ex.Message);
                respuesta = new DTO.Empleados();
            }
            return respuesta;
        }


        //USUARIO

        public bool IngresarUsuario(string nombre_usuario, string apellido_usuario, string username, string pass, int id_empresa)
        {
            try
            {

                db.INSERTAR_USUARIO(nombre_usuario, apellido_usuario, username, pass, 1, 3, id_empresa, 3);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarUsuario): " + ex.Message);
                return false;
            }
        }

        public bool IngresarUsuarioEmpleado(string nombre_usuario, string apellido_usuario, string username, string pass, int id_empresa)
        {
            try
            {

                db.INSERTAR_USUARIO(nombre_usuario, apellido_usuario, username, pass, 1, 2, id_empresa, 2);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarUsuario): " + ex.Message);
                return false;
            }
        }


        //HUESPED
        public bool IngresarHuesped( string nombre_huesped, string apellido_huesped, int rut_huesped, char dv_rut_huesped, int telefono_huesped, string correo_huesped,int empresa_id_empresa)
        {
            try
            {

                db.INSERTAR_HUESPED( nombre_huesped, apellido_huesped, rut_huesped, dv_rut_huesped.ToString(), telefono_huesped, correo_huesped, empresa_id_empresa);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarHuesped): " + ex.Message);
                return false;
            }
        }

        public bool ExisteHuesped(int rut_huesped)
        {
            try
            {
                
                if (rut_huesped != 0)
                {
                    var huesped = db.HUESPED.Where(e => e.RUT_HUESPED == rut_huesped);
                    if (huesped.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteHuesped): " + ex.Message);
                return false;
            }
        }

        public List<DTO.Huesped> obtenerHuesped()
        {
            List<DTO.Huesped> lista = new List<DTO.Huesped>();
            try
            {

                var huesped = db.HUESPED.ToList();
                foreach (var Huesped in huesped)
                {

                    lista.Add(DTOBuilder.Huesped(Huesped));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuesped): " + ex.Message);
                lista = new List<DTO.Huesped>();
            }
            return lista;
        }

        public bool EliminarHuesped(int huespedId)
        {
            try
            {
                db.ELIMINAR_HUESPED(huespedId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarHuesped): " + ex.Message);
                return false;
            }
        }

        public DTO.Huesped obtenerHuespedPorRut(int rut_huesped)
        {

            DTO.Huesped respuesta = new DTO.Huesped();

            try
            {
                Resources.HUESPED huesped = db.HUESPED.FirstOrDefault(h => h.RUT_HUESPED == rut_huesped);
                return DTOBuilder.Huesped(huesped);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuespedPorRut): " + ex.Message);
                respuesta = new DTO.Huesped();
            }
            return respuesta;
        }

        public bool EditarHuesped(int huespedId ,  string huespedNombre,string  huespedApellido, int huespedRut , char huespedDvRut, int huespedTelefono, string huespedCorreo, int huespedEmpresaId)
        {
            try
            {

                db.ACTUALIZAR_HUESPED(huespedId, huespedNombre, huespedApellido, huespedRut, huespedDvRut.ToString(), huespedTelefono, huespedCorreo, huespedEmpresaId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarHuesped): " + ex.Message);
                return false;
            }
        }


        //EMPLEADO
        public bool IngresarEmpleado(string nombre_empleado, string apellido_empleado, int telefono_empelado, string correo_empleado, int edad_empleado, string direccion_empleado, int id_usuario, int id_perfil)
        {
            try
            {
                                
                db.INSERTAR_EMPLEADOS(nombre_empleado, apellido_empleado, telefono_empelado, correo_empleado, edad_empleado, direccion_empleado, id_usuario, id_perfil);
                return true;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarEmpleado): " + ex.Message);
                return false;
            }
        }

        public bool ExisteEmpleado(string correo)
        {
            try
            {
                if (correo != "")
                {
                    var empleado = db.EMPLEADOS.Where(e => e.CORREO_EMPLEADO == correo);
                    if (empleado.Count() > 0)
                    {
                        return true;
                    }
                }
              
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteEmpleado): " + ex.Message);
                return false;
            }
        }

        public DTO.Empleados obtenerEmpleadoPorCorreo(string correo)
        {

            DTO.Empleados respuesta = new DTO.Empleados();

            try
            {
                Resources.EMPLEADOS empleado = db.EMPLEADOS.FirstOrDefault(e => e.CORREO_EMPLEADO == correo);
                return DTOBuilder.Empleado(empleado);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpleadoPorCorreo): " + ex.Message);
                respuesta = new DTO.Empleados();
            }
            return respuesta;
        }

        public DTO.Usuario obtenerUsuarioPorCorreo(string correo)
        {

            DTO.Usuario respuesta = new DTO.Usuario();

            try
            {
                Resources.USUARIOS usuario = db.USUARIOS.FirstOrDefault(u => u.USERNAME == correo);
                return DTOBuilder.Usuario(usuario);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerUsuarioPorCorreo): " + ex.Message);
                respuesta = new DTO.Usuario();
            }
            return respuesta;
        }

        public List<DTO.Empleados> obtenerEmpleado()
        {
            List<DTO.Empleados> lista = new List<DTO.Empleados>();
            try
            {

                var empleado = db.EMPLEADOS.ToList();
                foreach (var Empleado in empleado)
                {

                    lista.Add(DTOBuilder.Empleado(Empleado));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpleado): " + ex.Message);
                lista = new List<DTO.Empleados>();
            }
            return lista;
        }

        public bool EditarEmpleado(int empleadoId, string empleadoNombre, string empleadoApellido, int empleadoTelefono, string empleadoCorreo, int empleadoEdad, string empleadoDireccion)
        {
            try
            {

                db.ACTUALIZAR_EMPLEADO(empleadoId, empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarEmpleado): " + ex.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(int empleadoId)
        {
            try
            {
                db.ELIMINAR_EMPLEADO(empleadoId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarEmpleado): " + ex.Message);
                return false;
            }
        }

        //PROVEEDOR
        public bool IngresarProveedor(string nombre_proveedor, int contacto_proveedor, string rubro_proveedor, string direccion_proveedor, string correo_proveedor, int id_perfil)
        {
            try
            {
                                
                db.INSERTAR_PROVEEDOR(nombre_proveedor, contacto_proveedor, rubro_proveedor, direccion_proveedor, correo_proveedor, id_perfil);
                return true;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarProveedor): " + ex.Message);
                return false;
            }
        }

        public bool ExisteProveedor(string correo)
        {
            try
            {
                if (correo != "")
                {
                    var proveedor = db.PROVEEDOR.Where(e => e.CORREO_PROVEEDOR == correo);
                    if (proveedor.Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteProveedor): " + ex.Message);
                return false;
            }
        }

        public DTO.Proveedor obtenerProveedorPorCorreo(string correo)
        {

            DTO.Proveedor respuesta = new DTO.Proveedor();

            try
            {
                Resources.PROVEEDOR proveedor = db.PROVEEDOR.FirstOrDefault(e => e.CORREO_PROVEEDOR == correo);
                return DTOBuilder.Proveedor(proveedor);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerProveedorPorCorreo): " + ex.Message);
                respuesta = new DTO.Proveedor();
            }
            return respuesta;
        }

        public DTO.Proveedor obtenerProveedorPorId(int proveedorId)
        {

            DTO.Proveedor respuesta = new DTO.Proveedor();

            try
            {
                Resources.PROVEEDOR proveedor = db.PROVEEDOR.FirstOrDefault(e => e.ID_PROVEEDOR == proveedorId);
                return DTOBuilder.Proveedor(proveedor);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerProveedorPorId): " + ex.Message);
                respuesta = new DTO.Proveedor();
            }
            return respuesta;
        }


        public List<DTO.Proveedor> obtenerProveedor()
        {
            List<DTO.Proveedor> lista = new List<DTO.Proveedor>();
            try
            {

                var proveedor = db.PROVEEDOR.ToList();
                foreach (var Proveedor in proveedor)
                {

                    lista.Add(DTOBuilder.Proveedor(Proveedor));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerProveedor): " + ex.Message);
                lista = new List<DTO.Proveedor>();
            }
            return lista;
        }

        public bool EditarProveedor(int proveedorId, string proveedorNombre, int proveedorContacto, string proveedorRubro, string proveedorDireccion, string proveedorCorreo)
        {
            try
            {

                db.ACTUALIZAR_PROVEEDOR(proveedorId, proveedorNombre, proveedorContacto, proveedorRubro, proveedorDireccion, proveedorCorreo);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarProveedor): " + ex.Message);
                return false;
            }
        }

        public bool EliminarProveedor(int proveedorId)
        {
            try
            {
                db.ELIMINAR_PROVEEDOR(proveedorId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarProveedor): " + ex.Message);
                return false;
            }
        }

        //HABITACIONES
        public List<DTO.Habitacion> obtenerHabitacionesActivas()
        {
            List<DTO.Habitacion> lista = new List<DTO.Habitacion>();
            try
            {

                var habitaciones = db.HABITACION.Where(e => e.E_ID_ESTADO_HABITACION == 1).ToList().OrderBy(h => h.ID_HABITACION);
                foreach (var habitacion in habitaciones)
                {

                    lista.Add(DTOBuilder.Habitacion(habitacion));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresas): " + ex.Message);
                lista = new List<DTO.Habitacion>();
            }
            return lista;
        }


        public List<DTO.Habitacion> obtenerHabitaciones()
        {
            List<DTO.Habitacion> lista = new List<DTO.Habitacion>();
            try
            {

                var habitaciones = db.HABITACION.ToList().OrderBy(h => h.ID_HABITACION);
                foreach (var habitacion in habitaciones)
                {

                    lista.Add(DTOBuilder.Habitacion(habitacion));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresas): " + ex.Message);
                lista = new List<DTO.Habitacion>();
            }
            return lista;
        }

        public List<DTO.HabitacionDetalle> obtenerHabitacionesConFecha()
        {
            List<DTO.HabitacionDetalle> lista = new List<DTO.HabitacionDetalle>();
            try
            {

                var detalleHabitacion = (from ha in db.HABITACION
                                         join dh in db.DETALLE_HABITACIONES
                                           on ha.ID_HABITACION equals dh.HABITACION_ID_HABITACION into resultado
                                         from pat in resultado.DefaultIfEmpty()
                                         join hu in db.HUESPED
                                         on pat.HUESPED_ID_HUESPED equals hu.ID_HUESPED into resultado2
                                         from pat2 in resultado2.DefaultIfEmpty()
                                         join em in db.EMPRESA
                                         on pat2.EMPRESA_ID_EMPRESA equals em.ID_EMPRESA into resultado3
                                         from pat3 in resultado3.DefaultIfEmpty()
                                         select new
                                         {

                                             habitacionId = ha.ID_HABITACION,
                                             habitacionNombre = ha.NOMBRE_HABITACION,
                                             habitacionEstado = ha.ESTADO_HABITACION.ID_ESTADO_HABITACION,
                                             estadoUsado = pat.ID_ESTADO_HABITACION,
                                             nombreHuesped = pat2 == null ? "" :   pat2.NOMBRE_HUESPED,
                                             apellidoHuesped = pat2 == null ? "" : pat2.APELLIDO_HUESPED,
                                             nombreEmpresa = pat3 == null ? "" : pat3.RAZON_SOCIAL,
                                             fechaInicio = pat == null ? DateTime.MinValue : pat.FECHA_INGRESO


             }).ToList();
                foreach (var row in detalleHabitacion)
                {
                    DTO.HabitacionDetalle habitacionDetalle = new DTO.HabitacionDetalle();
                    habitacionDetalle.idHabitacion = row.habitacionId;
                    habitacionDetalle.nombreHabitacion = row.habitacionNombre;
                    habitacionDetalle.estadoUsado = Convert.ToInt32(row.estadoUsado);
                    habitacionDetalle.estadoHabitacion = Convert.ToInt32(row.habitacionEstado);
                    habitacionDetalle.nombreHuesped = row.nombreHuesped;
                    habitacionDetalle.apellidoHuesped = row.apellidoHuesped;
                    habitacionDetalle.nombreEmpresa = row.nombreEmpresa;
                    habitacionDetalle.fechaInicio = row.fechaInicio;
                    lista.Add(habitacionDetalle);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerEmpresas): " + ex.Message);
                lista = new List<DTO.HabitacionDetalle>();
            }
            return lista;
        }


        public bool ExisteHabitacion(string nombreHabitacion)
        {
            try
            {
                if (nombreHabitacion != "")
                {
                    var habitacion = db.HABITACION.Where(h => h.NOMBRE_HABITACION == nombreHabitacion);
                    if (habitacion.Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteHabitacion): " + ex.Message);
                return false;
            }
        }


        public bool IngresarHabitacion(string nombreHabitacion, string tipoCamaHabitacion, string accesorioHabitacion, int valorHabitacion, int id_estado_habitacion)
        {
            try
            {
                db.INSERTAR_HABITACION(nombreHabitacion, tipoCamaHabitacion, accesorioHabitacion, valorHabitacion, id_estado_habitacion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarHabitacion): " + ex.Message);
                return false;
            }
        }


        public DTO.Habitacion obtenerHabitacionPorId(int idHabitacion)
        {

            DTO.Habitacion respuesta = new DTO.Habitacion();

            try
            {
                Resources.HABITACION habitacion = db.HABITACION.FirstOrDefault(h => h.ID_HABITACION == idHabitacion);
                return DTOBuilder.Habitacion(habitacion);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHabitacionPorId): " + ex.Message);
                respuesta = new DTO.Habitacion();
            }
            return respuesta;
        }


        public bool EditarHabitacion(int habitacionId, string nombreHabitacion, string tipoCamaHabitacion, string accesorioHabitacion, int valorHabitacion)
        {
            try
            {
                db.ACTUALIZAR_HABITACION(habitacionId, nombreHabitacion, tipoCamaHabitacion, accesorioHabitacion, valorHabitacion, 1);
                // db.ACTUALIZAR_EMPLEADO(empleadoId, empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarHabitacion): " + ex.Message);
                return false;
            }
        }


        public bool EliminarHabitacion(int habitacionId)
        {
            try
            {
                db.ELIMINAR_HABITACION(habitacionId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarHabitacion): " + ex.Message);
                return false;
            }
        }


        //DETALLE SERVICIO


        public bool IngresarDetalleDeServicio(int servicioId, int huespedId, int valorServicio, int codigoServicio, string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fecha_i = Convert.ToDateTime(fechaInicio);
                DateTime fecha_f = Convert.ToDateTime(fechaFin);

                db.INSERTAR_DETALLE_SERVICIO(servicioId, huespedId, valorServicio, codigoServicio,fecha_i, fecha_f);
                //db.INSERTAR_HABITACION(nombreHabitacion, tipoCamaHabitacion, accesorioHabitacion, valorHabitacion, id_estado_habitacion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarDetalleDeServicio): " + ex.Message);
                return false;
            }
        }

        //DETALLE DE HABITACION


        public bool IngresarDetalleHabitacion(int empresaId, int huespedId, int habitacionId, int habitacionValor, int estadoHabitacionId, int empleadoId)
        {
            try
            {

                db.INSERTAR_DETALLE_HABITACION(estadoHabitacionId, habitacionValor, huespedId, habitacionId, empleadoId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarDetalleHabitacion): " + ex.Message);
                return false;
            }
        }

        public DTO.DetalleServicio obtenerDetalleServicioPorCodigo(int codigoReserva)
        {


            //List<DTO.DetalleServicio> lista = new List<DTO.DetalleServicio>();
            try
            {
                DTO.DetalleServicio dtsr = new DTO.DetalleServicio();
                var lista = (from dt in db.DETALLE_SERVICIO
                             join h in db.HUESPED
                             on dt.ID_HUESPED equals h.ID_HUESPED
                             join e in db.EMPRESA
                             on h.EMPRESA_ID_EMPRESA equals e.ID_EMPRESA
                             where dt.CODIGO_SERVICIO == codigoReserva
                             select new
                             {
                                 detalleServicioId = dt.ID_DETALLE_SERVICIO,
                                 detalleServicioValor = dt.VALOR_SERVICIO,
                                 huespedId = h.ID_HUESPED,
                                 nombreHuesped = h.NOMBRE_HUESPED,
                                 apellidoHuesped = h.APELLIDO_HUESPED,
                                 correoHuesped = h.CORREO_HUESPED,
                                 empresaId = e.ID_EMPRESA,
                                 nombreEmpresa = e.RAZON_SOCIAL,
                                 servicioId = dt.ID_SERVICO
                                   }).FirstOrDefault() ;

                dtsr.detalleServicioId = lista.detalleServicioId;
                dtsr.detalleServicioValor = lista.detalleServicioValor;
                dtsr.servicioId = lista.servicioId;
                dtsr.empresaId = lista.empresaId;
                dtsr.huespedId = lista.huespedId;
                dtsr.nombreHuesped = lista.nombreHuesped;
                dtsr.apellidoHuesped = lista.apellidoHuesped;
                dtsr.correoHuesped = lista.correoHuesped;
                dtsr.nombreEmpresa = lista.nombreEmpresa;
                

                return dtsr;


           
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuespedPorEmpresaId): " + ex.Message);
                return new DTO.DetalleServicio();
            }
           
        }


        //COMEDORES
        public List<DTO.Comedor> obtenerComedor()
        {
            List<DTO.Comedor> lista = new List<DTO.Comedor>();
            try
            {

                var comedores = db.COMEDORES.ToList().OrderBy(h => h.ID_COMEDOR);
                foreach (var comedor in comedores)
                {

                    lista.Add(DTOBuilder.Comedor(comedor));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerComedor): " + ex.Message);
                lista = new List<DTO.Comedor>();
            }
            return lista;
        }

        public bool ExisteComedor(string nombreComedor)
        {
            try
            {
                if (nombreComedor != "")
                {
                    var comedor = db.COMEDORES.Where(h => h.NOMBRE_COMEDOR == nombreComedor);
                    if (comedor.Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteComedor): " + ex.Message);
                return false;
            }
        }

        public bool IngresarComedor(string nombreComedor, int capacidadComedor,int estadoComedor)
        {
            try
            {
                db.INSERTAR_COMEDOR(nombreComedor, capacidadComedor,estadoComedor);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarComedor): " + ex.Message);
                return false;
            }
        }

        public DTO.Comedor obtenerComedorPorId(int idComedor)
        {

            DTO.Comedor respuesta = new DTO.Comedor();

            try
            {
                Resources.COMEDORES comedor = db.COMEDORES.FirstOrDefault(h => h.ID_COMEDOR == idComedor);
                return DTOBuilder.Comedor(comedor);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerComedorPorId): " + ex.Message);
                respuesta = new DTO.Comedor();
            }
            return respuesta;
        }

        public bool EditarComedor(int comedorId, string nombreComedor, int capacidadComedor)
        {
            try
            {
                db.ACTUALIZAR_COMEDOR(comedorId, nombreComedor, capacidadComedor, 1);
                // db.ACTUALIZAR_EMPLEADO(empleadoId, empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarComedor): " + ex.Message);
                return false;
            }
        }

        public bool EliminarComedor(int comedorId)
        {
            try
            {
                db.ELIMINAR_COMEDOR(comedorId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarComedor): " + ex.Message);
                return false;
            }
        }

        //MENU
        public List<DTO.Menu> obtenerMenu()
        {
            List<DTO.Menu> lista = new List<DTO.Menu>();
            try
            {

                var menu = db.MENU.ToList().OrderBy(h => h.ID_MENU);
                foreach (var menus in menu)
                {

                    lista.Add(DTOBuilder.Menu(menus));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerMenu): " + ex.Message);
                lista = new List<DTO.Menu>();
            }
            return lista;
        }

        public bool ExisteMenu(string nombreMenu)
        {
            try
            {
                if (nombreMenu != "")
                {
                    var menu = db.MENU.Where(h => h.NOMBRE_MENU == nombreMenu);
                    if (menu.Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (ExisteMenu): " + ex.Message);
                return false;
            }
        }

        public bool IngresarMenu(string nombreMenu, int tipoMenu,int estiloMenu ,string descripcionMenu,int valorMenu,int cantidadMenu,int estadoMenu)
        {
            try
            {
                db.INSERTAR_MENU(nombreMenu, tipoMenu,estiloMenu,descripcionMenu,valorMenu,cantidadMenu, estadoMenu);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarMenu): " + ex.Message);
                return false;
            }
        }

        public DTO.Menu obtenerMenuPorId(int idMenu)
        {

            DTO.Menu respuesta = new DTO.Menu();

            try
            {
                Resources.MENU menu = db.MENU.FirstOrDefault(h => h.ID_MENU == idMenu);
                return DTOBuilder.Menu(menu);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerMenuPorId): " + ex.Message);
                respuesta = new DTO.Menu();
            }
            return respuesta;
        }

        public bool EditarMenu(int menuId, string nombreMenu, int tipoMenu,int estiloMenu, string descripcionMenu, int valorMenu, int cantidadMenu)
        {
            try
            {
                db.ACTUALIZAR_MENU(menuId, nombreMenu, tipoMenu, estiloMenu, descripcionMenu, valorMenu, cantidadMenu, 1);
                // db.ACTUALIZAR_EMPLEADO(empleadoId, empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EditarMenu): " + ex.Message);
                return false;
            }
        }

        public bool EliminarMenu(int menuId)
        {
            try
            {
                db.ELIMINAR_MENU(menuId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (EliminarMenu): " + ex.Message);
                return false;
            }
        }


        public bool cambiarEstadoComedor(int comedorId)
        {
            try
            {
                db.LIBERAR_COMEDOR(comedorId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (cambiarEstadoComedor): " + ex.Message);
                return false;
            }
        }

        public bool IngresarDetalleComedor(int empleadoId, int huespedId, int comedorId, int menuId)
        {
            try
            {

                //b.INSERTAR_DETALLE_HABITACION(estadoHabitacionId, habitacionValor, huespedId, habitacionId, empleadoId);
                db.INSERTAR_DETALLE_COMEDOR(comedorId, huespedId, empleadoId,menuId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarDetalleComedor): " + ex.Message);
                return false;
            }
        }

        public List<DTO.ComedorDetalle> obtenerComedoresConFecha()
        {
            List<DTO.ComedorDetalle> lista = new List<DTO.ComedorDetalle>();
            try
            {
                var detalleComedor = (from c in db.COMEDORES 
                                      join dc in db.DETALLE_COMEDORES
                                      on c.ID_COMEDOR equals dc.ID_COMEDOR  into resultado
                                      from pat in resultado.DefaultIfEmpty()
                                      
                                      join hu in db.HUESPED
                                      on pat.ID_HUESPED equals hu.ID_HUESPED into resultado_2
                                      from pat2 in resultado_2.DefaultIfEmpty()
                                      join em in db.EMPRESA
                                      on pat2.EMPRESA_ID_EMPRESA equals em.ID_EMPRESA into resultado_3
                                      from pat3 in resultado_3.DefaultIfEmpty()
                                     
                                      select  new
                                      {
                                          comedorId = c.ID_COMEDOR,
                                          comedorNombre = c.NOMBRE_COMEDOR,
                                          comedorEstado = c.ESTADO_COMEDOR,
                                          comedorCapacidad = c.CAPACIDAD_COMEDOR,
                                          estadoUsado = pat.ESTADO_USADO,
                                          nombreHuesped = pat2 == null ? "" : pat2.NOMBRE_HUESPED,
                                          apellidoHuesped = pat2 == null ? "" : pat2.APELLIDO_HUESPED,
                                          nombreEmpresa = pat3 == null ? "" : pat3.RAZON_SOCIAL,
                                          fechaInicio = pat == null ? DateTime.MinValue : pat.FECHA_SOLICITUD
                                      }).ToList();

           
                foreach (var row in detalleComedor)
                {
                    DTO.ComedorDetalle comedorDetalle = new DTO.ComedorDetalle();
                    comedorDetalle.idComedor = row.comedorId;
                    comedorDetalle.nombreComedor = row.comedorNombre;
                    comedorDetalle.estadoComedor = Convert.ToInt32(row.comedorEstado);
                    comedorDetalle.capacidadComedor = Convert.ToInt32(row.comedorCapacidad);
                    comedorDetalle.estadoUsado = Convert.ToInt32(row.estadoUsado);
                    comedorDetalle.nombreHuesped = row.nombreHuesped;
                    comedorDetalle.apellidoHuesped = row.apellidoHuesped;
                    comedorDetalle.nombreEmpresa = row.nombreEmpresa;
                    comedorDetalle.fechaInicio = Convert.ToDateTime(row.fechaInicio);
                    lista.Add(comedorDetalle);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerComedoresConFecha): " + ex.Message);
                lista = new List<DTO.ComedorDetalle>();
            }
            return lista;
        }

        public List<DTO.Servicios> obtenerReservas()
        {
            List<DTO.Servicios> lista = new List<DTO.Servicios>();
            try
            {
                var detalleServicios = (from dt in db.DETALLE_SERVICIO
                                      join hu in db.HUESPED
                                      on dt.ID_HUESPED equals hu.ID_HUESPED into resultado
                                      from pat in resultado.DefaultIfEmpty()
                                      join em in db.EMPRESA
                                      on pat.EMPRESA_ID_EMPRESA equals em.ID_EMPRESA into resultado_2
                                      from pat2 in resultado_2.DefaultIfEmpty()
                                      select new
                                      {
                                          detalleServicioId = dt.ID_DETALLE_SERVICIO,
                                          codigoServicio = dt.CODIGO_SERVICIO,
                                          huespedId = pat.ID_HUESPED,
                                          huespedNombre = pat.NOMBRE_HUESPED +" "+ pat.APELLIDO_HUESPED,
                                          empresaId = pat2.ID_EMPRESA,
                                          empresaNombre = pat2.RAZON_SOCIAL
                                      }).ToList();


                foreach (var row in detalleServicios)
                {
                    DTO.Servicios servicioDetalle = new DTO.Servicios();
                    servicioDetalle.detalleServicioId = Convert.ToInt32(row.detalleServicioId);
                    servicioDetalle.codigoServicio = Convert.ToInt32(row.codigoServicio);
                    servicioDetalle.huespedId = Convert.ToInt32(row.huespedId);
                    servicioDetalle.nombreHuesped = row.huespedNombre;
                    servicioDetalle.empresaId = Convert.ToInt32(row.empresaId);
                    servicioDetalle.EmpresaNombre = row.empresaNombre;
                    lista.Add(servicioDetalle);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerReservas): " + ex.Message);
                lista = new List<DTO.Servicios>();
            }
            return lista;
        }


        public List<DTO.CodigoServicio> obtenerCodigoServicioByCode(int codigoServicio)
        {
            List<DTO.CodigoServicio> lista = new List<DTO.CodigoServicio>();
            try
            {
                var detalleCodigoServicio = (from dt in db.DETALLE_SERVICIO
                                        join hu in db.HUESPED
                                        on dt.ID_HUESPED equals hu.ID_HUESPED into resultado
                                        from pat in resultado.DefaultIfEmpty()
                                        join em in db.EMPRESA
                                        on pat.EMPRESA_ID_EMPRESA equals em.ID_EMPRESA into resultado_2
                                        from pat2 in resultado_2.DefaultIfEmpty()
                                        join dh in db.DETALLE_HABITACIONES
                                        on dt.ID_HUESPED equals dh.HUESPED_ID_HUESPED into resultado_3
                                        from pat3 in resultado_3.DefaultIfEmpty()
                                        join ha in db.HABITACION
                                        on pat3.HABITACION_ID_HABITACION equals ha.ID_HABITACION into resultado_4
                                        from pat4 in resultado_4.DefaultIfEmpty()
                                        where dt.CODIGO_SERVICIO == codigoServicio
                                        select new
                                        {
                                            codigoServicio = dt.CODIGO_SERVICIO,
                                            fechaInicio = dt.FECHA_INICIO_RES,
                                            fechaFin = dt.FECHA_FIN_RES,
                                            //diasDif = dt.FECHA_FIN_RES - dt.FECHA_INICIO_RES,
                                            huespedId = pat.ID_HUESPED,
                                            huespedNombre = pat.NOMBRE_HUESPED + " " + pat.APELLIDO_HUESPED,
                                            empresaId = pat2.ID_EMPRESA,
                                            empresaNombre = pat2.RAZON_SOCIAL,
                                            valorServicio = dt.VALOR_SERVICIO,
                                            habitacionId = pat4.ID_HABITACION,
                                            habitacionNombre = pat4.NOMBRE_HABITACION,
                                            habitacionFecha = pat3.FECHA_INGRESO
                                        }).ToList();


                foreach (var row in detalleCodigoServicio)
                {
                    DTO.CodigoServicio codigoServicioDet = new DTO.CodigoServicio();
                    codigoServicioDet.codigoServicio = Convert.ToInt32(row.codigoServicio);
                    codigoServicioDet.fechaInicio = Convert.ToDateTime(row.fechaInicio);
                    codigoServicioDet.fechaFin = Convert.ToDateTime(row.fechaFin);
                    codigoServicioDet.diasDif = 0;//Convert.ToInt32(row.diasDif);
                    codigoServicioDet.huespedId = Convert.ToInt32(row.huespedId);
                    codigoServicioDet.huespedNombre = row.huespedNombre;
                    codigoServicioDet.empresaId = Convert.ToInt32(row.empresaId);
                    codigoServicioDet.empresaNombre = row.empresaNombre;
                    codigoServicioDet.valorServicio = Convert.ToInt32(row.valorServicio);
                    codigoServicioDet.habitacionId = Convert.ToInt32(row.huespedId);
                    codigoServicioDet.habitacionNombre = row.habitacionNombre;
                    codigoServicioDet.fechaIngresoHabitacion = Convert.ToDateTime(row.habitacionFecha);
                    lista.Add(codigoServicioDet);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerCodigoServicioByCode): " + ex.Message);
                lista = new List<DTO.CodigoServicio>();
            }
            return lista;
        }


        public List<DTO.comedorMenu> obtenerDetalleComedoresByHuespedId(int huespedId)
        {
            List<DTO.comedorMenu> lista = new List<DTO.comedorMenu>();
            try
            {
                var detalleComedorMenu = (from dc in db.DETALLE_COMEDORES
                                          join mu in db.MENU
                                          on dc.ID_MENU equals mu.ID_MENU into resultado
                                          from pat in resultado.DefaultIfEmpty()
                                          where dc.ID_HUESPED == huespedId
                                          select new
                                          {
                                              menuId = pat.ID_MENU,
                                              menuFechaSolicitud = dc.FECHA_SOLICITUD,
                                              menuValor = pat.VALOR_MENU,
                                              menuNombre = pat.NOMBRE_MENU,
                                              tipoMenu = pat.TIPO_MENU


                                             }).ToList();


                foreach (var row in detalleComedorMenu)
                {
                    DTO.comedorMenu comedorMenu = new DTO.comedorMenu();
                    comedorMenu.menuId = Convert.ToInt32(row.menuId);
                    comedorMenu.menuFechaSolicitud = Convert.ToDateTime(row.menuFechaSolicitud);
                    comedorMenu.valorMenu = Convert.ToInt32(row.menuValor);
                    comedorMenu.nombreMenu = row.menuNombre;
                    comedorMenu.tipoMenu = Convert.ToInt32(row.tipoMenu);
                    lista.Add(comedorMenu);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerDetalleComedoresByHuespedId): " + ex.Message);
                lista = new List<DTO.comedorMenu>();
            }
            return lista;
        }

        public bool cambiarEstadoHabitacion(int habitacionId)
        {
            try
            {
                db.LIBERAR_HABITACION(habitacionId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (cambiarEstadoHabitacion): " + ex.Message);
                return false;
            }
        }

        //PRoducto



        public bool IngresarProducto(string nombreProducto, int stock, int idProveedor,int estadoproducto)
        {
            try
            {
                db.INSERTAR_PRODUCTO(nombreProducto, stock, idProveedor, estadoproducto);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarProducto): " + ex.Message);
                return false;
            }
        }


        public DTO.Producto obtenerProductoPorId(int idProducto)
        {

            DTO.Producto respuesta = new DTO.Producto();

            try
            {
                Resources.PRODUCTO producto = db.PRODUCTO.FirstOrDefault(h => h.ID_PRODUCTO == idProducto);
                return DTOBuilder.Producto(producto);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerProductoPorId): " + ex.Message);
                respuesta = new DTO.Producto();
            }
            return respuesta;
        }



        public List<DTO.Producto> obtenerProducto()
        {
            List<DTO.Producto> lista = new List<DTO.Producto>();
            try
            {

                var productos = db.PRODUCTO.ToList();
                foreach (var producto in productos)
                {

                    lista.Add(DTOBuilder.Producto(producto));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerProducto): " + ex.Message);
                lista = new List<DTO.Producto>();
            }
            return lista;
        }

        public List<DTO.Producto> obtenerProductoPorProveedorId(int proveedorId)
        {

            List<DTO.Producto> lista = new List<DTO.Producto>();
            try
            {

                //var producto = db.HUESPED.Where(e => e.EMPRESA_ID_EMPRESA == empresaId && e.ID_ESTADO_HUESPED == 1 || e.ID_ESTADO_HUESPED == 2);   // db.HUESPED.ToList();
                var producto = db.PRODUCTO.Where(p => p.ID_PROVEEDOR == proveedorId);
                foreach (var Producto in producto)
                {

                    lista.Add(DTOBuilder.Producto(Producto));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (obtenerHuespedPorEmpresaId): " + ex.Message);
                lista = new List<DTO.Producto>();
            }
            return lista;
        }


        public bool IngresarOrdenDePedido(int provedoorId,string productos, int cantidadProductos, int codigoOrden)
        {
            try
            {

                //b.INSERTAR_DETALLE_HABITACION(estadoHabitacionId, habitacionValor, huespedId, habitacionId, empleadoId);
                db.INSERTAR_ORDEN_PEDIDO(provedoorId, productos, cantidadProductos, codigoOrden);
                //db.INSERTAR_DETALLE_COMEDOR(comedorId, huespedId, empleadoId, menuId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models (IngresarOrdenDePedido): " + ex.Message);
                return false;
            }
        }


        public List<DTO.OrdenPedido> obtenerOrdenesDePedido()
        {
            List<DTO.OrdenPedido> lista = new List<DTO.OrdenPedido>();
            try
            {

                var ordenPedidos = db.ORDEN_PEDIDO.ToList();
                foreach (var ordenPedido in ordenPedidos)
                {

                    lista.Add(DTOBuilder.OrdenPedido(ordenPedido));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("hostalDoñaClarita.Models.MantenedorModel (OrdenPedido): " + ex.Message);
                lista = new List<DTO.OrdenPedido>();
            }
            return lista;
        }
    }
}