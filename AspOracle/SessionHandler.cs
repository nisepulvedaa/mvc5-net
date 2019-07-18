using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspOracle
{
    public class SessionHandler
    {
        public static bool Logged
        {
            get
            {
                if (HttpContext.Current.Session["logged"] != null)
                {
                    return (bool)HttpContext.Current.Session["logged"];
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session["logged"] = value;
            }
        }
        public static string Usuario
        {
            get
            {
                return (string)HttpContext.Current.Session["usuario"];
            }
            set
            {
                HttpContext.Current.Session["usuario"] = value;
            }
        }


        public static string Apellido
        {
            get
            {
                return (string)HttpContext.Current.Session["apellido"];
            }
            set
            {
                HttpContext.Current.Session["apellido"] = value;
            }
        }

        public static int UsuarioId
        {
            get
            {
                return (int)HttpContext.Current.Session["usuarioId"];
            }
            set
            {
                HttpContext.Current.Session["usuarioId"] = value;
            }
        }
        public static string Mail
        {
            get
            {
                return (string)HttpContext.Current.Session["mail"];
            }
            set
            {
                HttpContext.Current.Session["mail"] = value;
            }
        }
        public static int Perfil
        {
            get
            {
                return (int)HttpContext.Current.Session["perfil"];
            }
            set
            {
                HttpContext.Current.Session["perfil"] = value;
            }
        }
        public static int EmpresaId
        {
            get
            {
                return (int)HttpContext.Current.Session["empresaId"];
            }
            set
            {
                HttpContext.Current.Session["empresaId"] = value;
            }
        }

        public static string EmpresaNombre
        {
            get
            {
                return (string)HttpContext.Current.Session["empresaNombre"];
            }
            set
            {
                HttpContext.Current.Session["empresaNombre"] = value;
            }
        }

        public static bool pwdEstado
        {
            get
            {
                return (bool)HttpContext.Current.Session["pwdEstado"];
            }
            set
            {
                HttpContext.Current.Session["pwdEstado"] = value;
            }
        }

    }
}