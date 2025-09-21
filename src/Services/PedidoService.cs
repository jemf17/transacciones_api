using api_transacciones.Models;
using ApiTrans.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ApiTrans.Services
{
    public class PedidoService
    {
        private ApplicationDbContext _context;
        public PedidoService(ApplicationDbContext context){
            _context = context;
        }
        public List<ViewPedidoDTO> consulta(){
                var AllPedidos = _context.Pedidos
                .Select(pe => new ViewPedidoDTO
                            {
                                IdIdiomaTraducir = pe.IdIdiomaTraducir,
                                IdObra = pe.IdObra,
                                NumeroCap = pe.NumeroCap,
                                Fecha = pe.Fecha,
                                Id = pe.Id,
                            })
                            .OrderBy(pe => pe.Fecha) 
                            .ToList();
            return AllPedidos;

            }
        public List<ViewPedidoDTO> consultaComunidad(){
                var AllPedidos = _context.Pedidos
                .Where(pe => pe.Tipo == "comunidad")
                .Select(pe => new ViewPedidoDTO
                            {
                                IdIdiomaTraducir = pe.IdIdiomaTraducir,
                                IdObra = pe.IdObra,
                                NumeroCap = pe.NumeroCap,
                                Fecha = pe.Fecha,
                                Id = pe.Id,
                            })
                            .OrderByDescending(pe => _context.Donaciones.Where(x => x.IdPedido == pe.Id).Sum(x => (float?)x.Cantidad ?? 0))
                            .OrderBy(pe => pe.Fecha) 
                            .ToList();
            return AllPedidos;

            
            }
            public List<ViewPedidoDTO> consultaArtistasTraductor(){
                var AllPedidos = _context.Pedidos
                .Where(pe => pe.Tipo == "traduccion")
                .Select(pe => new ViewPedidoDTO
                            {
                                IdIdiomaTraducir = pe.IdIdiomaTraducir,
                                IdObra = pe.IdObra,
                                NumeroCap = pe.NumeroCap,
                                Fecha = pe.Fecha,
                                Id = pe.Id,
                            })
                            .OrderBy(pe => pe.Fecha) 
                            .ToList();
            return AllPedidos;
            }
        public PedidoDTO consultaPorId(Guid id){
                var Pedido = _context.Pedidos
               .Where(pe => pe.Id == id)
               .Select(pe => new PedidoDTO
                            {
                                IdIdiomaTraducir = pe.IdIdiomaTraducir,
                                IdObra = pe.IdObra,
                                NumeroCap = pe.NumeroCap,
                                IdArtista = pe.IdArtista,
                                Tipo = pe.Tipo,
                                Fecha = pe.Fecha,
                                Panel = pe.Panel,
                                Texto = pe.Texto,
                                Id = pe.Id,
                                Estado = pe.Estado,
                                paga = pe.Tipo == "comunidad" 
                                ? _context.Donaciones.Where(x => x.IdPedido == pe.Id).Sum(x => (float?)x.Cantidad ?? 0) : 0
                            })
                            .FirstOrDefault() ?? new PedidoDTO();
            return Pedido;

            }
        public async Task crearPedido(dynamic data){
            try {
            var pedido = new Pedido
            {
                IdIdiomaTraducir = data.IdIdiomaTraducir,
                IdObra = data.IdObra,
                NumeroCap = data.NumeroCap,
                IdArtista = data.IdArtista,
                Tipo = data.Tipo,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Panel = data.Panel,
                Texto = data.Texto,
                Estado = "libre",
            };
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el pedido", ex);
            }
        }
        public bool ExistPedido(Guid id, Guid id_artista){
            var exists = _context.Pedidos.Any(p => p.Id == id && p.IdArtista == id_artista);
            return exists;
        }
        public async Task EditarPedido(Guid id, dynamic data){
            try {
            var pedido = await _context.Pedidos.FindAsync(id);
            pedido.IdIdiomaTraducir = data.IdIdiomaTraducir;
            pedido.IdObra = pedido.IdObra;
            pedido.NumeroCap = pedido.NumeroCap;
            pedido.IdArtista = pedido.IdArtista;
            pedido.Tipo = data.Tipo;
            pedido.Fecha = pedido.Fecha;
            pedido.Panel = data.Panel;
            pedido.Texto = data.Texto;
            pedido.Estado = pedido.Estado;
            await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al editar el pedido", ex);
            }
    }
    }
}