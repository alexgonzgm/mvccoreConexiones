﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.IO;
using MvcCore.Models;
using MvcCore.repositories;

namespace MvcCore.Controllers
{
    public class JoyeriasController : Controller
    {
        RepositoryJoyerias repo;

        public JoyeriasController(RepositoryJoyerias repository )
        {
            this.repo = repository;
        }

        public IActionResult Index()
        {

            List<Joyeria> joyerias = this.repo.GetJoyerias();
            return View(joyerias);
        }
    }
}
