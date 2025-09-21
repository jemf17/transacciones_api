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
        [HttpPost("verificarAuthCapitulo")]
        public async Task<ActionResult<bool>> VerificarAuthCapitulo([FromBody] dynamic request)
        {
            await Task.Delay(1);
            return Ok(_authCapituloService.VerificarUsuarioObra(request.IdUser, request.IdObra, request.Capitulo));
        }
    }
}
