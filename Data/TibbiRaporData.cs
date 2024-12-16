using prolabson.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Data
{
    public class TibbiRaporData
    {
        private prolabsonEntities1 db = new prolabsonEntities1();
        public List<TibbiRaporlar> GetTibbiRaporlarByHastaId(int HastaId)
        {
            return db.TibbiRaporlar.Where(r => r.HastaID == HastaId).ToList();
        }

        // Bir Hastaa yeni bir TibbiRapor ekler
        public void CreateTibbiRapor(TibbiRaporlar TibbiRapor)
        {
            db.TibbiRaporlar.Add(TibbiRapor);
            db.SaveChanges();
        }

        // Bir TibbiRaporyu günceller
        public void UpdateTibbiRapor(TibbiRaporlar TibbiRapor)
        {
            db.Entry(TibbiRapor).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Bir TibbiRaporyu siler
        public void DeleteTibbiRapor(int id)
        {
            using (var db = new prolabsonEntities1())
            {
                string sql = "DELETE FROM TibbiRaporlar WHERE TibbiRaporID = @p0";
                int affectedRows = db.Database.ExecuteSqlCommand(sql, id);
                Console.WriteLine($"{affectedRows} rows deleted.");
            }
        }
    }
}