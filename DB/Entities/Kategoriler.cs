using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StokYonetimi.DB.Entities
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        [Key]
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public string Gorsel { get; set; }
        public bool Durum { get; set; }

        public ICollection<Urunler> Urunler { get; set; }
    }
}
