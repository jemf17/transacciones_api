using Cobros.Crypto.Interface;

namespace Cobros.Crypto.Controller
{
    public class GestorBinance : IGestorBinance
    {
        public async Task<string> GenerarUrlPago(dynamic json)
        {
            await Task.Delay(1);
            return "url_pago_generada";
        }

        public async Task<string> ObtenerPago(dynamic json)
        {
            await Task.Delay(1);
            return "pago_obtenido";
        }

        public async Task<string> RealizarPago(dynamic json)
        {
            await Task.Delay(1);
            return "pago_realizado";
        }
    }
}