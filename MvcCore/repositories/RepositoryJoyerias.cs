using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.repositories
{
    public class RepositoryJoyerias
    {
        PathProvider PathProvider;
        private XDocument docxml;
        public RepositoryJoyerias(PathProvider PathProvider)
        {
            this.PathProvider = PathProvider;
            string path = this.PathProvider.MapPath("joyerias.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Joyeria> GetJoyerias()
        {
            var consulta = from datos in this.docxml.Descendants("joyeria")
                           select new Joyeria
                           {
                               Nombre = datos.Element("nombrejoyeria").Value
                               ,
                               Direccion = datos.Element("direccion").Value
                               ,
                               Telefono = datos.Element("telf").Value
                               ,
                               Cif = datos.Attribute("cif").Value
                           };
            List<Joyeria> joyerias = consulta.ToList();
            return joyerias;
        }
    }
}
