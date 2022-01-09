using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StokYonetimi.DB;
using StokYonetimi.DB.Entities;
using StokYonetimi.Islemler;

namespace StokYonetimi.Controllers
{
    public class UrunController : Controller
    {
        private readonly StokYonetimiDbContext _context;
        private readonly UrunIslemleri _UI;
        private readonly KategoriIslemleri _KI;
        private readonly IHostingEnvironment _ortam;
        public UrunController(StokYonetimiDbContext context, UrunIslemleri UI, KategoriIslemleri KI, IHostingEnvironment ortam)
        {
            _context = context;
            _UI = UI;
            _KI = KI;
            _ortam = ortam;
        }

        public IActionResult UrunListele()
        {
            var urunler = _UI.UrunleriListele();
            var kategoriler = _KI.KategorileriListele();

            var cokluModelGonder = new CokluModelGonder()
            {
                urunler = urunler,
                kategoriler = kategoriler
            };

            return View(cokluModelGonder);
        }

        [HttpGet]
        public IActionResult UrunEkle()
        {
            var kategoriler = _KI.KategorileriListele();

            var cokluModelGonder = new CokluModelGonder()
            {
                kategoriler = kategoriler
            };

            return View(cokluModelGonder);
        }

        [HttpPost]
        public IActionResult UrunEkle(string UrunAdi, int UrunStogu, bool UrunDurumu, int KategoriId, IFormFile GorselSec)
        {

            var gorselAdi = Guid.NewGuid().ToString();
            if (GorselSec != null)
            {
                var Yukle = Path.Combine(_ortam.WebRootPath, "img/urungorselleri", gorselAdi+".jpg");
                GorselSec.CopyTo(new FileStream(Yukle, FileMode.Create));
            }

            var urun = new Urunler()
            {
                UrunAdi = UrunAdi,
                Gorsel = gorselAdi+".jpg",
                Durum = UrunDurumu,
                KategoriId = KategoriId,
                Stok = UrunStogu
            };

            _UI.UrunEkle(urun);

            return RedirectToAction("UrunListele", "Urun");
        }
        public IActionResult UrunSil(int id)
        {
            _UI.UrunSil(id);
            return RedirectToAction("UrunListele", "Urun");
        }
        [HttpGet]
        public IActionResult UrunGuncelle(int id)
        {
            var cokluModelGonder = new CokluModelGonder()
            {
                urun = _UI.UrunGetir(id),
                kategoriler = _KI.KategorileriListele()
            };
            
            return View(cokluModelGonder);
        }

        [HttpPost]
        public IActionResult UrunGuncelle(int UrunId, string UrunAdi, int UrunStogu, bool UrunDurumu, int KategoriId, IFormFile GorselSec)
        {

            var guncellenecekUrun = _UI.UrunGetir(UrunId);

            if (GorselSec != null)
            {
                var gorselAdi = Guid.NewGuid().ToString();
                var Yukle = Path.Combine(_ortam.WebRootPath, "img/urungorselleri", gorselAdi + ".jpg");
                GorselSec.CopyTo(new FileStream(Yukle, FileMode.Create));

                
                guncellenecekUrun.UrunAdi = UrunAdi;
                guncellenecekUrun.Gorsel = gorselAdi + ".jpg";
                guncellenecekUrun.Durum = UrunDurumu;
                guncellenecekUrun.KategoriId = KategoriId;
                guncellenecekUrun.Stok = UrunStogu;

            }
            else
            {
                guncellenecekUrun.UrunAdi = UrunAdi;
                guncellenecekUrun.Durum = UrunDurumu;
                guncellenecekUrun.KategoriId = KategoriId;
                guncellenecekUrun.Stok = UrunStogu;
            }

            _UI.UrunGuncelle(guncellenecekUrun);

            return RedirectToAction("UrunListele", "Urun");
        }
    }
}
