using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StokYonetimi.DB.Entities;

namespace StokYonetimi.Islemler
{
    public class CokluModelGonder
    {
        public Urunler urun { get; set; }
        public Kategoriler kategori { get; set; }
        public List<Urunler> urunler { get; set; }
        public List<Kategoriler> kategoriler { get; set; }
    }
}
