using Microsoft.AspNetCore.Mvc;
using Aziz_Emlak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Drawing;

namespace Aziz_Emlak.Controllers
{
    

    [Authorize]
    public class PanelController : Controller
    {   
        VeritabaniContext c = new VeritabaniContext();
        [HttpGet]
        public  IActionResult Index()
        {
            var kulanici = User.Identity.Name;
            var danismanid = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.danismanid).FirstOrDefault();
            var deger = c.danismanlar.Where(x => x.id == danismanid).ToList();
            return View(deger);
        }
        [HttpPost]
        public IActionResult Index(danisman d,DanismanImage dimage)
        {
            
            var danis = c.danismanlar.Find(d.id);
            string wp = "https://wa.me/9";

            string location = danis.DanismanFotgrafi;
            string image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", location);
            if (System.IO.File.Exists(image))
            {
                System.IO.File.Delete(image);
            }
            if (dimage.DanismanFotgrafi != null)
            {
                string extension = Path.GetExtension(dimage.DanismanFotgrafi.FileName);
                string newimagename = Guid.NewGuid() + extension;
                string loc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                using (var stream = new FileStream(loc, FileMode.Create))
                {
                    dimage.DanismanFotgrafi.CopyTo(stream);
                }
                danis.DanismanFotgrafi = newimagename;

            }


            danis.Role = d.Role;
            danis.Tecrube = d.Tecrube;
            danis.DanismanAdi = d.DanismanAdi;
            danis.DanismanAciklamasi = d.DanismanAciklamasi;
            danis.DanismanEmail = d.DanismanEmail;
            danis.DanismanGsm = d.DanismanGsm;
            danis.DanismanFace = d.DanismanFace;
            danis.DanismanInsta = d.DanismanInsta;
            danis.DanismanTwitter = d.DanismanTwitter;
            danis.DanismanWp = wp+ d.DanismanWp;
          
            c.SaveChanges();

            return RedirectToAction("Index",danis);
        }
        public IActionResult Mesajlar(int page=1)
        {
            var m = c.mesajlars.OrderByDescending(x=>x.mesajtarihi).ToList().ToPagedList(page, 9);

            return View(m);
        }
        public IActionResult MesajlarDelete(int id)
        {
            var mesaj = c.mesajlars.Find(id);
            c.mesajlars.Remove(mesaj);
            c.SaveChanges();

            return RedirectToAction("Mesajlar");
        }
        public IActionResult Danismans()
        {
            var role = c.danismanlar.ToList();

            var kulanici = User.Identity.Name;
            var UserId = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.Id).FirstOrDefault();
            var deger = c.Kullanicilar.Where(x => x.Id == UserId).ToList();
            var did = deger[0].danismanid;
            var yetkili = c.danismanlar.Where(x => x.id == did).ToList();
            var d = yetkili[0].Role;


