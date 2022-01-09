using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StokYonetimi.DB;
using StokYonetimi.DB.Entities;
using StokYonetimi.Islemler;

namespace StokYonetimi.Controllers
{
    public class HomeController : Controller
    {

        private readonly StokYonetimiDbContext _context;
        private readonly UrunIslemleri _UI;
        private readonly KategoriIslemleri _KI;

        public HomeController(StokYonetimiDbContext context,
            KategoriIslemleri kategoriIslemleri,
            UrunIslemleri urunIslemleri
            )
        {
            _context = context;
            _KI = kategoriIslemleri;
            _UI = urunIslemleri;
        }

        public IActionResult Index()
        {
            var urun = _UI.UrunGetir(2);
            var urunler = _UI.UrunleriListele();
            var kategoriler = _KI.KategorileriListele();

            var cokluModel = new CokluModelGonder()
            {
                urun = urun,
                urunler = urunler,
                kategoriler = kategoriler
                
            };

            return View(cokluModel);
        }
    }
}
