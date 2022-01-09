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
    public class KategoriController : Controller
    {
        private readonly StokYonetimiDbContext _context;
        private readonly UrunIslemleri _UI;
        private readonly KategoriIslemleri _KI;
        private readonly IHostingEnvironment _ortam;
        public KategoriController(StokYonetimiDbContext context, UrunIslemleri UI, KategoriIslemleri KI, IHostingEnvironment ortam)
        {
            _context = context;
            _UI = UI;
            _KI = KI;
            _ortam = ortam;
        }

        public IActionResult KategoriListele()
        {
            var cokluModelGonder = new CokluModelGonder()
            {
                kategoriler = _KI.KategorileriListele(),
                urunler = _UI.UrunleriListele()
            };

            return View(cokluModelGonder);
        }
        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KategoriEkle(string KategoriAdi, IFormFile GorselSec, bool KategoriDurumu)
        {
            var gorselAdi = Guid.NewGuid().ToString();
            if (GorselSec != null)
            {
                var Yukle = Path.Combine(_ortam.WebRootPath, "img/kategorigorselleri", gorselAdi + ".jpg");
                GorselSec.CopyTo(new FileStream(Yukle, FileMode.Create));
            }

            var eklenecekKategori = new Kategoriler()
            {
                KategoriAdi = KategoriAdi,
                Gorsel = gorselAdi+".jpg",
                Durum = KategoriDurumu
            };

            _KI.KategoriEkle(eklenecekKategori);

            return RedirectToAction("KategoriListele", "Kategori");
        }
        public IActionResult KategoriSil(int id)
        {
            _KI.KategoriSil(id);
            return RedirectToAction("KategoriListele", "Kategori");
        }
        [HttpGet]
        public IActionResult KategoriGuncelle(int id)
        {
            var cokluModelGonder = new CokluModelGonder()
            {
                kategori = _KI.KategoriGetir(id)
            };

            return View(cokluModelGonder);
        }
        [HttpPost]
        public IActionResult KategoriGuncelle(int KategoriId, string KategoriAdi, IFormFile GorselSec, bool KategoriDurumu)
        {
            var guncellenecekKategori = _KI.KategoriGetir(KategoriId);

            if (GorselSec != null)
            {
                var gorselAdi = Guid.NewGuid().ToString();
                var Yukle = Path.Combine(_ortam.WebRootPath, "img/kategorigorselleri", gorselAdi + ".jpg");
                GorselSec.CopyTo(new FileStream(Yukle, FileMode.Create));


                guncellenecekKategori.KategoriAdi = KategoriAdi;
                guncellenecekKategori.Gorsel = gorselAdi + ".jpg";
                guncellenecekKategori.Durum = KategoriDurumu;
            }
            else
            {
                guncellenecekKategori.KategoriAdi = KategoriAdi;
                guncellenecekKategori.Durum = KategoriDurumu;
            }

            _KI.KategoriGuncelle(guncellenecekKategori);

            return RedirectToAction("KategoriListele", "Kategori");
        }
    }
}
