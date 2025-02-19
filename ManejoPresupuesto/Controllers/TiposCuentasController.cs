using System.Data.SqlTypes;
using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }

        //private readonly string connectionString;
        //public TiposCuentasController(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetConnectionString("DefaultConnection");
        //}
        public IActionResult Crear()
        {
            //using (var connection = new SqlConnection(connectionString)) { 
            
            //    var query =connection.Query("Select 1").FirstOrDefault();   

            //}


            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);

            }
            tipoCuenta.UsuarioId = 1;

            var yaExisteTipoCuenta =
                await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre),
                    $"El nombre {tipoCuenta.Nombre} ya existe.");
                
                
                return View(tipoCuenta);
            }




            await repositorioTiposCuentas.Crear(tipoCuenta);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(String nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);
            
            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe ");
            }

            return Json(true);

        }
        }
}