            if( d=="Danışman")
            {
                return View("yetkisiz");
            }
            else
            {
                return View(role);
            }
            
        }
        public IActionResult Danismansekle()
        {
       
            return View();
        }
        [HttpPost]
        public IActionResult Danismansekle(DanismanImage d,kullanici k)
        {
            danisman danismann= new danisman();
            string wp = "https://wa.me/9";
            if (d.DanismanFotgrafi != null)
            {
                string extension = Path.GetExtension(d.DanismanFotgrafi.FileName);
                string newimagename = Guid.NewGuid() + extension;
                string location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Media/img/",newimagename);
                var stream= new FileStream(location, FileMode.Create);
                d.DanismanFotgrafi.CopyTo(stream);
                danismann.DanismanFotgrafi = newimagename;
 
            }
            danismann.Tecrube=d.Tecrube;
            danismann.Role=d.Role;
            danismann.DanismanAdi = d.DanismanAdi;
            danismann.DanismanAciklamasi= d.DanismanAciklamasi;
            danismann.DanismanGsm = d.DanismanGsm;
            danismann.DanismanEmail= d.DanismanEmail;
            danismann.DanismanFace= d.DanismanFace;
            danismann.DanismanInsta= d.DanismanInsta;
            danismann.DanismanTwitter= d.DanismanTwitter;
            danismann.DanismanWp= wp+d.DanismanWp;
            

            c.danismanlar.Add(danismann);
            c.SaveChanges();

            var userId = danismann.id;
            k.danismanid = userId;
            c.Kullanicilar.Add(k);
            c.SaveChanges();

            return RedirectToAction("Danismans");
        }
        public IActionResult DanismansDelete(int id)
        {
            var danismann = c.danismanlar.Find(id);

            string location = danismann.DanismanFotgrafi;
            string image = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Media/img",location);
            if(System.IO.File.Exists(image))
            {
                System.IO.File.Delete(image);
            }

            c.danismanlar.Remove(danismann);
            c.SaveChanges();

            return RedirectToAction("Danismans");
        }
        [HttpGet]
        public IActionResult DanismansDüzenle(int id)
        {
            var danısman = c.danismanlar.Where(x => x.id == id).ToList();
            var user = c.Kullanicilar.Where(x => x.danismanid == id).ToList();

            ViewBag.user = user;

            return View(danısman);

        }
        [HttpPost]
        public IActionResult DanismansDüzenle(danisman d,DanismanImage dimage)
        {


            var danis = c.danismanlar.Find(d.id);
            string wp = "https://wa.me/9";

            string location = danis.DanismanFotgrafi;
            string image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", location);

            var dfoto = dimage.DanismanFotgrafi; //5
                     
            if(dfoto!=null)
            {
                if(image!=null && dfoto!=null)
                {                    

                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                        string extension = Path.GetExtension(dimage.DanismanFotgrafi.FileName);
                        string newimagename = Guid.NewGuid() + extension;
                        string loc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                        using (var stream = new FileStream(loc, FileMode.Create))
                        {
                            dimage.DanismanFotgrafi.CopyTo(stream);
                        }

                       
                        danis.DanismanFotgrafi = newimagename;

                    
                }
                if(image==null)
                {
                    string extension = Path.GetExtension(dimage.DanismanFotgrafi.FileName);
                    string newimagename = Guid.NewGuid() + extension;
                    string loc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                    var stream = new FileStream(loc, FileMode.Create);
                    dimage.DanismanFotgrafi.CopyTo(stream);
                    danis.DanismanFotgrafi = newimagename;
                }
            }
            danis.Role = d.Role;
            danis.Tecrube = d.Tecrube;
            danis.DanismanAdi = d.DanismanAdi;
            danis.DanismanAciklamasi = d.DanismanAciklamasi;
            danis.DanismanEmail = d.DanismanEmail;
            danis.DanismanGsm = d.DanismanGsm;
            danis.DanismanFace = d.DanismanFace;
            danis.DanismanInsta = d.DanismanInsta;
            danis.DanismanTwitter = d.DanismanTwitter;
            if (d.DanismanWp == null)
            {
                danis.DanismanWp = wp + d.DanismanWp;
            }
            else
            {
                danis.DanismanWp = d.DanismanWp;
            }
            

            c.SaveChanges();


            return RedirectToAction("Danismans");
        }     
        public IActionResult Portfoylerim(int Page=1)
        {            

            portfoy portfoy = new portfoy();

            var kulanici = User.Identity.Name;
            var danismanid = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.danismanid).FirstOrDefault();
            var deger = c.danismanlar.Where(x => x.id == danismanid).ToList();
            var did = deger[0].id;
            var pid=c.portfoyler.Where(x=> x.danismanid == did ).ToList();

            var p= pid.OrderByDescending(x=>x.IlanTarihi).ToList().ToPagedList(Page, 10);

            return View(p);

        }
        public IActionResult Portfoyekle()
        {

            var kulanici = User.Identity.Name;
            var UserId = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.Id).FirstOrDefault();
            var user = c.Kullanicilar.Find(UserId);
            var Danisman = c.danismanlar.Where(x=> x.id ==user.danismanid).ToList();

            return View(Danisman);
        }
        [HttpPost]
        public IActionResult Portfoyekle(portfoy p, Images images)
        {
            if (images.ImageName != null)
            {
                string extension = Path.GetExtension(images.ImageName[0].FileName);
                string newimagename = Guid.NewGuid() + extension;
                string location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    images.ImageName[0].CopyTo(stream);
                }                 
                p.Image = newimagename;

            }

            c.portfoyler.Add(p);
            c.SaveChanges();

            Resim resim=new Resim();
            string[] list = new string[images.ImageName.Count()];

            for (int i = 0; i < images.ImageName.Count(); i++)
            {
                list[i] = images.ImageName[i].FileName;

            }
            var imgid=c.Resim.Take(1).ToList();

            Random rnd = new Random();
            int x = 0;
            foreach (var File in list)
            {
                int y = rnd.Next(100, 50000000); 
                string extension = Path.GetExtension(File);
                string newimagename = Guid.NewGuid() + extension;
                string location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    images.ImageName[x].CopyTo(stream);
                }                   
                resim.Name = newimagename;
                resim.Portfoyid =p.id;
                resim.id = y;
                c.Resim.Add(resim);
                c.SaveChanges();
                x++;
            }

            var kulanici = User.Identity.Name;
            var UserId = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.Id).FirstOrDefault();
            var user = c.Kullanicilar.Find(UserId);
            var Danisman = c.danismanlar.Where(x => x.id == user.danismanid).ToList();

           

            return RedirectToAction("Portfoylerim", Danisman);      
        }
        public IActionResult PortfoySil(int id)
        {
            var portfot=c.portfoyler.Find(id);

            var Images = c.Resim.Where(x => x.Portfoyid == id).ToList();


            if (portfot.Image != null)
            {
                string locationn = portfot.Image;
                string img = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", locationn);
                if (System.IO.File.Exists(img))
                {
                    System.IO.File.Delete(img);
                }
            }


            foreach (var i in Images)
            {
                if (i.Name != null)
                {
                    string locationn = i.Name;
                    string imagee = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Media\\img", locationn);
                    if (System.IO.File.Exists(imagee))
                    {
                        System.IO.File.Delete(imagee);
                    }
                }
            }
                    c.portfoyler.Remove(portfot);
            c.SaveChanges();

            return RedirectToAction("Portfoylerim");
        }
        public IActionResult PortfoyDuzenle(int id)
        {
            var p = c.portfoyler.Find(id);


            return View(p);
        }
        [HttpPost]
        public IActionResult PortfoyDuzenle(portfoy p)
        {
            var portfoy = c.portfoyler.Find(p.id);
            portfoy.KısaAciklama = p.KısaAciklama;
            portfoy.Ilce=p.Ilce;
            portfoy.Mahalle=p.Mahalle;
            portfoy.PortfoyTipi=p.PortfoyTipi;
            portfoy.KonutTipi=p.KonutTipi;
            portfoy.Fiyati=p.Fiyati;
            portfoy.Mburut=p.Mburut;
            portfoy.Mnet=p.Mnet;
            portfoy.OdaSayısı = p.OdaSayısı;
            portfoy.BinaYasi=p.BinaYasi;
            portfoy.BulunduguKat=p.BulunduguKat;
            portfoy.KatSayısı = p.KatSayısı;
            portfoy.Isıtma = p.Isıtma;
            portfoy.BanyoSayısı = p.BanyoSayısı;
            portfoy.Aidat=p.Aidat;
            portfoy.Esyalı = p.Esyalı;
            portfoy.SiteIcerisinde = p.SiteIcerisinde;
            portfoy.Baslik=p.Baslik;
            portfoy.Durumu=p.Durumu;
            portfoy.Onay=p.Onay;
            portfoy.Kuzey=p.Kuzey;
            portfoy.Dogu=p.Dogu;
            portfoy.Guney=p.Guney;
            portfoy.ADSL=p.ADSL;
            portfoy.AkiliEv=p.AkiliEv;
            portfoy.Alarm=p.Alarm;
            portfoy.AlaturkaTuvalet = p.AlaturkaTuvalet;
            portfoy.AmerikanMutfak=p.AmerikanMutfak;
            portfoy.CelikKapi=p.CelikKapi;
            portfoy.DusaKabin=p.DusaKabin;
            portfoy.EbeveynBanyosu=p.EbeveynBanyosu;
            portfoy.IsiCam=p.IsiCam;
            portfoy.Diafon=p.Diafon;
            portfoy.Kuvet=p.Kuvet;
            portfoy.KartonpiyerTavan = p.KartonpiyerTavan;
            portfoy.Teras=p.Teras;
            portfoy.Kiler=p.Kiler;
            portfoy.SesYalıtımı = p.SesYalıtımı;
            portfoy.IsıYalıtımı = p.IsıYalıtımı;
            portfoy.Jenerator=p.Jenerator;
            portfoy.KameraSistemi = p.KameraSistemi;
            portfoy.Kapici=p.Kapici;
            portfoy.OtoparkAcık = p.OtoparkAcık;
            portfoy.OtoparkKapalı = p.OtoparkKapalı;
            portfoy.KabloTv=p.KabloTv;
            portfoy.SporAlanı = p.SporAlanı;
            portfoy.SuDeposu = p.SuDeposu;
            portfoy.YanginMerdiveni= p.YanginMerdiveni;
            portfoy.YüzmeHavuzuAcik = p.YüzmeHavuzuAcik;
            portfoy.YüzmeHavuzuKapalı = p.YüzmeHavuzuKapalı;
            portfoy.Asansor=p.Asansor;
            portfoy.AlısverisMerkezi = p.AlısverisMerkezi;
            portfoy.Camii=p.Camii;
            portfoy.Belediye=p.Belediye;
            portfoy.DenizeSifir=p.DenizeSifir;
            portfoy.Eczane=p.Eczane;
            portfoy.Hastane=p.Hastane;
            portfoy.İlkOkul = p.İlkOkul;
            portfoy.OrtaOkul= p.OrtaOkul;
            portfoy.Lise=p.Lise;
            portfoy.Universite=p.Universite;
            portfoy.Park=p.Park;
            portfoy.SaglikOcagi=p.SaglikOcagi;
            portfoy.İtfaye = p.İtfaye;
            portfoy.SehirMerkezi=p.SehirMerkezi;
            portfoy.AnaYol=p.AnaYol;
            portfoy.Cadde=p.Cadde;
            portfoy.Dolmus=p.Dolmus;
            portfoy.E5=p.E5;
            portfoy.Havaalanı = p.Havaalanı;
            portfoy.Metrobus=p.Metrobus;
            portfoy.OtobusDuragı = p.OtobusDuragı;
            portfoy.Tramvay=p.Tramvay;
            portfoy.TEM=p.TEM;
            portfoy.Metro=p.Metro;
            portfoy.Deniz=p.Deniz;
            portfoy.Doga=p.Doga;
            portfoy.Sehir=p.Sehir;
            portfoy.ParkYesilAlan=p.ParkYesilAlan;
            portfoy.Gol=p.Gol;
            portfoy.AraKat=p.AraKat;
            portfoy.AraKatDublex=p.AraKatDublex;
            portfoy.BahceDublex=p.BahceDublex;
            portfoy.Bahceli=p.Bahceli;
            portfoy.CatıDublex = p.CatıDublex;
            portfoy.Forlex=p.Forlex;
            portfoy.DukkanUstu=p.DukkanUstu;
            portfoy.GirisKatı = p.GirisKatı;
            portfoy.TersDublex=p.TersDublex;
            portfoy.Dublex=p.Dublex;
            portfoy.Triplex=p.Triplex;
            portfoy.Mustakil=p.Mustakil;
            portfoy.ZeminKat=p.ZeminKat;


            c.SaveChanges();
            return RedirectToAction("Portfoylerim",p);
        }
        public IActionResult ImageEdit(int id)
        {
            var img = c.Resim.Where(x=>x.Portfoyid==id).ToList();
            var imge = c.portfoyler.Find(id);

            ViewBag.image = imge.Image;
          

      
            return View(img);
        }     
        public IActionResult ImageDelete(int Id)
        {
            var img = c.Resim.Find(Id);

            if (img.Name != null)
            {
                string locationn = img.Name;
                string delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", locationn);
                if (System.IO.File.Exists(delete))
                {
                    System.IO.File.Delete(delete);
                }
            }
            c.Resim.Remove(img);
            c.SaveChanges();

            return RedirectToAction("Portfoylerim");
        }
        [HttpPost]
        public IActionResult ImageEdit(int id , IFormFile Image ,IFormFile Name)
        {
            var img = c.portfoyler.Find(id);
            Resim Img =new Resim();

            Random rnd= new Random();
            var imgid=rnd.Next(0,50000000);

            if (Image != null)
            {
                if (img.Image != null)
                {
                    string locationn = img.Image;
                    string delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", locationn);
                    if (System.IO.File.Exists(delete))
                    {
                        System.IO.File.Delete(delete);
                    }
                }

                string extension = Path.GetExtension(Image.FileName);
                string newimagename = Guid.NewGuid() + extension;
                string location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }  
                img.Image = newimagename;

            }
            if(Name!= null)
            {
                string extension = Path.GetExtension(Name.FileName);
                string newimagename = Guid.NewGuid() + extension;
                string location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                Name.CopyTo(stream);
                Img.Name = newimagename;
                Img.Portfoyid = img.id;
                Img.id = imgid;

                c.Resim.Add(Img);

            }

            c.SaveChanges();

            return RedirectToAction("ImageEdit");
        }

        public IActionResult Ayarlar()
        {
            var kulanici = User.Identity.Name;
            var UserId = c.Kullanicilar.Where(x => x.KullaniciAdi == kulanici).Select(y => y.Id).FirstOrDefault();
            var deger = c.Kullanicilar.Where(x => x.Id == UserId).ToList();

            return View(deger);
        }
        [HttpPost]
        public IActionResult Ayarlar(kullanici k)
        {
            var User = c.Kullanicilar.Find(k.Id);
            User.KullaniciAdi = k.KullaniciAdi;
            User.Sifre= k.Sifre; 

            c.SaveChanges();

            return RedirectToAction("girisyap", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("girisyap", "Home");
        }
    }
}
  