using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StokYonetimi.DB;
using StokYonetimi.DB.Entities;

namespace StokYonetimi.Islemler
{

    public class UrunIslemleri
    {
        protected readonly StokYonetimiDbContext _context;
        public UrunIslemleri(StokYonetimiDbContext context)
        {
            _context = context;
        }

        public  List<Urunler> UrunleriListele()
        {
            return _context.Urunler.ToList();
        }
        public Urunler UrunGetir(int Id)
        {
            return _context.Urunler.FirstOrDefault(u => u.Id == Id);
        }
        public void UrunEkle(Urunler urun)
        {
            _context.Urunler.Add(urun);
            _context.SaveChanges();
        }
        public void UrunSil(int id)
        {
            _context.Urunler.Remove(UrunGetir(id));
            _context.SaveChanges();
        }
        public void UrunGuncelle(Urunler urun)
        {
            _context.Urunler.Update(urun);
            _context.SaveChanges();
        }
    }
}

