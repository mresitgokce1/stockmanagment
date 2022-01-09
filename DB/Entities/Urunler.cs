using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StokYonetimi.DB.Entities
{
    [Table("Urunler")]
    public class Urunler
    {
        [Key] 
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public int Stok { get; set; }
        public bool Durum { get; set; } = true;
        public string Gorsel { get; set; }

        public int KategoriId { get; set; }
        public Kategoriler Kategori { get; set; }
    }
}
