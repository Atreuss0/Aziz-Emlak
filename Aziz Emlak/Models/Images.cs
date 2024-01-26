using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aziz_Emlak.Models
{
    public class Images
    {

        public int Id { get; set; }
        public int Portfoyid { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile[] ImageName { get; set; }

        public portfoy Portfoy { get; set; }
    }
}
