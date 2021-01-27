using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCore.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        private PathProvider pathProvider;
        private MailService MailService;
        private UploadService uploadService;
        public HomeController(PathProvider pathProvider , MailService MailService, UploadService uploadService)
        {
            this.pathProvider = pathProvider;
            this.MailService = MailService;
            this.uploadService = uploadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EjemploMail(string receptor , string asunto ,string mensaje , IFormFile fichero)
        {

            if (fichero != null)
            {
                string filename = fichero.FileName;
                string path = await this.uploadService.UploadFileAsync(fichero, Folders.Temporal);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fichero.CopyToAsync(stream);
                }
                this.MailService.SendMail(receptor, asunto, mensaje , path);
            }
            else
            {
                this.MailService.SendMail(receptor, asunto, mensaje);
            }

            ViewData["MENSAJE"] = "MENSAJE ENVIADO";
                
            return View();
        }

        public IActionResult CifradoHash()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHash(string contenido, string resultado, string accion)
        {
            //NECESITAMOS TRABAJAR A NIVEL DE BYTE[] 
            //DEBENOS CONVERTIR A BYTE EL CONTENIDO DE LA ENTRADA
            byte[] entrada;
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            SHA1Managed sha = new SHA1Managed();
            // debemos convertir el contenido de la entrasa a bytes
            entrada = encoding.GetBytes(contenido);
            salida = sha.ComputeHash(entrada);
            string res = encoding.GetString(salida);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["RESULTADO"] = res;
            }
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res)
                {
                    ViewData["RESULTADO"] = "<h1>Iguales/h1>" ;
                }
            }
           
            return View();
        }
    }
}
