using api_transacciones.Models;
using ApiTrans.DTO;
//using Microsoft.EntityFrameworkCore;


namespace ApiTrans.Services {
    public class DonacionService {
        private readonly ApplicationDbContext _context;
        public DonacionService(ApplicationDbContext context){
            _context = context;
        }
        public List<DonacionDTO> consulta(){
                var AllDonaciones = _context.Donaciones
                .Select(dto => new DonacionDTO
                            {
                                Id = dto.Id,
                                IdDonador = dto.IdDonador,
                                IdPedido = dto.IdPedido,
                                Cantidad = dto.Cantidad,
                                Idioma = dto.Idioma,
                                Fecha = dto.Fecha
                            })
                            .ToList();
            return AllDonaciones;
        
    }
    }
}