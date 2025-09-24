namespace Cobros.Crypto.Interface
{
    interface IGestorBinance
    {
        Task<string> ObtenerPago(dynamic json);

        Task<string> RealizarPago(dynamic json);

        Task<string> GenerarUrlPago(dynamic json);
    }
}