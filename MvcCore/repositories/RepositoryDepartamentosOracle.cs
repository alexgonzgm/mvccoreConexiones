using MvcCore.Data;
using MvcCore.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.repositories
{
    public class RepositoryDepartamentosOracle : IRepositoryDepartamentos
    {
        OracleDataAdapter adapter;
        DataTable table;
        OracleCommandBuilder builder;

        public RepositoryDepartamentosOracle(string cadenaoracle)
        {
            this.adapter = new OracleDataAdapter("select * from dept", cadenaoracle);
            this.builder = new OracleCommandBuilder(this.adapter);
            this.table = new DataTable();
            this.adapter.Fill(this.table);
        }
        public Departamento BuscatDepartamento(int iddepartamento)
        {
            var consulta = from datos in this.table.AsEnumerable()
                           where datos.Field<int>("DEPT_NO") == iddepartamento
                           select new Departamento
                           {
                               IdDepartamento = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<string>("DNOMBRE"),
                               Localidad = datos.Field<string>("LOC")
                           };
            return consulta.FirstOrDefault();
        }

        private DataRow GetDataRowId(int Iddept)
        {
            DataRow fila = this.table.AsEnumerable().Where(x => x.Field<int>("DEPT_NO") == Iddept).FirstOrDefault();
            return fila;
                
        }
        public void DeleteDepartamento(int iddepartamento)
        {
            //PARA ELIMMINAR DEBEMOS HACERLO SOBRE EL OBJETO DATATABLE 
            //DEBEMOS BUSCAR LA FILA QUE CORRESPONDA CON EL ID , LA FILA TIENE UN METODO DELETE
            // QUE MARCARA EN LA TABLA EL VALOR PARA ELIMINAR ,
            // POSTERIORMENTE EL ADAPTADOR A LA IGUAL QUE TIENE UN  METODO PARA TRAER LOS DATOS (FILL)
            // TENEMOS UN METODO PARA AUTOMATIZAR LOS CAMBIOS (Update)
            DataRow row = this.GetDataRowId(iddepartamento);
            row.Delete();
            this.adapter.Update(this.table);
            this.table.AcceptChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.table.AsEnumerable()
                           select new Departamento
                           {
                               IdDepartamento = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<string>("DNOMBRE"),
                               Localidad = datos.Field<string>("LOC")
                           };
            return consulta.ToList();
        }

        public void InsertarDepartamento(int iddepartametno, string nombre, string localidad)
        {
            DataRow row = this.table.NewRow();
            row["DEPT_NO"] = iddepartametno;
            row["DNOMBRE"] = nombre;
            row["LOC"] = localidad;
            this.table.Rows.Add(row);
            this.adapter.Update(this.table);
            this.table.AcceptChanges();
        }

        public void UpdateDepartamento(int iddepartamento, string nombre, string localidad)
        {
            DataRow row = this.GetDataRowId(iddepartamento);
            row["DNOMBRE"] = nombre;
            row["LOC"] = localidad;
            this.adapter.Update(this.table);
            this.table.AcceptChanges();
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
