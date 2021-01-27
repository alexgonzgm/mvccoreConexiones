using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.repositories
{
    public class RepositoryDepartamentosXML : IRepositoryDepartamentos
    {
        private PathProvider provider;
        private XDocument docxml;
        private string path;
        public RepositoryDepartamentosXML(PathProvider provider)
        {
            this.provider = provider;
            this.path = this.provider.MapPath("departamentos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in docxml.Descendants("DEPARTAMENTO")
                           select new Departamento
                           {
                               IdDepartamento = int.Parse(datos.Attribute("NUMERO").Value),
                               Nombre = datos.Element("NOMBRE").Value,
                               Localidad = datos.Element("LOCALIDAD").Value
                           };
            return consulta.ToList();
        }
        public Departamento BuscatDepartamento(int iddepartamento)
        {
            var consulta = from datos in docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddepartamento.ToString()
                           select new Departamento
                           {
                               IdDepartamento = int.Parse(datos.Attribute("NUMERO").Value),
                               Nombre = datos.Element("NOMBRE").Value,
                               Localidad = datos.Element("LOCALIDAD").Value
                           };
            return consulta.FirstOrDefault();
        }

        public void DeleteDepartamento(int iddepartamento)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddepartamento.ToString()
                           select datos;
            XElement departamento = consulta.FirstOrDefault();
            departamento.Remove();
            this.docxml.Save(this.path);
        }

        public void InsertarDepartamento(int iddepartametno , string nombre , string localidad)
        {
            XElement nuevoDepartamento = new XElement("DEPARTAMENTO");
            XAttribute numero = new XAttribute("NUMERO", iddepartametno);
            XElement elementNombre = new XElement("NOMBRE", nombre);
            XElement elementLoc = new XElement("LOCALIDAD", localidad);
            nuevoDepartamento.Add(numero);
            nuevoDepartamento.Add(elementNombre);
            nuevoDepartamento.Add(elementLoc);

            this.docxml.Element("DEPARTAMENTOS").Add(nuevoDepartamento);
            this.docxml.Save(this.path);

        }
        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad)
        {
            var cosnulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddepartamento.ToString()
                           select datos;
            XElement departamento = cosnulta.FirstOrDefault();
            departamento.Attribute("NUMERO").Value = iddepartamento.ToString();
            departamento.Element("NOMBRE").Value = nombre;
            departamento.Element("LOCALIDAD").Value = localidad;
            this.docxml.Save(this.path);

        }

        public void InsertarDepartamento(int iddepartametno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
