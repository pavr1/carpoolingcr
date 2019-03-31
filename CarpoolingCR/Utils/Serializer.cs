using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CarpoolingCR.Utils
{
    public class Serializer
    {
        public static string Serialize(object obj)
        {
            var serializer = new JavaScriptSerializer();
            var serializedObj = serializer.Serialize(obj);

            serializer = null;

            return serializedObj;
        }

        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}