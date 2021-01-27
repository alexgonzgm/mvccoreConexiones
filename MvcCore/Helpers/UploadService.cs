using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class UploadService
    {
        private PathProvider provider;
        public string path { get; set; }
        public UploadService(PathProvider provider)
        {
            this.provider = provider;
        }
        public async Task<String> UploadFileAsync(IFormFile fichero , Folders folders)
        {
            string filename = fichero.FileName;
            this.path = this.provider.MapPath(filename, folders);
            using (var stream = new FileStream(path,FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            return path;
        }
    }
}
