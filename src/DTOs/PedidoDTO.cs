namespace ApiTrans.DTO {

    public class PedidoDTO {
    public string IdIdiomaTraducir { get; set; } = null!;

    public Guid IdObra { get; set; }

    public int NumeroCap { get; set; }

    public Guid IdArtista { get; set; }

    public string Tipo { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string? Panel { get; set; }

    public string? Texto { get; set; }

    public Guid Id { get; set; }

    public string Estado { get; set; } = null!;

    public float paga { get; set; } = 0;

    public float total { get; set; } = 0;
    }}
