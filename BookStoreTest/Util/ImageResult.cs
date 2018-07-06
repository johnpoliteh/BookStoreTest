using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreTest.Util
{
    public class ImageResult : ActionResult
    {
        private string path;
        public ImageResult(string path)
        {
            this.path = path;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("<div style='width:100%;text-align:left;'>" +
                "<img style='max-width:300px;' src='" + path + "' /></div>" +
                "<h3> Это модель Экстракции </h3>");
        }

    }
}