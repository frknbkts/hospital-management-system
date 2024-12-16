using prolabson.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Data
{
    public class RandevuData
    {
        private prolabsonEntities1 db = new prolabsonEntities1();
        public List<Randevular> GetRandevularByHastaId(int HastaId)
        {
            return db.Randevular.Where(r => r.HastaID == HastaId).ToList();
        }
        
        // Bir Hastaa yeni bir randevu ekler
        public void Create(DateTime? randevutarihi, int? hid, int? did)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "INSERT INTO Randevular (RandevuTarihi,HastaID,DoktorID) VALUES (@p0, @p1, @p2)";
                int affectedRows = db.Database.ExecuteSqlCommand(sql, randevutarihi, hid, did);
                Console.WriteLine($"{affectedRows} rows affected.");

            }
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
    }
}