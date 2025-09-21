using Microsoft.AspNetCore.Mvc;
using ApiTrans.Services;
using api_transacciones.Models;
namespace ApiTrans.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AuthCapitulosController : ControllerBase
    {   
        private readonly AuthCapituloService _authCapituloService; 
        public AuthCapitulosController(AuthCapituloService authCapituloService)
        {
            _authCapituloService = authCapituloService;
        }
        [HttpGet("{id_user}/{id_obra}/{capitulo}")]
        public async Task<ActionResult<bool>> VerificarUsuarioObra(Guid id_user, Guid id_obra, int capitulo)
        {   
            await Task.Delay(1);
            return Ok(_authCapituloService.VerificarUsuarioObra(id_user, id_obra, capitulo));
        }

    }
}
