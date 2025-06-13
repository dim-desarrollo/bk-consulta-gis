using consultas_gis.api.Data;
using consultas_gis.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace consultas_gis.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {

        
        [HttpGet("/consulta")]
        public ActionResult<Response> ObtenerListado()
        {
            var repo = new Repositorio();


            return Ok(new Response
            {
                Status = 200,
                Content = repo.ConsultaPadron()

            });
        }

    }
}
