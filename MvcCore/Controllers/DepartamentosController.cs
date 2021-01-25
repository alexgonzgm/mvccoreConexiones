using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.repositories;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repository;
        public DepartamentosController(IRepositoryHospital repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {

            return View(this.repository.GetDepartamentos());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departamento dep)
        {
            this.repository.InsertarDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int iddepartamento)
        {

            return View(this.repository.BuscatDepartamento(iddepartamento));
        }

        public IActionResult Edit(int iddepartamento)
        {

            return View(this.repository.BuscatDepartamento(iddepartamento));
        }
        [HttpPost]
        public IActionResult Edit(Departamento dep)
        {
            this.repository.UpdateDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddepartamento)
        {
            this.repository.DeleteDepartamento(iddepartamento);
            return RedirectToAction("Index");
        }

        public IActionResult SeleccionMultiple()
        {
            List<Departamento> departamentos = this.repository.GetDepartamentos();
            List<Empleado> empleados = this.repository.GetEmpleados();
            ViewData["DEPARTAMENTOS"] = departamentos;
            return View(empleados);

        }
        [HttpPost]
        public IActionResult SeleccionMultiple(List<int> iddepartamentos)
        {
            List<Departamento> departamentos = this.repository.GetDepartamentos();
            ViewData["DEPARTAMENTOS"] = departamentos;

            List<Empleado> empleados = this.repository.BuscarEmpleadosDepartamentos(iddepartamentos);
            return View(empleados);
        }


    }
}