using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StokYonetimi.DB;
using StokYonetimi.DB.Entities;

namespace StokYonetimi.Islemler
{

    public class KategoriIslemleri
    {
        protected readonly StokYonetimiDbContext _context;
        public KategoriIslemleri(StokYonetimiDbContext context)
        {
            _context = context;
        }

        public List<Kategoriler> KategorileriListele()
        {
            return _context.Kategoriler.ToList();
        }
        public Kategoriler KategoriGetir(int Id)
        {
            return _context.Kategoriler.FirstOrDefault(u => u.Id == Id);
        }
        public void KategoriEkle(Kategoriler kategori)
        {
            _context.Kategoriler.Add(kategori);
            _context.SaveChanges();
        }
        public void KategoriSil(int id)
        {
            _context.Kategoriler.Remove(KategoriGetir(id));
            _context.SaveChanges();
        }
        public void KategoriGuncelle(Kategoriler kategori)
        {
            _context.Kategoriler.Update(kategori);
            _context.SaveChanges();
        }

    }
}
