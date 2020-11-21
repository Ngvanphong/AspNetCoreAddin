using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Extensions
{
    public static class DeleteElement
    {
        public static void DeleteElementByString(this string path, IWebHostEnvironment env)
        {
            try
            {
                string webHost = env.WebRootPath;
                string fullPath = webHost + path;
                File.Delete(fullPath);
            }
            catch { }
        }
    }
}
