using consultas_gis.api.Data;
using consultas_gis.api.Models;
using consultas_gis.DTOs;
using consultas_gis.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace consultas_gis.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {

        //[Authorize]
        [HttpGet("/consulta")]
        public ActionResult<List<ConsultaDTO>> ObtenerListado()
        {
            var repo = new Repositorio();
            
            Console.WriteLine("Consulta realizada");
            var resu = repo.ConsultaPadron();
            Console.WriteLine($"La cantidad de rows son {resu.Count}");
            return Ok(Mapper.ConsultaModelToConsultaDTO(resu));
        }

        [HttpGet("/consulta-prueba")]
        public ActionResult<string> ObtenerPrueba()
        {
            var repo = new Repositorio();


            return Ok($"ambientes deploy {new DateTime()}");
        }

    }
}
