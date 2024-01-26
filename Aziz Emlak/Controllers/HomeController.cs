
using Aziz_Emlak.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList;

namespace Aziz_Emlak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        VeritabaniContext c = new VeritabaniContext();
        public IActionResult Index()
        {          
            var portfoy=c.portfoyler.OrderByDescending(x => x.IlanTarihi).Take(8).ToList();
          
            return View(portfoy);
        }

        public IActionResult Kurumsal()
        {
            return View();
        }
        public IActionResult Portfoyler(int page=1)
        {   

            var p = c.portfoyler.OrderByDescending(x => x.IlanTarihi).ToPagedList(page,6);
            
            return View(p);
        }
        [HttpPost]
        public IActionResult Portfoyler(string? Ilce, string? PortfoyTipi, string? KonutTipi, Boolean? Esyalı, string? Durumu,string? Sıralama)
        {
            if (Ilce=="Hepsi")
            {
                Ilce=null;
            }
            if (PortfoyTipi == "Hepsi")
            {
                PortfoyTipi = null;
            }
            if (KonutTipi == "Hepsi")
            {
                KonutTipi = null;
            }
            if (Durumu == "Hepsi")
            {
                Durumu = null;
            }

            var portfoyler = c.portfoyler
             .Where(p => (Ilce == null || p.Ilce == Ilce)
              && (PortfoyTipi == null || p.PortfoyTipi == PortfoyTipi)
              && (KonutTipi == null || p.KonutTipi == KonutTipi)
              && (Esyalı == null || p.Esyalı == Esyalı)
              && (Durumu == null || p.Durumu == Durumu))
             .ToList();


            if (Sıralama == "PU")
            {
                portfoyler = portfoyler.OrderByDescending(p => p.Fiyati).ToList();
            }
            else if (Sıralama == "UP")
            {
                portfoyler = portfoyler.OrderBy(p => p.Fiyati).ToList();
            }
            else if (Sıralama == "EY")
            {
                portfoyler = portfoyler.OrderBy(p => p.IlanTarihi).ToList();
            }
            else if (Sıralama == "YE")
            {
                portfoyler = portfoyler.OrderByDescending(p => p.IlanTarihi).ToList();
            }

            return View(portfoyler.ToPagedList());
        }
        public IActionResult Danısmanlar()
        {
            var d=c.danismanlar.ToList();

            return View(d);
        }
        public IActionResult Iletisim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Iletisim(mesajlar m)
        {   
                                        
            c.mesajlars.Add(m);
            c.SaveChanges();   
            return View();
        }
        public IActionResult Portfoy(int ID)
        {
            var portfoy = c.portfoyler.Where(x => x.id == ID).ToList();
            var danısman = c.danismanlar.ToList();
            var resimler=c.Resim.Where(x=> x.Portfoyid ==ID).ToList();

            ViewBag.danisman = danısman;
            ViewBag.resim = resimler.ToList();

            return View(portfoy);
        }
        public IActionResult Profil(int ID)
        {
            var danısman = c.danismanlar.Where(x => x.id == ID).ToList();
            var portfoy= c.portfoyler.OrderByDescending(x=>x.IlanTarihi).Where(x => x.danismanid ==ID).ToList();

            ViewBag.ptoplamı = portfoy.Count;
            ViewBag.portfoy = portfoy;


            return View(danısman);
        }
        [HttpGet]
        public IActionResult girisyap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> girisyap(kullanici k)
        {
            var login=c.Kullanicilar.FirstOrDefault(x=>x.KullaniciAdi==k.KullaniciAdi && x.Sifre==k.Sifre);
            if (login !=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,k.KullaniciAdi)
                };
                var useridentity=new ClaimsIdentity(claims,"Login");

                ClaimsPrincipal principal =new ClaimsPrincipal(useridentity);

                await HttpContext.SignInAsync(principal);


                return RedirectToAction("Index", "Panel");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}