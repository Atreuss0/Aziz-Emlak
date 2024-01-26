namespace Aziz_Emlak.Models
{
    public class DanismanImage
    {

        public int id { get; set; }
        public int Tecrube { get; set; }

        public IFormFile DanismanFotgrafi { get; set; }

        public string Role { get; set; }
        public string DanismanAdi { get; set; }
        public string DanismanAciklamasi { get; set; }
        public string DanismanGsm { get; set; }
        public string DanismanEmail { get; set; }
        public string DanismanFace { get; set; }
        public string DanismanInsta { get; set; }
        public string DanismanTwitter { get; set; }
        public string DanismanWp { get; set; }

    }
}
