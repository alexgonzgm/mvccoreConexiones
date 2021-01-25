using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        #region TablaEmpleados

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public List<Empleado> BuscarEmpleadosDepartamentos(List<int> iddepartamentos)
        {
            var consulta = from datos in context.Empleados
                           where iddepartamentos.Contains(datos.IdDepartamento)
                           select datos;
            return consulta.ToList();
        }

        #endregion

        #region TablaDepartamentos
        public Departamento BuscatDepartamento(int iddepartamento)
        {
            return this.context.Departamentos.Where(x => x.IdDepartamento == iddepartamento).FirstOrDefault();
        }

        public void DeleteDepartamento(int iddepartamento)
        {
            Departamento departamento = this.BuscatDepartamento(iddepartamento);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.context.Departamentos
                           select datos;
            return consulta.ToList();

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

        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad)
        {
            Departamento departamento = this.BuscatDepartamento(iddepartamento);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.SaveChanges();
        }

       

        #endregion

    }
}
