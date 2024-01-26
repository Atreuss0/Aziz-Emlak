using System.ComponentModel.DataAnnotations.Schema;

namespace Aziz_Emlak.Models
{
    public class Resim
    {

        public int id { get; set; }
        public string Name { get; set; }
        public int Portfoyid { get; set; }
        
        public portfoy Portfoy { get; set; }




    }
}
