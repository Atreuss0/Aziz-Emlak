
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Aziz_Emlak.Models
{
    public class portfoy
    {
        public int id { get; set; }
        public int danismanid { get; set; }

        public string Image { get; set; }
        public string Baslik { get; set; }
        public string KısaAciklama { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string PortfoyTipi { get; set; }
        public string KonutTipi { get; set; }
        public string Durumu { get; set; }      
        public string Mburut { get; set; }
        public string Mnet { get; set; }
        public string OdaSayısı { get; set; }
        public string BinaYasi { get; set; }
        public string BulunduguKat { get; set; }
        public string KatSayısı { get; set; }
        public string Isıtma { get; set; }
        public string BanyoSayısı { get; set; }
        public string Aidat { get; set; }
        public decimal Fiyati { get; set; }

        public Boolean Onay { get; set; }
        public Boolean Esyalı { get; set; }
        public Boolean SiteIcerisinde { get; set; }

        public DateTime IlanTarihi { get; set; }

        public danisman danisman { get; set; }
       
        //cephe
        public Boolean Kuzey { get; set; }
        public Boolean Guney { get; set; }
        public Boolean Dogu { get; set; }
        public Boolean Bati { get; set; }
        //ic Özelikler
        public Boolean ADSL { get; set; }
        public Boolean AkiliEv { get; set; }
        public Boolean Alarm { get; set; }
        public Boolean AlaturkaTuvalet { get; set; }
        public Boolean AmerikanMutfak { get; set; }
        public Boolean CelikKapi { get; set; }
        public Boolean DusaKabin { get; set; }
        public Boolean EbeveynBanyosu { get; set; }
        public Boolean IsiCam { get; set; }
        public Boolean Diafon { get; set; }
        public Boolean Kuvet { get; set; }
        public Boolean KartonpiyerTavan { get; set; }
        public Boolean Teras { get; set; }
        public Boolean Kiler { get; set; }
        //Dış özelikler
        public Boolean SesYalıtımı { get; set; }
        public Boolean IsıYalıtımı { get; set; }
        public Boolean Jenerator { get; set; }
        public Boolean KameraSistemi { get; set; }
        public Boolean Kapici { get; set; }
        public Boolean OtoparkAcık { get; set; }
        public Boolean OtoparkKapalı { get; set; }
        public Boolean KabloTv { get; set; }
        public Boolean SporAlanı { get; set; }
        public Boolean SuDeposu { get; set; }
        public Boolean YanginMerdiveni { get; set; }
        public Boolean YüzmeHavuzuAcik { get; set; }
        public Boolean YüzmeHavuzuKapalı { get; set; }
        public Boolean Asansor { get; set; }
        //Muhit
        public Boolean AlısverisMerkezi { get; set; }
        public Boolean Camii { get; set; }
        public Boolean Belediye { get; set; }
        public Boolean DenizeSifir { get; set; }
        public Boolean Eczane { get; set; }
        public Boolean Hastane { get; set; }
        public Boolean İlkOkul { get; set; }
        public Boolean OrtaOkul { get; set; }
        public Boolean Lise { get; set; }
        public Boolean Universite { get; set; }
        public Boolean Park { get; set; }
        public Boolean SaglikOcagi { get; set; }
        public Boolean İtfaye { get; set; }
        public Boolean SehirMerkezi { get; set; }
        //Ulaşim
        public Boolean AnaYol { get; set; }
        public Boolean Cadde { get; set; }
        public Boolean Dolmus { get; set; }
        public Boolean E5 { get; set; }
        public Boolean Havaalanı { get; set; }
        public Boolean Metrobus { get; set; }
        public Boolean OtobusDuragı { get; set; }
        public Boolean Tramvay { get; set; }
        public Boolean TEM { get; set; }
        public Boolean Metro { get; set; }
        //Manzara
        public Boolean Deniz { get; set; }
        public Boolean Doga { get; set; }
        public Boolean Sehir { get; set; }
        public Boolean ParkYesilAlan { get; set; }
        public Boolean Gol { get; set; }
        //Konut Tipi
        public Boolean AraKat { get; set; }
        public Boolean AraKatDublex { get; set; }
        public Boolean BahceDublex { get; set; }
        public Boolean Bahceli { get; set; }
        public Boolean CatıDublex { get; set; }
        public Boolean Forlex { get; set; }
        public Boolean DukkanUstu { get; set; }
        public Boolean GirisKatı { get; set; }
        public Boolean TersDublex { get; set; }
        public Boolean Dublex { get; set; }
        public Boolean Triplex { get; set; }
        public Boolean Mustakil { get; set; }
        public Boolean ZeminKat { get; set; }


    }
}
