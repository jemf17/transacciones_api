using Microsoft.AspNetCore.Mvc;
//using ApiTrans.Services.ContextBilleterasVirtuales;
using ApiTrans.Services;
using ApiTrans.DTO;
// agregar asyncrinia a los controladores
namespace ApiTrans.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase {
        private readonly PedidoService _pedidoService;
        public PedidosController(PedidoService pedidoService){
            _pedidoService = pedidoService;
        }
        [HttpPost("all/",Name = "GetPedidos")]
        public async Task<ActionResult> GetAll() {
            await Task.Delay(1);
            return Ok(_pedidoService.consulta());
        }
        [HttpPost("comuniti/",Name = "GetPedidoComunity")]
        public async Task<ActionResult> GetComunity() {
            await Task.Delay(1);
            var service =  _pedidoService.consultaComunidad();
            return Ok(service);
        }
        [HttpPost("artist/",Name = "GetPedidosArtist")]
        public async Task<ActionResult> GetArtists() {
            await Task.Delay(1);
            var service =  _pedidoService.consultaArtistasTraductor();
            return Ok(service);
        }
        [HttpPost("PedidoId", Name = "GetPedidosByID")]
        public async Task<ActionResult> GetPedidoById([FromBody] dynamic request) {
            await Task.Delay(1);
            if (id == Guid.Empty)
                return NotFound();
            var service =  _pedidoService.consultaPorId(request.id);
            return Ok(service);
        }
        [HttpPost("create", Name = "CreatePedido")]
        public async Task<ActionResult> CreatePedido([FromBody] dynamic data) {
            try {
            await Task.Delay(1);
            if (data == null)
                return BadRequest(new PostResultDTO{
                Mensaje = "Los datos del pedido no pueden ser nulos",
                Estado = 400});
            var service =  _pedidoService.crearPedido(data);
            return Ok(new PostResultDTO
            {
                Mensaje = "Ok",
                Estado = 200
            });
            } catch (Exception ex) {
                var statusCode = ex is ArgumentException ? 400 : 
                        ex is UnauthorizedAccessException ? 401 :
                        ex is KeyNotFoundException ? 404 :
                        500;

                return StatusCode(statusCode, new PostResultDTO
                {
                    Mensaje = ex.Message,
                    Estado = statusCode
                });
            }
        }
    [HttpPut("edit/{id}", Name = "EditPedido")]
    public async Task<ActionResult> EditPedido(Guid id, [FromBody] dynamic data){
        try {
            await Task.Delay(1);
            if (_pedidoService.ExistPedido(id, data.IdArtista)) // IdArtista tiene que ser reemplazado por un uuid auth del id del artista
                return BadRequest(new PostResultDTO{
                Mensaje = "pedido no existe",
                Estado = 400});
            var service =  _pedidoService.EditarPedido(id ,data);
            return Ok(new PostResultDTO
            {
                Mensaje = "Ok",
                Estado = 200
            });
            } catch (Exception ex) {
                var statusCode = ex is ArgumentException ? 400 : 
                        ex is UnauthorizedAccessException ? 401 :
                        ex is KeyNotFoundException ? 404 :
                        500;

                return StatusCode(statusCode, new PostResultDTO
                {
                    Mensaje = ex.Message,
                    Estado = statusCode
                });
            }
        
    }
    }

}