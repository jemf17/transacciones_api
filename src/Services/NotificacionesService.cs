using api_transacciones.Models;
using ApiTrans.DTO;
using System.Net.WebSockets;
//using ApiTrans.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text;
//using Microsoft.VisualBasic;
using System.Text.Json;
using ApiTrans.Email;
using Resend;
using Npgsql;
using DotNetEnv;
using Npgsql.Internal;



namespace ApiTrans.Services
{
    public class NotificacionesService {
        private readonly IResend _resend;
        private readonly TemplateService _templateService;
        private readonly ApplicationDbContext _context;
        private readonly NpgsqlConnection _socked;
        private readonly string[] _canales;
        private bool _notificationHandler = false; 
        public NotificacionesService(IResend resend, TemplateService templateService, ApplicationDbContext context)
        {
            Env.Load();
            _context = context;
            _resend = resend;
            _templateService = templateService;    
            _canales = ["ColaboradorNotifi"];
            _socked = new NpgsqlConnection($"{Environment.GetEnvironmentVariable("DATABASE_URL")}");
        }
        public async Task SendWelcomeEmail( string templateName, 
        string toEmail, 
        string subject, 
        Dictionary<string, object> parameters)
        {
            var htmlContent = _templateService.GetTemplate(templateName, parameters);

            var message = new EmailMessage
            {
                To = { toEmail },
                Subject = subject,
                HtmlBody = htmlContent
            };
            await _resend.EmailSendAsync(message);
        }
        //Envia una lista de colaboradores que quieren colaborar con cierta obra los que no esten notificados
        public List<ColaboradorDTO> getColaps(Guid artista){
            var GetColaborador =  _context.ObrasArtistas.Join(_context.Obras, c => c.IdObra, o => o.Artista, (c, o) =>
            new {c,o})
                                .Where(x => x.o.Artista == artista && x.c.Notificado == false)
                                .OrderByDescending(t => t.c.Createdat)
                                .Select(x => new ColaboradorDTO{
                                     Colaborador = x.c.IdArtist,
                                    IdObra = x.c.IdObra
                                })
                                .ToList();
            return GetColaborador;
        }
        public async Task Echo2(WebSocket webSocket, Guid user, CancellationToken cancellationToken = default){
            Console.WriteLine($"dentro de la task: {user}");
            await _socked.OpenAsync();
                while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested){
                if (!_notificationHandler) // Evitar suscribirse múltiples veces
                {
                _socked.Notification += async (o,e) => {
                    var json = JsonDocument.Parse(e.Payload);
                    var root = json.RootElement;
                    if (root.TryGetProperty("user", out var userProp))
                    {
                        Guid userFromPayload;
                        if (Guid.TryParse(userProp.GetString(), out userFromPayload))
                        {
                            if (userFromPayload == user)
                            {
                                await Echo(webSocket, e.Payload, e.Channel);
                                //await Task.Delay(5);
                            }
                        }
                    }
                };
                _notificationHandler = true;
                }

            // array de canales

            foreach (var canal in _canales)
            {
                var cmd = new NpgsqlCommand($"LISTEN \"{canal}\"", _socked);
                //var cmd = new NpgsqlCommand($"LISTEN \"ColaboradorNotifi\"", _socked);
                cmd.ExecuteNonQuery();
            }
            await _socked.WaitAsync(); // no me toma ni el wait ni waitasync
        }}
        
        
        private async Task Echo(WebSocket webSocket, string payload,
        string listen,
        CancellationToken cancellationToken = default)
        {
            try
                {
                var payloadObject = JsonSerializer.Deserialize<object>(payload);
                var mensajeEnviar = new
                {
                    tipo = listen,
                    data = payloadObject
                };
                string json = JsonSerializer.Serialize(mensajeEnviar);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                if (webSocket.State == WebSocketState.Open){
                    await webSocket.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar notificación: {ex.Message}");
            }
            }
        }

}