using prolabson.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Repository
{
    public class DoktorData
    {

        private prolabsonEntities1 db = new prolabsonEntities1();

        // Tüm doktorları veritabanından alır
        public List<Doktorlar> GetAll()
        {
            return db.Doktorlar.ToList();
        }

        // 'id' parametresine göre bir doktoru veritabanından alır
        public Doktorlar GetById(int id)
        {
            return db.Doktorlar.Find(id);
        }

        // Bir doktora ait tüm randevuları alır
        public List<Randevular> GetRandevularByDoktorId(int doktorId)
        {
            return db.Randevular.Where(r => r.DoktorID == doktorId).ToList();
        }

        // Bir doktora yeni bir randevu ekler
        public void CreateRandevu(Randevular randevu)
        {
            db.Randevular.Add(randevu);
            db.SaveChanges();
        }

        // Bir randevuyu günceller
        public void UpdateRandevu(Randevular randevu)
        {
            db.Entry(randevu).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Bir randevuyu siler
        public void DeleteRandevu(int randevuId)
        {
            Randevular randevu = db.Randevular.Find(randevuId);
            if (randevu != null)
            {
                db.Randevular.Remove(randevu);
                db.SaveChanges();
            }
        }
        public void Profil(Doktorlar Doktor)
        {
            using (var db = new prolabsonEntities1())
            {
                // Update işlemini gerçekleştir
                db.Doktorlar.Attach(Doktor); // Ekleme veya güncelleme işlemi için entity'yi bağla
                db.Entry(Doktor).State = EntityState.Modified; // Durumu 'Modified' olarak işaretle

                db.SaveChanges(); // Değişiklikleri kaydet

                Console.WriteLine("Doktor güncellendi.");
            }
        }
        public void Create(int id, string ad, string soyad, string uzmanlikalani, string hastane)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "INSERT INTO Doktorlar (DoktorID,Ad, Soyad, UzmanlikAlani, CalistigiHastane) VALUES (@p0, @p1, @p2,@p3,@p4)";
                int affectedRows = db.Database.ExecuteSqlCommand(sql,id, ad, soyad, uzmanlikalani, hastane);
                Console.WriteLine($"{affectedRows} rows affected.");
               
            }
        }

        // READ işlemi
        public Doktorlar Read(int id)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "SELECT * FROM Doktorlar WHERE DoktorID = @p0";
                // var doktor = db.Database.ExecuteSqlCommand(sql);
                
                Doktorlar doktor = db.Database.SqlQuery<Doktorlar>(sql, id).FirstOrDefault();
                
                if (doktor != null)
                {
                    Console.WriteLine($"Doktor bulundu:{doktor}");
                    return doktor;
                }
                else
                {
                    Console.WriteLine("Doktor bulunamadı.");
                    return null;
                }
            }
        }

        // UPDATE işlemi
        public void Update(Doktorlar doktor)
        {
            using (var db = new prolabsonEntities1())
            {
                // Update işlemini gerçekleştir
                db.Doktorlar.Attach(doktor); // Ekleme veya güncelleme işlemi için entity'yi bağla
                db.Entry(doktor).State = EntityState.Modified; // Durumu 'Modified' olarak işaretle

                db.SaveChanges(); // Değişiklikleri kaydet

                Console.WriteLine("Doktor güncellendi.");
            }
        }


        // DELETE işlemi
        public void Delete(int id)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "DELETE FROM Doktorlar WHERE DoktorID = @p0";
                int affectedRows = db.Database.ExecuteSqlCommand(sql, id);
                Console.WriteLine($"{affectedRows} rows deleted.");
            }
        }

       



    }

}