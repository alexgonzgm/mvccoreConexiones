using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.repositories;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repository;
        private PathProvider provider;
        public DepartamentosController(IRepositoryHospital repository , PathProvider provider)
        {
            this.repository = repository;
            this.provider = provider;
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
        public async Task<IActionResult> Create(Departamento dep , IFormFile ficheroimagen)
        {
            string filename = ficheroimagen.FileName;
            string path = this.provider.MapPath(filename, Folders.Images);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await ficheroimagen.CopyToAsync(stream);
            }
            this.repository.InsertarDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad,filename);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int iddepartamento)
        {

            return View(this.repository.BuscatDepartamento(iddepartamento));
        }

        public IActionResult Edit(int iddepartamento )
        {

            return View(this.repository.BuscatDepartamento(iddepartamento));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dep  , IFormFile ficheroimagen)
        {
            string filename = ficheroimagen.FileName;
            string path = this.provider.MapPath(filename, Folders.Images);
            using (var stream = new FileStream(path , FileMode.Create))
            {
                await ficheroimagen.CopyToAsync(stream);
            }
            this.repository.UpdateDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad, filename);
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