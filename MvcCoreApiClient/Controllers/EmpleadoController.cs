using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClient.Models;
using MvcCoreApiClient.Services;

namespace MvcCoreApiClient.Controllers
{
    public class EmpleadoController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadoController(ServiceEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> emp = await this.service.GetEmpleadoAsync();
            return View(emp);
        }

        public async Task<IActionResult> Details(int id)
        {
            Empleado emp = await this.service.FindEmpleadoAsync(id);
            return View(emp);
        }
    }
}
