using Microsoft.AspNetCore.Mvc;
//using ApiTrans.Services.ContextBilleterasVirtuales;
using ApiTrans.Services;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
using ApiTrans.DTO;
using System.Text.Json; // Para JsonSerializer
using System.Text;
using Sprache;

namespace ApiTrans.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase{
        private readonly NotificacionesService _Notificacion;
        private static List<WebSocket> _sockets = new List<WebSocket>();
        public NotificacionesController(NotificacionesService Notificacion)
        {
            _Notificacion = Notificacion;
        }
    [HttpGet("colawebs/{user}", Name = "WebSNotificacion")]
    public async Task ConnectWebSocket(Guid user)
    {   
        try{
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            _sockets.Add(webSocket);
            await _Notificacion.Echo2(webSocket, user);
        }}
        catch (Exception ex) {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
            
    }
    [HttpGet("getcalaps/{artist}", Name = "GetSolicitudColaboracion")]
    public async Task<ActionResult> GetSolicitudColaboracion(Guid artist) {
            await Task.Delay(1);
            return Ok(_Notificacion.getColaps(artist));
        }
    }

}