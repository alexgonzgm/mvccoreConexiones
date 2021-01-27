using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.repositories
{
    public class RepositoryDepartamentosMySql : IRepositoryDepartamentos
    {
        private HospitalContext context;
        public RepositoryDepartamentosMySql(HospitalContext context)
        {
            this.context = context;
        }
        public Departamento BuscatDepartamento(int iddepartamento)
        {
           return this.context.Departamentos
                 .Where(x => x.IdDepartamento == iddepartamento).FirstOrDefault();
            
        }

        public void DeleteDepartamento(int iddepartamento)
        {
            Departamento departamento = this.BuscatDepartamento(iddepartamento);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public void InsertarDepartamento(int iddepartametno, string nombre, string localidad)
        {
            Departamento departamento = new Departamento();
            departamento.IdDepartamento = iddepartametno;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.Departamentos.Add(departamento);
            this.context.SaveChanges();
        }

        public void InsertarDepartamento(int iddepartametno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad)
        {
            Departamento departamento = this.BuscatDepartamento(iddepartamento);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.SaveChanges();
        }

        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
