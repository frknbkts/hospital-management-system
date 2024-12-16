using prolabson.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace prolabson.Repository
{
    public class HastaData
    {

        private prolabsonEntities1 db = new prolabsonEntities1();

        // Tüm Hastaları veritabanından alır
        public List<Hastalar> GetAll()
        {
            return db.Hastalar.ToList();
        }

        // 'id' parametresine göre bir Hastau veritabanından alır
        public Hastalar GetById(int id)
        {
            return db.Hastalar.Find(id);
        }

        // Bir Hastaa ait tüm randevuları alır
        public List<Randevular> GetRandevularByHastaId(int HastaId)
        {
            return db.Randevular.Where(r => r.HastaID == HastaId).ToList();
        }

        // Bir Hastaa yeni bir randevu ekler
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
        public void Create(int id, string ad, string soyad, string dogumtarihi, string cinsiyet, string telefonnumarasi, string adres)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "INSERT INTO Hastalar (HastaID,Ad, Soyad, DogumTarihi, Cinsiyet, TelefonNumarasi, Adres) VALUES (@p0, @p1, @p2,@p3,@p4,@p5,@p6)";
                int affectedRows = db.Database.ExecuteSqlCommand(sql, id, ad, soyad, dogumtarihi, cinsiyet, telefonnumarasi, adres);
                Console.WriteLine($"{affectedRows} rows affected.");

            }
        }

        // READ işlemi
        public Hastalar Read(int id)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "SELECT * FROM Hastalar WHERE HastaID = @p0";
                // var Hasta = db.Database.ExecuteSqlCommand(sql);

                Hastalar Hasta = db.Database.SqlQuery<Hastalar>(sql, id).FirstOrDefault();

                if (Hasta != null)
                {
                    Console.WriteLine($"Hasta bulundu:{Hasta}");
                    return Hasta;
                }
                else
                {
                    Console.WriteLine("Hasta bulunamadı.");
                    return null;
                }
            }
        }

        // UPDATE işlemi
        public void Update(Hastalar Hasta)
        {
            using (var db = new prolabsonEntities1())
            {
                // Update işlemini gerçekleştir
                db.Hastalar.Attach(Hasta); // Ekleme veya güncelleme işlemi için entity'yi bağla
                db.Entry(Hasta).State = EntityState.Modified; // Durumu 'Modified' olarak işaretle

                db.SaveChanges(); // Değişiklikleri kaydet

                Console.WriteLine("Hasta güncellendi.");
            }
        }
        public void Profil(Hastalar Hasta)
        {
            using (var db = new prolabsonEntities1())
            {
                // Update işlemini gerçekleştir
                db.Hastalar.Attach(Hasta); // Ekleme veya güncelleme işlemi için entity'yi bağla
                db.Entry(Hasta).State = EntityState.Modified; // Durumu 'Modified' olarak işaretle

                db.SaveChanges(); // Değişiklikleri kaydet

                Console.WriteLine("Hasta güncellendi.");
            }
        }

        // DELETE işlemi
        public void Delete(int id)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "DELETE FROM Hastalar WHERE HastaID = @p0";
                int affectedRows = db.Database.ExecuteSqlCommand(sql, id);
                Console.WriteLine($"{affectedRows} rows deleted.");
            }
        }





    }

}