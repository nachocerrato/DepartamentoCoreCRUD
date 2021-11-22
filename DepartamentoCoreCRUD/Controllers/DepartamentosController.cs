using DepartamentoCoreCRUD.Data;
using DepartamentoCoreCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartamentoCoreCRUD.Controllers
{
    public class DepartamentosController : Controller
    {
        DepartamentosContext context;

        public DepartamentosController(DepartamentosContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Departamento> departamentos = this.context.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Details(int iddepartamento)
        {
            Departamento departamento = this.context.FindDepartamento(iddepartamento);
            return View(departamento);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int iddepartamento, String nombre, String localidad)
        {
            this.context.InsertDepartamento(iddepartamento, nombre, localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddepartamento)
        {
            Departamento departamento = this.context.FindDepartamento(iddepartamento);
            return View(departamento);
        }

        [HttpPost]
        public IActionResult Edit(int iddepartamento, string nombre, string localidad)
        {
            this.context.UpdateDepartamento(iddepartamento, nombre, localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddepartamento)
        {
            this.context.DeleteDepartamento(iddepartamento);
            return RedirectToAction("Index");
        }
    }
}
