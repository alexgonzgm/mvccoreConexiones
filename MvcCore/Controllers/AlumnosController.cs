using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.repositories;

namespace MvcCore.Controllers
{
    public class AlumnosController : Controller
    {
        private RepositoryAlumnos repository;
        public AlumnosController(RepositoryAlumnos repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            List<Alumno> alumnos = this.repository.GetAlumnos();
            return View(alumnos);
        }
        public IActionResult Details(int idalumno)
        {
            Alumno alumno = this.repository.BuscarAlumno(idalumno);
            return View(alumno);
        }
        public IActionResult Delete(int idalumno)
        {
            this.repository.EliminarAlumno(idalumno);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            this.repository.InsertarAlumno(alumno.IdAlumno, alumno.Nombre, alumno.Apellidos, alumno.Nota);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int idalumno)
        {
            Alumno alumno = this.repository.BuscarAlumno(idalumno);
            return View(alumno);
        }
        [HttpPost]
        public IActionResult Edit(Alumno alumno)
        {
            this.repository.UpdateAlumno(alumno.IdAlumno, alumno.Nombre, alumno.Apellidos, alumno.Nota);
            return RedirectToAction("Index");
        }
    }
}