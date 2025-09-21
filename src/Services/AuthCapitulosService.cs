using api_transacciones.Models;
//using ApiTrans.DTO;
using Microsoft.EntityFrameworkCore;
namespace ApiTrans.Services
{
    public class AuthCapituloService {
        private readonly ApplicationDbContext _context;

        public AuthCapituloService(ApplicationDbContext context)
         {
             _context = context;
         }
        public bool VerificarUsuarioObra(Guid id_user, Guid id_obra, int capitulo)
        {
            bool verifyCapPay = _context.Capitulos.Any(c => c.Price == 0 && c.IdObra == id_obra && c.Numero == capitulo);
            if (verifyCapPay)
            {
                return true;
            }
            bool authArtist = _context.Obras
            .Join(_context.Colaboladores, o => o.Id, c => c.IdObra, (o, c) => new { o, c })
            .Any(x => (x.o.Artista == id_user || x.c.IdArts== id_user) && x.o.Id == id_obra);
            bool authPayCap = _context.AutorizacionesCapitulosPagos.Any(ac => ac.IdObra == id_obra && ac.IdUser == id_user && ac.Numero == capitulo);
            if (authArtist || authPayCap)
            {
                return true;
            }
            return false;
    }
}
}
