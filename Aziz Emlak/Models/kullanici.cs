using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aziz_Emlak.Models
{
    public class kullanici
    {
 
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        [ForeignKey("danisman")]
        public int danismanid { get;set; }

        public danisman danisman { get; set; }

    }
}
