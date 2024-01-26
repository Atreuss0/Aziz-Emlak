namespace Aziz_Emlak.Models
{
    public class mesajlar
    {
        public int Id { get; set; }
        public string? AdiSoyadi { get; set; }
        public string? Eposta { get; set; }
        public string? Gsm { get; set; }
        public string? mesaj { get; set; }

        public DateTime mesajtarihi { get; set; }


    }
}
