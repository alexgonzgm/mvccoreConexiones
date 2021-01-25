using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.repositories
{   
    public class RepositoryAlumnos
    {
        private PathProvider provider;
        private XDocument docxml;
        private string path;
        public RepositoryAlumnos(PathProvider provider)
        {
            this.provider = provider;
            this.path = this.provider.MapPath("alumnos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Apellidos = datos.Element("apellidos").Value,
                               Nombre = datos.Element("nombre").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.ToList();
        }

        public Alumno BuscarAlumno(int idalumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Apellidos = datos.Element("apellidos").Value,
                               Nombre = datos.Element("nombre").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.FirstOrDefault();
        }
        public void EliminarAlumno(int idalumno)
        {
            var consulta = from datos in docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select datos;
            XElement elemetnAlumno = consulta.FirstOrDefault();
            elemetnAlumno.Remove();
            this.docxml.Save(this.path);
        }
        public void InsertarAlumno(int idalumno , string nombre , string apellidos , int nota)
        {
            XElement elemnetAlumno = new XElement("alumno");
            XElement elementIdAlumno = new XElement("idalumno", idalumno);
            XElement elementNombre = new XElement("nombre", nombre);
            XElement elementApellidos = new XElement("apellidos", apellidos);
            XElement elementNota = new XElement("nota", nota);

            elemnetAlumno.Add(elementIdAlumno);
            elemnetAlumno.Add(elementNombre);
            elemnetAlumno.Add(elementApellidos);
            elemnetAlumno.Add(elementNota);

            //EL XML ELEMNT DEBEMOS AGREGARLO AL DOCUMENTO Y EN LA ERIQUETA QUE CORRESPONDA 
            this.docxml.Element("alumnos").Add(elemnetAlumno);
            this.docxml.Save(this.path);

        }
        public void UpdateAlumno(int idalumno , string nombre , string apellidos , int nota)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select datos;
            XElement elemnetAlumno = consulta.FirstOrDefault();
            elemnetAlumno.Element("nombre").Value = nombre;
            elemnetAlumno.Element("apellidos").Value = apellidos;
            elemnetAlumno.Element("nota").Value = nota.ToString();
            this.docxml.Save(this.path);
        }    
    }
}
