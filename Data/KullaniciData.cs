using prolabson.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace prolabson.Repository
{
    public class KullaniciData
    {
        private prolabsonEntities1 db = new prolabsonEntities1();

        public List<Kullanicilar> GetAll()
        {
            return db.Kullanicilar.ToList();
        }
        // Update işlemi
        public void Update(Kullanicilar Kullanici)
        {
            using (var db = new prolabsonEntities1())
            {
                // Update işlemini gerçekleştir
                db.Kullanicilar.Attach(Kullanici); // Ekleme veya güncelleme işlemi için entity'yi bağla
                db.Entry(Kullanici).State = EntityState.Modified; // Durumu 'Modified' olarak işaretle

                db.SaveChanges(); // Değişiklikleri kaydet

                Console.WriteLine("Kullanıcı güncellendi.");
            }
        }

        // ... (Diğer HastaData metodları)
    }
}
