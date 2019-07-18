using AspOracle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspOracle.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Solicitar()
        {


            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Habitaciones";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();



                    int empresaId = SessionHandler.EmpresaId;

                    var lista = modelMantenedor.obtenerHuespedPorEmpresaId(empresaId);

                   // var lista = modelMantenedor.obtenerHuesped();
                    string html = "<table class='separador'><th><input type ='checkbox' id='selectall' onclick='seleccionarTodos()' ></th><th>Seleccionar Todos  Los Huespedes</th><th></th><th></th><th></th><th></th>";

                    foreach (var huesped in lista)
                    {
                        html += "<tr>";
                        html += "<td><input type='checkbox' class='case'  value='" + huesped.idHuesped + "' ></td>";
                        html += "<td>" + huesped.nombreHuesped + "</td>";
                        html += "<td>" + huesped.apellidoHuesped + "</td>";
                        html += "<td>" + huesped.rutHuesped + "-" + huesped.dvRutHuesped + "</td>";
                        html += "<td>" + huesped.telefonoHuesped + "</td>";
                        html += "<td>" + huesped.correoHuesped + "</td>";
                        html += "</tr>";

                    }
                    html += "</table>";
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


        // GET: Servicio
        public ActionResult Revisar()
        {


            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Revisar";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();






                    var lista = modelMantenedor.obtenerHabitaciones();
                    var lista2 = modelMantenedor.obtenerHabitacionesConFecha();
                    string html = "<table class='separador'><thead></thead><tbody>";

                    html += "<tr>";

                    int contador = 0;
                    foreach (var habitacion in lista)
                    {

                        contador++;



                        if (contador > 5)
                        {
                            html += "</tr><tr>";
                            contador = 1;
                        }

                        if (habitacion.estadoHabitacion == 1)
                        {
                            html += "<td><div id='" + habitacion.nombreHabitacion + "' class='disponible' onclick='cambiarEstados(this,"+habitacion.idHabitacion+ ",\"" + habitacion.nombreHabitacion + "\")'><span><center>" + habitacion.nombreHabitacion + "<br /><span>(DISPONIBLE)</span></center></span></div></td>";
                        }
                        else {

                            foreach (var habitacion2 in lista2)
                            {

                                if (habitacion.nombreHabitacion == habitacion2.nombreHabitacion)
                                {

                                    if (habitacion2.estadoUsado == 2)
                                    {
                                        html += "<td><div id='" + habitacion.nombreHabitacion + "' class='ocupado' onclick='cambiarEstados(this," + habitacion.idHabitacion + ",\"" + habitacion.nombreHabitacion + "\")'><b><span><center>" + habitacion.nombreHabitacion + "</span><br /><span>(OCUPADO)</span><b/><br /><span>" + habitacion2.nombreHuesped + " " + habitacion2.apellidoHuesped + "</span><br /><span>" + habitacion2.nombreEmpresa + "</span><br /><span>" + habitacion2.fechaInicio + "</span><br /><button class='btn blue' onclick='liberarHabitacion(" + habitacion.idHabitacion + ",\"" + habitacion.nombreHabitacion + "\")' >Liberar Habitacion</button></center></div></td>";
                                    }

                                }


                            }


                         
                        }                      


                    }
                    html += "</tr>";

                    html += "</tbody></table>";
                    ViewBag.Habitaciones = html;

                    

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

        public ActionResult Salida()
        {


            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Revisar";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    //var lista = modelMantenedor.obtenerHabitaciones();
                    var lista = modelMantenedor.obtenerHabitacionesConFecha();
                    string html = "<table class='separador'><thead></thead><tbody>";

                    html += "<tr>";

                    int contador = 0;
                    foreach (var habitacion in lista)
                    {

                        contador++;



                        if (contador > 5)
                        {
                            html += "</tr><tr>";
                            contador = 0;
                        }

                        if (habitacion.estadoHabitacion == 1)
                        {
                            html += "<td><div id='" + habitacion.nombreHabitacion + "' class='disponible' onclick='cambiarEstados(this," + habitacion.idHabitacion + ",\"" + habitacion.nombreHabitacion + "\")'><span><center>" + habitacion.nombreHabitacion + "<br /><span>(DISPONIBLE)</span></center></span></div></td>";
                        }
                        else
                        {

                            html += "<td><div id='" + habitacion.nombreHabitacion + "' class='ocupado' onclick='cambiarEstados(this," + habitacion.idHabitacion + ",\"" + habitacion.nombreHabitacion + "\")'><b><span><center>" + habitacion.nombreHabitacion + "</span><br /><span>(OCUPADO)</span><b/><br /><span>" + habitacion.nombreHuesped + " " + habitacion.apellidoHuesped + "</span><br /><span>" + habitacion.nombreEmpresa + "</span><br /><span>" + habitacion.fechaInicio + "</span></center></div></td>";
                        }


                    }
                    html += "</tr>";

                    html += "</tbody></table>";
                    ViewBag.Habitaciones = html;

                  

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

        public ActionResult Facturacion(int codigoServicio)
        {


            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Revisar";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();
                    var lista = modelMantenedor.obtenerCodigoServicioByCode(codigoServicio);

                    string codigoReserva = "";
                    int empresaId = 0;
                    int valorServicio = 0;
                    int huespedId = 0;
                    string servicio = "";
                    string tablaFactura = "";
                    int diasDif = 0;
                    int sumaTotal = 0;
                    DateTime fechaFin = DateTime.MinValue;
                    

                    foreach (var row in lista) {
                        codigoReserva = row.codigoServicio.ToString();
                        empresaId = row.empresaId;
                        fechaFin = row.fechaFin;
                        valorServicio = row.valorServicio;
                        huespedId = row.huespedId;
                        switch (valorServicio) {
                            case 25000:
                                servicio = "SERVICIO 1";
                                break;
                            case 35000:
                                servicio = "SERVICIO 2";
                                break;
                            case 45000:
                                servicio = "SERVICIO 3";
                                break;
                            case 55000:
                                servicio = "SERVICIO 4";
                                break;

                        }
                  


                        DateTime fecha1 = Convert.ToDateTime(row.fechaInicio);
                        DateTime fecha2 = Convert.ToDateTime(row.fechaFin);

                        TimeSpan dias = fecha2.Subtract(fecha1);

                        diasDif = Convert.ToInt32(dias.Days.ToString());
                        sumaTotal = sumaTotal + (valorServicio * diasDif);

                        tablaFactura += "<tr>";
                        tablaFactura += "<td>"+ servicio +" por " + diasDif + " Dias Huesped "+ row.huespedNombre +"</td>";
                        tablaFactura += "<td>1</td>";
                        tablaFactura += "<td>"+row.valorServicio * diasDif +"</td>";
                        tablaFactura += "</tr>";

                    }

                    ViewBag.codigoReserva = codigoReserva;
                    ViewBag.fechaFin = fechaFin.ToString().Substring(0, 10);
                   

                    Models.DTO.Empresa lista2 = modelMantenedor.obtenerEmpresaPorId(empresaId);

                    string nombreEmpresa = lista2.razonSocial;
                    string rutEmpresa = lista2.rutEmpresa +"-"+lista2.dvRutEmpresa;
                    string correoEmpresa = lista2.correoEmpresa;
                    string direccionEmpresa = lista2.direccionEmpresa;
                    string telefono = lista2.telefonoEmpresa.ToString();

                    ViewBag.nombreEmpresa = nombreEmpresa;
                    ViewBag.rutEmpresa = rutEmpresa;
                    ViewBag.correoEmpresa = correoEmpresa;
                    ViewBag.direccionEmpresa = direccionEmpresa;
                    ViewBag.telefonoEmpresa = telefono;

                    int descuento = 0;

                    var listaDetalleComedores = modelMantenedor.obtenerDetalleComedoresByHuespedId(huespedId);
                    foreach (var row2 in listaDetalleComedores)
                    {
                        switch (valorServicio)
                        {
                            case 25000:
                                descuento = 0;
                                break;
                            case 35000:
                                if (row2.tipoMenu == 1)
                                {
                                    descuento = descuento + row2.valorMenu;
                                }
                                break;
                            case 45000:
                                if (row2.tipoMenu == 1)
                                {
                                    descuento = descuento + row2.valorMenu;
                                }
                                else if (row2.tipoMenu == 2) {
                                    descuento = descuento + row2.valorMenu;
                                }
                                else
                                {


                                }
                                break;
                            case 55000:
                                if (row2.tipoMenu == 1)
                                {
                                    descuento = descuento + row2.valorMenu;
                                }
                                else if (row2.tipoMenu == 2)
                                {
                                    descuento = descuento + row2.valorMenu;
                                }
                                else
                                {

                                    descuento = descuento + row2.valorMenu;
                                }
                                break;

                        }



                        tablaFactura += "<tr>";
                        tablaFactura += "<td>" + row2.nombreMenu + "</td>";
                        tablaFactura += "<td>1</td>";
                        tablaFactura += "<td>" + row2.valorMenu + "</td>";
                        tablaFactura += "</tr>";
                        sumaTotal = sumaTotal + row2.valorMenu;
                    }
                    ViewBag.TotalFactura = sumaTotal;
                    ViewBag.TablaFacture = tablaFactura;
                    int totalDescuento = sumaTotal - descuento;
                    ViewBag.totalDescuentos = totalDescuento;
                    ViewBag.Descuento = descuento;

                    ViewBag.totalFinal = (totalDescuento * 0.19) + totalDescuento;


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

        public ActionResult Asignar()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Revisar";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    ////var lista = modelMantenedor.obtenerHabitaciones();
                    
                    var lista = modelMantenedor.obtenerComedoresConFecha();
                    var lista2 = modelMantenedor.obtenerComedor();
                    string html = "<table class='separador'><thead></thead><tbody>";

                    html += "<tr>";

                    int contador = 0;
                    foreach (var comedor in lista2)
                    {

                        contador++;



                        if (contador > 5)
                        {
                            html += "</tr><tr>";
                            contador = 1;
                        }

                        if (comedor.estadoComedor == 1)
                        {
                      
                                html += "<td><div id='" + comedor.nombreComedor + "' class='disponible' onclick='cambiarEstados(this," + comedor.idComedor + ",\"" + comedor.nombreComedor + "\")'><span><center>" + comedor.nombreComedor + "<br /><span>(DISPONIBLE)</span><br /><span>Capacidad: " + comedor.capacidadComedor + " Personas</span></center></span></div></td>";
                            

                           
                        }
                        else
                        {

                            foreach(var comedor2 in lista) {

                                if (comedor.nombreComedor == comedor2.nombreComedor) {

                                    if (comedor2.estadoUsado == 0)
                                    {
                                        html += "<td><div id='" + comedor.nombreComedor + "' class='ocupado' onclick='cambiarEstados(this," + comedor.idComedor + ",\"" + comedor.nombreComedor + "\")'><b><span><center>" + comedor2.nombreComedor + "</span><br /><span>(OCUPADO)</span><b/><br /><span>" + comedor2.nombreHuesped + " " + comedor2.apellidoHuesped + "</span><br /><span>" + comedor2.nombreEmpresa + "</span><br /><span>" + comedor2.fechaInicio + "</span><br /><button class='btn blue' onclick='liberarMesa(" + comedor.idComedor + ",\"" + comedor2.nombreComedor + "\")' >Liberar Comedor</button> </center></div></td>";
                                    }

                                }

                                
                            }

                           // if (comedor.estadoUsado == 0) {
                               
                            //}
                            
                        }


                    }
                    html += "</tr>";

                    html += "</tbody></table>";
                    ViewBag.Comedores = html;

                    var listaMenus = modelMantenedor.obtenerMenu();

                    string selectMenu = "";
                    string estiloMenu = "";
                    selectMenu += "<select id='menuId' class='form-control' >";
                    selectMenu += "<option value='0' selected>Seleccione Un Menu....</option>";
                    foreach (var menu in listaMenus) {

                        switch (menu.estiloMenu) {
                            case 1:
                                estiloMenu = "VEGETARIANO";
                                break;
                            case 2:
                                estiloMenu = "ESPECIAL";
                                break;
                            case 3:
                                estiloMenu = "EJECUTIVO";
                                break;

                        }


                        selectMenu += "<option value="+menu.idMenu+">"+menu.nombreMenu+" - "+ estiloMenu + "</option>";
                    }
                    selectMenu += "</select>";

                    ViewBag.SelectMenu = selectMenu;


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

        public ActionResult VerActividad() {

            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Ver Actividad";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerReservas();
                    string html = "";
                    foreach (var servicio in lista)
                    {
                        html += "<tr>";
                        html += "<td>" + servicio.codigoServicio + "</td>";
                        html += "<td>" + servicio.nombreHuesped + "</td>";
                        html += "<td>" + servicio.EmpresaNombre + "</td>";
                        html += "<td><a  class='btn btn-warning'  href='" + Url.Action("TimeLine", "Servicio", new RouteValueDictionary(new { codigoServicio = servicio.codigoServicio })) + "'>Ver Detalle</a><a  class='btn btn-success'  href='" + Url.Action("Facturacion", "Servicio", new RouteValueDictionary(new { codigoServicio = servicio.codigoServicio })) + "'>Ver Factura</a></td>";
                       // html += "<td><button class='btn btn-warning' onclick='verDetalleActividad(" + servicio.codigoServicio + ")' >Ver Detalle</button></td>";
                        //html += "<button class='btn btn-success' onclick='EliminarEmpleado(" + empleado.empleadoId + ", \"" + empleado.nombreEmpleado + " " + empleado.apellidoEmpleado + "\")' >Eliminar</button></td>";
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

        public ActionResult TimeLine(int codigoServicio) {

            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Time Line";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    var modelMantenedor = new Models.MantenedorModel();

                    var lista = modelMantenedor.obtenerCodigoServicioByCode(codigoServicio);

                    int huespedId = 0;
                    string huespedNombre = "";
                    string htmlTimeLine = "";
                    string htmlTimeLineStart = "";
                    string htmlTimeLineEnd = "";
                    string htmlTimeLineHabitacion = "";
                    string htmlTimeLineComedores = "";


                    foreach (var servicio in lista)
                    {
                        htmlTimeLineStart += "<li class='timeline-yellow'>";
                        htmlTimeLineStart += "<div class='timeline-time'>";
                        htmlTimeLineStart += "<span class='date'></span><span class='time' style='font-size: 16px !important;'>"+servicio.fechaInicio+"</span></div>";
                        htmlTimeLineStart += "<div class='timeline-icon'><i class='fa fa-rss'></i></div>";
                        htmlTimeLineStart += "<div class='timeline-body'><h2>Fecha de Reserva de Servicios</h2>";
                        htmlTimeLineStart += "<div class='timeline-content'>Empresa "+ servicio.empresaNombre+" Solicito Servicios al hostal";
                        htmlTimeLineStart += "</div></div></li>";

                        if (servicio.habitacionNombre != null) {
                            htmlTimeLineHabitacion += "<li class='timeline-blue'>";
                            htmlTimeLineHabitacion += "<div class='timeline-time'>";
                            htmlTimeLineHabitacion += "<span class='date'></span><span class='time' style='font-size: 16px !important;'>" + servicio.fechaIngresoHabitacion + "</span></div>";
                            htmlTimeLineHabitacion += "<div class='timeline-icon'><i class='fa fa-rss'></i></div>";
                            htmlTimeLineHabitacion += "<div class='timeline-body'><h2>Asignacion de Habitacion En Hostal</h2>";
                            htmlTimeLineHabitacion += "<div class='timeline-content'> Huesped " + servicio.huespedNombre + " hizo check in y Se le ha asigando la habitacion " + servicio.habitacionNombre + " ";
                            htmlTimeLineHabitacion += "</div></div></li>";
                        }

                       

                        
                        htmlTimeLineEnd += "<li class='timeline-red'>";
                        htmlTimeLineEnd += "<div class='timeline-time'>";
                        htmlTimeLineEnd += "<span class='date'></span><span class='time' style='font-size: 16px !important;'>" + servicio.fechaFin + "</span></div>";
                        htmlTimeLineEnd += "<div class='timeline-icon'><i class='fa fa-rss'></i></div>";
                        htmlTimeLineEnd += "<div class='timeline-body'><h2>Fecha de Check Out</h2>";
                        htmlTimeLineEnd += "<div class='timeline-content'>Huesped "+servicio.huespedNombre+" debe hacer el check out en la Hostal.";
                        htmlTimeLineEnd += "</div></div></li>";

                        huespedId = servicio.huespedId;
                        huespedNombre = servicio.huespedNombre;
                    }

                    var listaDetalleComedores = modelMantenedor.obtenerDetalleComedoresByHuespedId(huespedId);
                    foreach (var row2 in listaDetalleComedores) {
                        htmlTimeLineComedores += "<li class='timeline-green'>";
                        htmlTimeLineComedores += "<div class='timeline-time'>";
                        htmlTimeLineComedores += "<span class='date'></span><span class='time' style='font-size: 16px !important;'>" + row2.menuFechaSolicitud + "</span></div>";
                        htmlTimeLineComedores += "<div class='timeline-icon'><i class='fa fa-rss'></i></div>";
                        htmlTimeLineComedores += "<div class='timeline-body'><h2>Servicios de Comedor</h2>";
                        htmlTimeLineComedores += "<div class='timeline-content'>Acceso del huesped "+ huespedNombre + " A los Servicios de Comedor Accedio a "+row2.nombreMenu+"  ";
                        htmlTimeLineComedores += "</div></div></li>";
                    }


                    htmlTimeLine += htmlTimeLineStart +" "+ htmlTimeLineHabitacion + " "+ htmlTimeLineComedores + " " + htmlTimeLineEnd;
                     
                    ViewBag.TimeLine = htmlTimeLine;

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
        public ActionResult IngresarDetalleDeServicio(string usuariosHuesped, int servicios, int codigoServicio, string fechaInicio, string fechaFin)
        {
            var modelMantenedor = new Models.MantenedorModel();

            string[] words = usuariosHuesped.Split(',');
           


            int valorServicio = 1;
            switch (servicios)
            {
                case 1:
                    valorServicio = 25000;
                    break;
                case 2:
                    valorServicio = 35000;
                    break;
                case 3:
                    valorServicio = 45000;
                    break;
                case 4:
                    valorServicio = 55000;
                    break;

            }

            foreach (var uid in words)
            {
                string nuevoCodigoReserva = codigoServicio.ToString() + "" + uid.ToString();

                if (modelMantenedor.IngresarDetalleDeServicio(servicios, Convert.ToInt32(uid), valorServicio, Convert.ToInt32(nuevoCodigoReserva), fechaInicio, fechaFin))
                {
                   var listaH = modelMantenedor.obtenerHuespedPorId(Convert.ToInt32(uid));

                    foreach (var row in listaH) {
                        Enviar_Email_Reserva(row.nombreHuesped+" "+row.apellidoHuesped, Convert.ToInt32(nuevoCodigoReserva), row.correoHuesped);
                    }
                    
                   
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar El detalle de Servicios..." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);




        }


        [HttpPost]
        public ActionResult obtenerSelectHuespedes(int empresaId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            var listaSelectHuesped = modelMantenedor.obtenerHuespedPorEmpresaId(empresaId);
            string htmlSelectHuesped = "";

            htmlSelectHuesped += "<select id='huespedSelect'  class='form-control'>";

            if (listaSelectHuesped.Count() == 0)
            {
                htmlSelectHuesped += "<option value='0'>EMPRESA NO POSEE HUESPED</option>";
            }
            else {
                foreach (var huespedEmpresa in listaSelectHuesped)
                {
                    htmlSelectHuesped += "<option value='" + huespedEmpresa.idHuesped + "'>" + huespedEmpresa.nombreHuesped + "</option>";
                    
                }
            }
           
            htmlSelectHuesped += "</select>";

            return Json(new { selectHuespedes = htmlSelectHuesped }, JsonRequestBehavior.AllowGet);

        }

        //ingresarDetalleHabitacion
      

        [HttpPost]
        public ActionResult ingresarDetalleHabitacion(int empresaId, int huespedId, int habitacionId, int habitacionValor)
        {
            var modelMantenedor = new Models.MantenedorModel();
            int estadoHabitacionId = 2;
       
            //esto crea el campo empresaId  
            int empleadoId = SessionHandler.UsuarioId;
            

            Models.DTO.Empleados empleado = modelMantenedor.obtenerEmpleadoPorUsuarioId(empleadoId);


            if (modelMantenedor.IngresarDetalleHabitacion( empresaId,  huespedId,  habitacionId,  habitacionValor,  estadoHabitacionId, empleado.empleadoId))
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

        public void Enviar_Email_Reserva(string usuario, int codigoReserva, string email)
        {

            string subject = "Codigo de Reserva";
            string body = "";
            body = "Estimado Huesped, este es el codigo de reserva que debe presentar al momento de realizar su check in ";
            body += "<table><thead><th></th></thead><tbody><tr><td><b>Username:</b></td><td>'" + usuario + "'</td></tr><tr><td><b>Codigo Reserva:</b></td><td>'" + codigoReserva + "'</td></tr></tbody></table>";
            string footer = "Mensaje Auto generado por Hostal Doña Clarita";

            Enviar_Email(subject, email.ToString(), body, footer);


        }

       
        [HttpPost]
        public ActionResult obtenerServiciosPorCodigo(int codigoReserva)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.DetalleServicio detalleServicio = modelMantenedor.obtenerDetalleServicioPorCodigo(codigoReserva);

            return Json(new { detalleServicioId = detalleServicio.detalleServicioId,
                detalleServicioValor = detalleServicio.detalleServicioValor,
                servicioId = detalleServicio.servicioId,
                nombreHuesped = detalleServicio.nombreHuesped,
                huespedId = detalleServicio.huespedId,
                apellidoHuesped = detalleServicio.apellidoHuesped,
                correoHuesped = detalleServicio.correoHuesped,
                empresaId = detalleServicio.empresaId,
                nombreEmpresa = detalleServicio.nombreEmpresa }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ingresarDetalleComedor(int huespedId, int comedorId, int menuId)
        {
            var modelMantenedor = new Models.MantenedorModel();
           // int estadoHabitacionId = 2;

            //esto crea el campo empresaId  
            int empleadoId = SessionHandler.UsuarioId;


            Models.DTO.Empleados empleado = modelMantenedor.obtenerEmpleadoPorUsuarioId(empleadoId);


            if (modelMantenedor.IngresarDetalleComedor(empleado.empleadoId,huespedId,comedorId,menuId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de asignar el comedor..." }, JsonRequestBehavior.AllowGet);
            }
           

        }


        [HttpPost]
        public ActionResult liberarComedor(int comedorId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.cambiarEstadoComedor(comedorId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Liberar el comedor..." }, JsonRequestBehavior.AllowGet);
            }



        }

        [HttpPost]
        public ActionResult liberarHabitacion(int habitacionId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.cambiarEstadoHabitacion(habitacionId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Liberar la habitacion..." }, JsonRequestBehavior.AllowGet);
            }



        }
    }
}