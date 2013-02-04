using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;
using Newtonsoft.Json;


namespace Intertek.WEB.Handlers
{
    /// <summary>
    /// Descripción breve de Ensayo
    /// </summary>
    public class Ensayo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");


                 switch (context.Request["accion"])
            {
                case "eliminar":
                  //  LoadPais(context);
                    break;
                case "loadRol":
                  //  LoadRol(context);
                    break;
                case "periodo":
                  //  LaodPeriodos(context);
                    break;
                case "loadTimeLine":
                  //  LoadTimeLine(context);
                    break;
            }
        }

   

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}