using Microsoft.AspNetCore.Mvc;
//using ApiTrans.Services.ContextBilleterasVirtuales;
using ApiTrans.Services;
// agregar asyncrinia a los controladores
namespace ApiTrans.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase {
        private readonly PedidoService _pedidoService;
        public PedidosController(PedidoService pedidoService){
            _pedidoService = pedidoService;
        }
        [HttpGet("all/",Name = "GetPedidos")]
        public async Task<ActionResult> GetAll() {
            await Task.Delay(1);
            return Ok(_pedidoService.consulta());
        }
        [HttpGet("comuniti/",Name = "GetPedidoComunity")]
        public async Task<ActionResult> GetComunity() {
            await Task.Delay(1);
            var service =  _pedidoService.consultaComunidad();
            return Ok(service);
        }
        [HttpGet("artist/",Name = "GetPedidosArtist")]
        public async Task<ActionResult> GetArtists() {
            await Task.Delay(1);
            var service =  _pedidoService.consultaArtistasTraductor();
            return Ok(service);
        }
        [HttpGet("{id}", Name = "GetPedidosByID")]
        public async Task<ActionResult> GetPedidoById(Guid id) {
            await Task.Delay(1);
            if (id == Guid.Empty)
                return NotFound();
            var service =  _pedidoService.consultaPorId(id);
            return Ok(service);
        }
    }

}