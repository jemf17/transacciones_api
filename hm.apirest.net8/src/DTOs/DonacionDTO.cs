namespace ApiTrans.DTO {
    public class DonacionDTO {
        public int Id { get; set; }
        public Guid IdDonador { get; set; }
        public Guid IdPedido { get; set; }
        public float Cantidad { get; set; }
        //public DateTime Fecha { get; set; }
        public string Idioma { get; set; } = null!;
        public DateOnly Fecha { get; set; }


}}