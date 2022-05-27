using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Middlewares
{
    public static class CustomFileExtesion
    {
        //public static void UseCustomStaticFiles(this IApplicationBuilder app)
        //{
        //    app.UseStaticFiles(new StaticFileOptions()
        //    {
        //        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
        //        RequestPath = "/content"
        //    });
        //}
    }
}
