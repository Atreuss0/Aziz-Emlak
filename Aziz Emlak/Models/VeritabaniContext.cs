using Microsoft.EntityFrameworkCore;

namespace Aziz_Emlak.Models
{
    public class VeritabaniContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Db_Azizemlak;Integrated Security=False;Persist Security Info=False;");
        }


        public DbSet<danisman> danismanlar { get; set;}
        public DbSet<portfoy> portfoyler { get; set;}
        public DbSet<kullanici> Kullanicilar { get; set;}
        public DbSet<mesajlar> mesajlars { get; set;}
        public DbSet<Resim> Resim { get; set;}
  
    }
    //Data Source=P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net;Initial Catalog=Azizemlakdb;Integrated Security=False;Persist Security Info=False;User ID=ph10300230733;Password=05425975912mM#;"
}
