namespace ApiTrans.DTO {

    public class ViewPedidoDTO {
    public string IdIdiomaTraducir { get; set; } = null!;
    public Guid IdObra { get; set; }
    public int NumeroCap { get; set; }
    public DateOnly Fecha { get; set; }

    public Guid Id { get; set; }

}}
