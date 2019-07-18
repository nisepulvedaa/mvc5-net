using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspOracle.Models.DTO;

namespace AspOracle.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            SessionHandler.Logged = false;
            SessionHandler.Usuario = "";
            SessionHandler.UsuarioId = -1;
            SessionHandler.Mail = "";
            SessionHandler.Perfil = -1;
            SessionHandler.EmpresaId = -1;


            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {

            var modelLogin = new Models.LoginModel();
            var modelMantenedores = new Models.MantenedorModel();

            
            Usuario usuario = modelLogin.login(user, pass);

            if (usuario.idUsuario != 0)
            {

                SessionHandler.Logged = true;
                SessionHandler.Usuario = usuario.nombreUsuario;
                SessionHandler.UsuarioId = usuario.idUsuario;
                SessionHandler.Mail = usuario.userName;
                SessionHandler.Perfil = usuario.perfil.idPerfil;
                SessionHandler.EmpresaId = usuario.empresa.idEmpresa;
                SessionHandler.EmpresaNombre = usuario.empresa.razonSocial;
                SessionHandler.Apellido = usuario.apelldoUsuario;
                bool passEstatus = false;

                if (usuario.estadoPass.ToString() == "1") {
                    passEstatus = true;
                }
                
                SessionHandler.pwdEstado = passEstatus;
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                switch (usuario.idUsuario)
                {
                    case -1:
                        return Json(new { response = "error", message = "Nombre de usuario o password incorrecto" }, JsonRequestBehavior.AllowGet);
                    case -2:
                        return Json(new { response = "error", message = "No se ha podido establecer una conexion con el servidor" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { response = "error", message = "Error Desconocido" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}