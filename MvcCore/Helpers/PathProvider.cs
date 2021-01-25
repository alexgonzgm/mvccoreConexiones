﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public enum Folders
    {
        Images = 0, Documents = 1
    }
    public class PathProvider
    {
        private IWebHostEnvironment environment;
        public PathProvider(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        //METDO PARA DEVOLVER RUTAS DE FICHEROS 
        public string MapPath(string filename , Folders folder)
        {
            string carpeta = ""; //folder.ToString(); //documents , Images 
            if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }
            else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            string path = Path.Combine(this.environment.WebRootPath, carpeta, filename);
            return path;
        }
    }
}
