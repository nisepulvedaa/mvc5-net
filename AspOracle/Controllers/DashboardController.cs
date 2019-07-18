using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspOracle.Helpers;

namespace AspOracle.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Menu";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);


                    string usuarioNombre = SessionHandler.Usuario;
                    string usuarioApellido = SessionHandler.Apellido;
                    string usuarioEmpresa = SessionHandler.EmpresaNombre;
                    int perfilId = SessionHandler.Perfil;
                    string perfil = "";

                    switch (perfilId)
                    {
                        case 1:
                            perfil = "Administrador";
                            break;
                        case 2:
                            perfil = "Empleado";
                            break;
                        case 3:
                            perfil = "Empresa";
                            break;
                        case 4:
                            perfil = "Proveedor";
                            break;

                    }





                    ViewBag.usuarioNombre = usuarioNombre;
                    ViewBag.usuarioApellido = usuarioApellido;
                    ViewBag.usuarioEmpresa = usuarioEmpresa;
                    ViewBag.perfil = perfil;


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
    }
}