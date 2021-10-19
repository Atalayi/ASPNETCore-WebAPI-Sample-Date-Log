using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace SampleLogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHostingEnvironment _env;

        public ValuesController(IHostingEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public void Get()
        {
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            //var directoryPath = Path.Combine(_env.ContentRootPath, "bin");
            var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var path = Path.Combine(directoryPath,"log.txt");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }

            var log = "Date : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "\n" + ControllerContext.ActionDescriptor.ControllerName + "." + ControllerContext.ActionDescriptor.ActionName + "\nIp Number : " + HttpContext.Connection.RemoteIpAddress.ToString() + "\n\n";
            System.IO.File.AppendAllText(path, log);
        }

    }
}
