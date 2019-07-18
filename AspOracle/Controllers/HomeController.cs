using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspOracle.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            //Models.Resources.Entities2 db = new Models.Resources.Entities2();

            //EMPRESA e = new EMPRESA();
            //e.ID_EMPRESA = 99999;
            //e.RAZON_SOCIAL = "CENCOSUD SA";
            //e.RUT_EMPRESA = "96459389";
            //e.DV_RUT_EMPRESA = "0";
            //e.CORREO_EMPRESA = "cencosud@gmail.com";
            //e.DIRECCION_EMPRESA = "test";
            //e.TELEFONO = 8272763;
            //e.NOMBRE_REPRESENTANTE = "NICOLAS DAVID";
            //e.APELLIDO_REPRESENTANTE = "SEPULVEDA ALVEAR";

            //db.EMPRESA.Add(e);

            //db.SaveChanges();
            return View();

        }
        public ActionResult Somos()
        {
            
            return View();

        }
        public ActionResult Servicios()
        {

            return View();

        }
        public ActionResult Contacto()
        {

            return View();

        }

        // GET: Register
        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]
        public ActionResult ingresarEmpresa(string razon_social, string rut_empresa, string correo, string direccion, int telefono, string nombreRep, string apellidoRep)
        {
            var modelMantenedor = new Models.MantenedorModel();

            string rut_nuevo = rut_empresa.Replace(".","").Substring(0, rut_empresa.Replace(".", "").Length - 2);
            string dv_rut_empresa = rut_empresa.Replace(".", "").Substring(rut_empresa.Replace(".", "").Length - 1, 1);
            char dv_rut = Convert.ToChar(dv_rut_empresa);

            if (modelMantenedor.ExisteEmpresa(razon_social))
            {
                return Json(new { response = "error", message = "la Empresa " + razon_social + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }
            if (modelMantenedor.ExisteEmpresa("", rut_nuevo))
            {
                return Json(new { response = "error", message = "la Empresa con RUT " + rut_empresa + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarEmpresas(razon_social, rut_nuevo, dv_rut, correo, direccion, telefono, nombreRep, apellidoRep))
            {
                Models.DTO.Empresa empresa = modelMantenedor.obtenerEmpresaPorRut(rut_nuevo);

                Random r = new Random();
                int aleatorio = r.Next(11111111, 99999999);

      

                if (modelMantenedor.IngresarUsuario(empresa.nombreRepEmpresa, empresa.apellidoRepEmpresa, empresa.correoEmpresa, aleatorio.ToString(), empresa.idEmpresa))
                {
       
                   
                    Enviar_Email_Password(empresa.correoEmpresa, aleatorio.ToString(), empresa.correoEmpresa);
                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
                }


                
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

    

    }
}