using AspOracle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AspOracle.Controllers
{
    public class MantenedorController : Controller
    {

        public ActionResult Registro()
        {
            var modelMantenedor = new Models.MantenedorModel();

            var lista = modelMantenedor.obtenerEmpresas();
            string html = "";
            foreach (var empresa in lista)
            {
                html += "<tr>";
                html += "<td>" + empresa.razonSocial + "</td>";
                html += "<td>" + empresa.rutEmpresa + "-" + empresa.dvRutEmpresa + "</td>";
                html += "<td>" + empresa.correoEmpresa + "</td>";
                html += "<td>" + empresa.direccionEmpresa + "</td>";
                html += "<td>" + empresa.telefonoEmpresa + "</td>";
                html += "<td>" + empresa.nombreRepEmpresa + "</td>";
                html += "<td>" + empresa.apellidoRepEmpresa + "</td>";
                html += "<td><button class='btn btn-warning' onclick='obtenerEmpresa(\"" + empresa.rutEmpresa + "\")' >Editar</button>";
                html += "<button class='btn btn-success' onclick='eliminarEmpresa(" + empresa.idEmpresa + ", \"" + empresa.razonSocial + "\")' >Eliminar</button></td>";
                html += "</tr>";

            }

            ViewBag.Table = html;
            return View();
        }

        [HttpPost]
        public ActionResult obtenerEmpresa(string rutEmpresa)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Empresa empresa = modelMantenedor.obtenerEmpresaPorRut(rutEmpresa);

            return Json(new { empresaId = empresa.idEmpresa, razonSocial = empresa.razonSocial, rutEmpresa = empresa.rutEmpresa + "-" + empresa.dvRutEmpresa, correoEmpresa = empresa.correoEmpresa, direccionEmpresa = empresa.direccionEmpresa, telefono = empresa.telefonoEmpresa, nombreRep = empresa.nombreRepEmpresa, apellidoRep = empresa.apellidoRepEmpresa }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult editarEmpresa(int empresaId, string razon_social, string rut_empresa, string correo, string direccion, int telefono, string nombreRep, string apellidoRep)
        {
            var modelMantenedor = new Models.MantenedorModel();


            string rut_nuevo = rut_empresa.Substring(0, rut_empresa.Length - 2);
            string dv_rut_empresa = rut_empresa.Substring(rut_empresa.Length - 1, 1);
            char dv_rut = Convert.ToChar(dv_rut_empresa);


            if (modelMantenedor.EditarEmpresas(empresaId, razon_social, rut_nuevo, dv_rut, correo, direccion, telefono, nombreRep, apellidoRep))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult eliminarEmpresa(int empresaId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (!modelMantenedor.EmpresaEsEliminable(empresaId))
            {
                if (modelMantenedor.EliminarEmpresas(empresaId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar la empresa..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Borrar la Empresa porque tiene datos asociados..." }, JsonRequestBehavior.AllowGet);
            }


        }



        //HUESPED
        public ActionResult Huesped()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Dashboard";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    int empresaId = SessionHandler.EmpresaId;

                    var lista = modelMantenedor.obtenerHuespedPorEmpresaId(empresaId);
                    string html = "";
                    foreach (var huesped in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + huesped.nombreHuesped + "</td>";
                        html += "<td>" + huesped.apellidoHuesped + "</td>";
                        html += "<td>" + huesped.rutHuesped + "-" + huesped.dvRutHuesped + "</td>";
                        html += "<td>" + huesped.telefonoHuesped + "</td>";
                        html += "<td>" + huesped.correoHuesped + "</td>";
                        html += "<td><button class='btn btn-warning' onclick='obtenerHuesped(" + huesped.rutHuesped + ")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='eliminarHuesped(" + huesped.idHuesped + ", \"" + huesped.nombreHuesped + " " + huesped.apellidoHuesped + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;


                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult ingresarHuesped(string huespedNombre, string huespedApellido, string huespedRut, int huespedTelefono, string huespedCorreo)
        {
            var modelMantenedor = new Models.MantenedorModel();
            //esto crea el campo rut 
            string rut_nuevo = huespedRut.Replace(".", "").Substring(0, huespedRut.Replace(".", "").Length - 2);
            int rut = Convert.ToInt32(rut_nuevo.ToString());
            //esto crea el campo dv rut 
            string dv_rut_empresa = huespedRut.Replace(".", "").Substring(huespedRut.Replace(".", "").Length - 1, 1);
            char dv_rut = Convert.ToChar(dv_rut_empresa);
            //esto crea el campo empresaId  
            int empresaId = SessionHandler.EmpresaId;


            if (modelMantenedor.ExisteHuesped(rut))
            {
                return Json(new { response = "error", message = "El Huesped con el rut " + rut + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarHuesped(huespedNombre, huespedApellido, rut, dv_rut, huespedTelefono, huespedCorreo, empresaId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult obtenerHuesped(int rutHuesped)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Huesped huesped = modelMantenedor.obtenerHuespedPorRut(rutHuesped);

            return Json(new { huespedId = huesped.idHuesped, huespedNombre = huesped.nombreHuesped, huespedApellido = huesped.apellidoHuesped, huespedRut = huesped.rutHuesped + "-" + huesped.dvRutHuesped, huespedTelefono = huesped.telefonoHuesped, huespedCorreo = huesped.correoHuesped, huespedEmpresaId = huesped.empresa.idEmpresa }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult editarHuesped(int huespedId, string huespedNombre, string huespedApellido, string huespedRut, int huespedTelefono, string huespedCorreo)
        {
            var modelMantenedor = new Models.MantenedorModel();

            //esto crea el campo rut 
            string rut_nuevo = huespedRut.Replace(".", "").Substring(0, huespedRut.Replace(".", "").Length - 2);
            int rut = Convert.ToInt32(rut_nuevo.ToString());
            //esto crea el campo dv rut 
            string dv_rut_empresa = huespedRut.Replace(".", "").Substring(huespedRut.Replace(".", "").Length - 1, 1);
            char dv_rut = Convert.ToChar(dv_rut_empresa);
            //esto crea el campo empresaId  
            int empresaId = SessionHandler.EmpresaId;


            if (modelMantenedor.EditarHuesped(huespedId, huespedNombre, huespedApellido, rut, dv_rut, huespedTelefono, huespedCorreo, empresaId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar El Huesped..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult eliminarHuesped(int huespedId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarHuesped(huespedId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar el  huesped..." }, JsonRequestBehavior.AllowGet);
            }



        }

        //EMPLEADO

        public ActionResult Empleado()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Dashboard";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerEmpleado();
                    string html = "";
                    foreach (var empleado in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + empleado.nombreEmpleado + "</td>";
                        html += "<td>" + empleado.apellidoEmpleado + "</td>";
                        html += "<td>" + empleado.telefonoEmpleado + "</td>";
                        html += "<td>" + empleado.correoEmpleado + "</td>";
                        html += "<td>" + empleado.edadEmpleado + "</td>";
                        html += "<td>" + empleado.direccionEmpleado + "</td>";

                        html += "<td><button class='btn btn-warning' onclick='obtenerEmpleado(\"" + empleado.correoEmpleado + "\")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='EliminarEmpleado(" + empleado.empleadoId + ", \"" + empleado.nombreEmpleado + " " + empleado.apellidoEmpleado + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;


                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult ingresarEmpleado(string empleadoNombre, string empleadoApellido, int empleadoTelefono, string empleadoCorreo, int empleadoEdad, string empleadoDireccion)
        {
            var modelMantenedor = new Models.MantenedorModel();

            //esto crea el campo empresaId  
            int perfilId = 2;
            int empresaId = SessionHandler.EmpresaId;


            if (modelMantenedor.ExisteEmpleado(empleadoCorreo))
            {
                return Json(new { response = "error", message = "El Empleado con el correo " + empleadoCorreo + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            Random r = new Random();
            int aleatorio = r.Next(11111111, 99999999);

            if (modelMantenedor.IngresarUsuarioEmpleado(empleadoNombre, empleadoApellido, empleadoCorreo, aleatorio.ToString(), empresaId))
            {

                Enviar_Email_Password(empleadoCorreo, aleatorio.ToString(), empleadoCorreo);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

            Models.DTO.Usuario usuario = modelMantenedor.obtenerUsuarioPorCorreo(empleadoCorreo);

            if (modelMantenedor.IngresarEmpleado(empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion, usuario.idUsuario, perfilId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        public Boolean Enviar_Email(String subject, String destinatario, String body, String footer)
        {
            try
            {
                // MembershipUser myObject = Membership.GetUser();
                String addressAdm = System.Configuration.ConfigurationManager.AppSettings["ADDRESS_ADM"];
                MailAddress fromAddress = new MailAddress(addressAdm, "Administrator");
                //Cuenta Email : Administrator lotus.123 
                body = body.Replace("chr(13)", "<br />");


                var message = new System.Net.Mail.MailMessage()
                {
                    //Encabezado Normal
                    Subject = subject,
                    Body = body + "<br /><br />" + footer
                };

                message.IsBodyHtml = true;
                message.From = fromAddress;
                string strData = destinatario.ToString();
                char[] separator = new char[] { ',' };
                string[] strSplitArr = strData.Split(separator);
                foreach (String arrStr in strSplitArr)
                {
                    message.To.Add(new MailAddress(arrStr, "Destinatario"));
                }



                String clientSMTP = System.Configuration.ConfigurationManager.AppSettings["CLIENT_SMTP"];
                String userNameSMTP = System.Configuration.ConfigurationManager.AppSettings["USER_NAME_SMTP"];
                String pswSMTP = System.Configuration.ConfigurationManager.AppSettings["PSW_SMTP"];

                SmtpClient client = new SmtpClient();


                //Configuracion Mail de Doña Clarita:

                client = new SmtpClient(clientSMTP, 587);
                client.Credentials = new System.Net.NetworkCredential(userNameSMTP, pswSMTP);
                client.EnableSsl = true;


                client.Send(message);

                return true;
            }
            catch
            {
                //error en envio de correo
                return false;
            }
        }

        public void Enviar_Email_Password(string usuario, string password, string email)
        {
            
           

            string subject = "Credenciales para acceder al sistema";
            string body = "";
            body = "Estimado usuario, estas son las credenciales que necesita para acceder al Sistema del Hostal Doña Clarita";
            body += "<table><thead><th></th></thead><tbody><tr><td><b>Username:</b></td><td>'" + usuario + "'</td></tr><tr><td><b>Password:</b></td><td>'" + password + "'</td></tr></tbody></table>";
            string footer = "Mensaje Auto generado por Hostal Doña Clarita";

            Enviar_Email(subject, email.ToString(), body, footer);


        }


        public void Enviar_Email_OrdenPedido(string productos, string codigo, string email, int cantidad)
        {

            var modelMantenedor = new Models.MantenedorModel();

            string[] words = productos.Split(',');

            string subject = "Orden de Pedido " + codigo + " ";
            string body = "";
            body = "Estimado Proveedor, estas son los productods que se necesitan en la  Hostal Doña Clarita";
            body += "<table><thead><th></th></thead><tbody>";

           

            foreach (var uid in words)
            {
                Models.DTO.Producto producto =  modelMantenedor.obtenerProductoPorId(Convert.ToInt32(uid));
               
                body += "<tr><td><b>Producto:</b></td><td>'" + producto.nombreProducto + "'</td></tr><tr><td><b>Cantidad:</b></td><td>'" + cantidad + "'</td></tr>";
            }
            body += "</tbody></table>";

            string footer = "Mensaje Auto generado por Hostal Doña Clarita";

            Enviar_Email(subject, email.ToString(), body, footer);


        }


        [HttpPost]
        public ActionResult obtenerEmpleado(string correoEmpleado)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Empleados empleado = modelMantenedor.obtenerEmpleadoPorCorreo(correoEmpleado);

            //  return Json(new { huespedId = huesped.idHuesped, huespedNombre = huesped.nombreHuesped, huespedApellido = huesped.apellidoHuesped, huespedRut = huesped.rutHuesped + "-" + huesped.dvRutHuesped, huespedTelefono = huesped.telefonoHuesped, huespedCorreo = huesped.correoHuesped, huespedEmpresaId = huesped.empresa.idEmpresa }, JsonRequestBehavior.AllowGet);
            return Json(new { empleadoId = empleado.empleadoId, empleadoNombre = empleado.nombreEmpleado, empleadoApellido = empleado.apellidoEmpleado, empleadoTelefono = empleado.telefonoEmpleado, empleadoCorreo = empleado.correoEmpleado, empleadoEdad = empleado.edadEmpleado, empleadoDireccion = empleado.direccionEmpleado });
        }


        [HttpPost]
        public ActionResult EliminarEmpleado(int empleadoId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarEmpleado(empleadoId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar al Empleado..." }, JsonRequestBehavior.AllowGet);
            }



        }


        [HttpPost]
        public ActionResult editarEmpleado(int empleadoId, string empleadoNombre, string empleadoApellido, int empleadoTelefono, string empleadoCorreo, int empleadoEdad, string empleadoDireccion)
        {
            var modelMantenedor = new Models.MantenedorModel();




            if (modelMantenedor.EditarEmpleado(empleadoId, empleadoNombre, empleadoApellido, empleadoTelefono, empleadoCorreo, empleadoEdad, empleadoDireccion))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar El Huesped..." }, JsonRequestBehavior.AllowGet);
            }

        }

        //PROVEEDOR
        public ActionResult Proveedor()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Dashboard";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerProveedor();
                    string html = "";
                    foreach (var proveedor in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + proveedor.nombreProveedor + "</td>";
                        html += "<td>" + proveedor.contactoProveedor + "</td>";
                        html += "<td>" + proveedor.rubroProveedor + "</td>";
                        html += "<td>" + proveedor.direccionProveedor + "</td>";
                        html += "<td>" + proveedor.correoProveedor + "</td>";

                        html += "<td><button class='btn btn-warning' onclick='obtenerProveedor(\"" + proveedor.correoProveedor + "\")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='EliminarProveedor(" + proveedor.proveedorId + ", \"" + proveedor.nombreProveedor + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;


                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult obtenerProveedor(string correoProveedor)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Proveedor proveedor = modelMantenedor.obtenerProveedorPorCorreo(correoProveedor);

            //  return Json(new { huespedId = huesped.idHuesped, huespedNombre = huesped.nombreHuesped, huespedApellido = huesped.apellidoHuesped, huespedRut = huesped.rutHuesped + "-" + huesped.dvRutHuesped, huespedTelefono = huesped.telefonoHuesped, huespedCorreo = huesped.correoHuesped, huespedEmpresaId = huesped.empresa.idEmpresa }, JsonRequestBehavior.AllowGet);
            return Json(new { proveedorId = proveedor.proveedorId, proveedorNombre = proveedor.nombreProveedor, proveedorContacto = proveedor.contactoProveedor, proveedorRubro = proveedor.rubroProveedor, proveedorDireccion = proveedor.direccionProveedor, proveedorCorreo = proveedor.correoProveedor });
        }


        [HttpPost]
        public ActionResult ingresarProveedor(string proveedorNombre, int proveedorContacto, string proveedorRubro, string proveedorDireccion, string proveedorCorreo)
        {
            var modelMantenedor = new Models.MantenedorModel();


            int perfilId = 4;
            //int empresaId = SessionHandler.EmpresaId;


            if (modelMantenedor.ExisteProveedor(proveedorCorreo))
            {
                return Json(new { response = "error", message = "El Huesped con el rut " + proveedorCorreo + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarProveedor(proveedorNombre, proveedorContacto, proveedorRubro, proveedorDireccion, proveedorCorreo, perfilId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult EditarProveedor(int proveedorId, string proveedorNombre, int proveedorContacto, string proveedorRubro, string proveedorDireccion, string proveedorCorreo)
        {
            var modelMantenedor = new Models.MantenedorModel();




            if (modelMantenedor.EditarProveedor(proveedorId, proveedorNombre, proveedorContacto, proveedorRubro, proveedorDireccion, proveedorCorreo))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar El Proveedor..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult EliminarProveedor(int proveedorId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarProveedor(proveedorId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar al Proveedor..." }, JsonRequestBehavior.AllowGet);
            }



        }

        //HABITACION

        public ActionResult Habitaciones()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Habitaciones";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerHabitaciones();
                    string html = "";
                    foreach (var habitacion in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + habitacion.nombreHabitacion + "</td>";
                        html += "<td>" + habitacion.tipoCamaHabitacion + "</td>";
                        html += "<td>" + habitacion.accesoriosHabitacion + "</td>";
                        html += "<td>" + habitacion.valorHabitacion + "</td>";
                        html += "<td>" + habitacion.estadoHabitacion + "</td>";
                        html += "<td><button class='btn btn-warning' onclick='obtenerHabitacion(" + habitacion.idHabitacion + ")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='eliminarHabitacion(" + habitacion.idHabitacion + ", \"" + habitacion.nombreHabitacion + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;



                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }


        }


        [HttpPost]
        public ActionResult ingresarHabitacion(string nombreHabitacion, string tipoCamaHabitacion, string accesorioHabitacion, int valorHabitacion)
        {
            var modelMantenedor = new Models.MantenedorModel();

            int estadoHabitacion = 1;

            if (modelMantenedor.ExisteHabitacion(nombreHabitacion))
            {
                return Json(new { response = "error", message = "la habitacion " + nombreHabitacion + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }



            if (modelMantenedor.IngresarHabitacion(nombreHabitacion, tipoCamaHabitacion, accesorioHabitacion, valorHabitacion, estadoHabitacion))
            {


                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }




        }


        [HttpPost]
        public ActionResult obtenerHabitacion(int idHabitacion)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Habitacion habitacion = modelMantenedor.obtenerHabitacionPorId(idHabitacion);

            return Json(new { idHabitacion = habitacion.idHabitacion, nombreHabitacion = habitacion.nombreHabitacion, tipoCamaHabitacion = habitacion.tipoCamaHabitacion, accesorioHabitacion = habitacion.accesoriosHabitacion, valorHabitacion = habitacion.valorHabitacion }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult editarHabitacion(int habitacionId, string nombreHabitacion, string tipoCamaHabitacion, string accesorioHabitacion, int valorHabitacion)
        {
            var modelMantenedor = new Models.MantenedorModel();




            if (modelMantenedor.EditarHabitacion(habitacionId, nombreHabitacion, tipoCamaHabitacion, accesorioHabitacion, valorHabitacion))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar El Huesped..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult EliminarHabitacion(int habitacionId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarHabitacion(habitacionId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar la Habitacion..." }, JsonRequestBehavior.AllowGet);
            }



        }

        //COMEDOR

        public ActionResult Comedores()
        {

            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Comedores";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerComedor();
                    string html = "";
                    foreach (var comedor in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + comedor.nombreComedor + "</td>";
                        html += "<td>" + comedor.capacidadComedor + "</td>";
                        html += "<td>" + comedor.estadoComedor + "</td>";
                        html += "<td><button class='btn btn-warning' onclick='obtenerComedor(" + comedor.idComedor + ")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='eliminarComedor(" + comedor.idComedor + ", \"" + comedor.nombreComedor + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;



                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }


        }

        [HttpPost]
        public ActionResult obtenerComedor(int idComedor)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Comedor comedor = modelMantenedor.obtenerComedorPorId(idComedor);

            return Json(new { idComedor = comedor.idComedor, nombreComedor = comedor.nombreComedor, capacidadComedor = comedor.capacidadComedor }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult IngresarComedor(string nombreComedor, int capacidadComedor)
        {
            var modelMantenedor = new Models.MantenedorModel();

            int estadoComedor = 1;

            if (modelMantenedor.ExisteComedor(nombreComedor))
            {
                return Json(new { response = "error", message = "el comedor " + nombreComedor + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }



            if (modelMantenedor.IngresarComedor(nombreComedor, capacidadComedor, estadoComedor))
            {


                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el comedor..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult editarComedor(int comedorId, string nombreComedor, int capacidadComedor)
        {
            var modelMantenedor = new Models.MantenedorModel();




            if (modelMantenedor.EditarComedor(comedorId, nombreComedor, capacidadComedor))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el comedor..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult EliminarComedor(int comedorId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarComedor(comedorId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar el comedor..." }, JsonRequestBehavior.AllowGet);
            }



        }

        //MENU

        public ActionResult Menu()
        {

            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Menu";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerMenu();
                    string html = "";
                    string tipoMenu = "";
                    string estilomenu = "";
                    foreach (var menu in lista)
                    {
                        switch (menu.tipoMenu) {
                            case 1:
                                tipoMenu = "DESAYUNO";
                                break;
                            case 2:
                                tipoMenu = "ALMUERZO";
                                break;
                            case 3:
                                tipoMenu = "CENA";
                                break;
                        }
                        switch (menu.estiloMenu)
                        {
                            case 1:
                                estilomenu = "VEGETARIANO";
                                break;
                            case 2:
                                estilomenu = "ESPECIAL";
                                break;
                            case 3:
                                estilomenu = "EJECUTIVO";
                                break;
                        }
                        html += "<tr>";
                        html += "<td>" + menu.nombreMenu + "</td>";
                        html += "<td>" + tipoMenu + "</td>";
                        html += "<td>" + estilomenu + "</td>";
                        html += "<td>" + menu.descripcionMenu + "</td>";
                        html += "<td>" + menu.valorMenu + "</td>";
                        html += "<td>" + menu.cantidadMenu + "</td>";
                        html += "<td>" + menu.estadoMenu + "</td>";
                        html += "<td><button class='btn btn-warning' onclick='obtenerMenu(" + menu.idMenu + ")' >Editar</button>";
                        html += "<button class='btn btn-success' onclick='eliminarMenu(" + menu.idMenu + ", \"" + menu.nombreMenu + "\")' >Eliminar</button></td>";
                        html += "</tr>";

                    }

                    ViewBag.Table = html;



                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }


        }

        [HttpPost]
        public ActionResult obtenerMenu(int idMenu)
        {
            var modelMantenedor = new Models.MantenedorModel();
            Models.DTO.Menu menu = modelMantenedor.obtenerMenuPorId(idMenu);

            string htmlSelectMenu = "";

            htmlSelectMenu = "<select id='tipoMenu'  class='form-control'>";
            int tipo_menu = menu.tipoMenu ;

            switch (tipo_menu)
            {

                case 1:
                    htmlSelectMenu += "<option value = '0'>seleccione</option><option value = '1' selected>DESAYUNO</option><option value = '2' >ALMUERZO</option><option value = '3' > CENA </option> ";
                    break;
                case 2:
                    htmlSelectMenu += "<option value = '0'>seleccione</option><option value = '1'> DESAYUNO </option><option value = '2'  selected>ALMUERZO</option><option value = '3'>CENA</option>";
                    break;
                case 3:
                    htmlSelectMenu += "<option value = '0'> seleccione</option><option value = '1'>DESAYUNO</option><option value = '2'>ALMUERZO</option><option value = '3' selected>CENA</option> ";
                    break;
            }

            htmlSelectMenu += "</select>";

            // Estilo
            string htmlEstiloMenu = "";

            htmlEstiloMenu = "<select id='estiloMenu'  class='form-control'>";


            int estilo_menu = menu.estiloMenu;


            switch (estilo_menu)
            {

                case 1:
                    htmlEstiloMenu += "<option value = '0'> seleccione</option><option value = '1' selected>VEGETARIANO</option><option value = '2'>ESPECIAL</option><option value = '3'>EJECUTIVO</option> ";
                    break;
                case 2:
                    htmlEstiloMenu += "<option value = '0'> seleccione</option><option value = '1'>VEGETARIANO</option><option value = '2'  selected>ESPECIAL </option><option value = '3'>EJECUTIVO</option>";
                    break;
                case 3:
                    htmlEstiloMenu += "<option value = '0'>seleccione</option><option value = '1'> VEGETARIANO</option><option value = '2'>ESPECIAL</option><option value = '3' selected>EJECUTIVO</option > ";
                    break;
            }

            htmlEstiloMenu += "</select>";


            return Json(new { idMenu = menu.idMenu, nombreMenu = menu.nombreMenu, tipoMenu = menu.tipoMenu, estiloMenu = menu.estiloMenu, descripcionMenu = menu.descripcionMenu, valorMenu = menu.valorMenu, cantidadMenu = menu.cantidadMenu, SELECT_MENU = htmlSelectMenu, ESTILO_MENU = htmlEstiloMenu }, JsonRequestBehavior.AllowGet);




        }

        [HttpPost]
        public ActionResult IngresarMenu(string nombreMenu, int tipoMenu, int estiloMenu, string descripcionMenu, int valorMenu, int cantidadMenu)
        {
            var modelMantenedor = new Models.MantenedorModel();

            int estadoMenu = 1;

            if (modelMantenedor.ExisteMenu(nombreMenu))
            {
                return Json(new { response = "error", message = "el menu " + nombreMenu + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }



            if (modelMantenedor.IngresarMenu(nombreMenu, tipoMenu, estiloMenu, descripcionMenu, valorMenu, cantidadMenu, estadoMenu))
            {


                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el menu..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult editarMenu(int menuId, string nombreMenu, int tipoMenu, int estiloMenu, string descripcionMenu, int valorMenu, int cantidadMenu)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.EditarMenu(menuId, nombreMenu, tipoMenu, estiloMenu, descripcionMenu, valorMenu, cantidadMenu))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el Menu..." }, JsonRequestBehavior.AllowGet);
            }

        } 

        [HttpPost]
        public ActionResult EliminarMenu(int menuId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EliminarMenu(menuId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar el Menu..." }, JsonRequestBehavior.AllowGet);
            }



        }

        //ORDEN DE PEDIDO

        public ActionResult OrdenPedido()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Dashboard";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerProveedor();

                    string html = "<select id='proveedorId' class='form-control' onchange='seleccionarProduto()' > ";
                    html += "<option value='0' selected>Seleccione Una Opcion</option>";
                    foreach (var row in lista)
                    {

                        html += "<option value='" + row.proveedorId + "'>" + row.nombreProveedor + "</option>";
                    }

                    html += "</select>";

                    ViewBag.SelectProveedor = html;

                    var listaOrdenes = modelMantenedor.obtenerOrdenesDePedido();

                    string tablaOrdenesPedido = "";
                    foreach (var table in listaOrdenes)
                    {
                        tablaOrdenesPedido += "<tr>";
                        tablaOrdenesPedido += "<td>" + table.codigoOrdenPedido + "</td>";
                        tablaOrdenesPedido += "<td>" + table.proveedor.nombreProveedor + "</td>";

                        string[] words = table.productos.Split(',');
                        string productoNombre = "";
                        foreach (var uid in words)
                        {
                            Models.DTO.Producto producto = modelMantenedor.obtenerProductoPorId(Convert.ToInt32(uid));
                            productoNombre += " "+producto.nombreProducto ; 
                        }

                        tablaOrdenesPedido += "<td>" + productoNombre + "</td>";
                        tablaOrdenesPedido += "<td>" + table.cantidad + "</td>";
                        tablaOrdenesPedido += "</tr>";
                    }
                    ViewBag.TablaOrdenesPedido = tablaOrdenesPedido;
                    //// var lista = modelMantenedor.obtenerHuesped();
                    //string html = "<table class='separador'><th><input type ='checkbox' id='selectall' onclick='seleccionarTodos()' ></th><th>Seleccionar Todos  Los Provedoores</th><th></th><th></th><th></th><th></th>";

                    //foreach (var proveedor in lista)
                    //{
                    //    html += "<tr>";
                    //    html += "<td><input type='checkbox' class='case'  value='" + proveedor.proveedorId + "' ></td>";
                    //    html += "<td>" + proveedor.nombreProveedor + "</td>";
                    //    html += "<td>" + proveedor.contactoProveedor + "</td>";
                    //    html += "<td>" + proveedor.rubroProveedor  + "</td>";
                    //    html += "<td>" + proveedor.direccionProveedor + "</td>";
                    //    html += "<td>" + proveedor.correoProveedor + "</td>";
                    //    html += "</tr>";

                    //}
                    //html += "</table>";
                    //ViewBag.Table = html;





                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        //Producto

        public ActionResult Producto()
        {

            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Producto";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerProveedor();

                    string html = "<select id='proveedorId' class='form-control'> ";
                    html += "<option value='0' selected>Seleccione Una Opcion</option>";
                    foreach (var row in lista) {

                        html += "<option value='"+row.proveedorId+"'>"+row.nombreProveedor+"</option>";
                    }

                    html += "</select>";

                    ViewBag.SelectProveedor = html;

                    var listaProducto = modelMantenedor.obtenerProducto();
                    string htmlTabla = "";
                    foreach (var producto in listaProducto)
                    {
                        htmlTabla += "<tr>";
                        htmlTabla += "<td>" +  producto.nombreProducto+ "</td>";
                        htmlTabla += "<td>" + producto.stockProducto + "</td>";
                        htmlTabla += "<td>" + producto.proveedor.nombreProveedor + "</td>";
                        htmlTabla += "<td><onclick='obtenerProducto(" + producto.idProducto + ")' ";
                      //  htmlTabla += "<button class='btn btn-success' onclick='eliminarProducto(" + producto.idProducto + ", \"" + producto.nombreProducto + "\")' >Eliminar</button></td>";
                        htmlTabla += "</tr>";
                    }
                    ViewBag.Tabla = htmlTabla;

                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }


        }

        

        [HttpPost]
        public ActionResult obtenerProductoPorId(int productoId)
        {
            var modelMantenedor = new Models.MantenedorModel();

           var lista  = modelMantenedor.obtenerProductoPorProveedorId(productoId);


            string html = "<select id='productoId' class='form-control js-example-basic-multiple' name='productos[]' multiple='multiple'> ";
            foreach (var row in lista)
            {

                html += "<option value='" + row.idProducto + "'>" + row.nombreProducto + "</option>";
            }

            html += "</select>";


            return Json(new { productoSelect = html }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult IngresarProducto(string nombreProducto, int stockProducto, int proveedorId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            int estadoProducto = 1;

          
            if (modelMantenedor.IngresarProducto(nombreProducto, stockProducto, proveedorId, estadoProducto))
            {


                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el menu..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult ingresarOrdenDePedido(int proveedorId, string[] productos, int cantidadProducto)
        {
            var modelMantenedor = new Models.MantenedorModel();

          var producto = string.Join(",", productos);


            Models.DTO.Proveedor proveedor = modelMantenedor.obtenerProveedorPorId(proveedorId);

            Random r = new Random();
            int codigo = r.Next(11111111, 99999999);

            if (modelMantenedor.IngresarOrdenDePedido(proveedorId, producto, cantidadProducto, codigo))
            {
                Enviar_Email_OrdenPedido(producto, codigo.ToString(), proveedor.correoProveedor, cantidadProducto);

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }


       
        //[HttpPost]
        //public ActionResult obtenerProducto(int idproducto)
        //{
        //    var modelMantenedor = new Models.MantenedorModel();

        //    Models.DTO.Producto producto = modelMantenedor.obtenerComedorPorId(idComedor);

        //    return Json(new { idComedor = comedor.idComedor, nombreComedor = comedor.nombreComedor, capacidadComedor = comedor.capacidadComedor }, JsonRequestBehavior.AllowGet);

        //}



    }
}