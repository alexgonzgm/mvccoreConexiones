using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.repositories
{
    public interface IRepositoryDepartamentos
    {
        List<Departamento> GetDepartamentos();
        Departamento BuscatDepartamento(int iddepartamento);
        void DeleteDepartamento(int iddepartamento);
        void InsertarDepartamento(int iddepartametno, string nombre, string localidad);
        void InsertarDepartamento(int iddepartametno, string nombre, string localidad, string imagen);
        void UpdateDepartamento(int iddepartamento, string nombre, string localidad);
        void UpdateDepartamento(int iddepartamento, string nombre, string localidad, string imagen);

    }
}
