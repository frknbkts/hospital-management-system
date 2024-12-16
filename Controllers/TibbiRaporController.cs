using prolabson.Data;
using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Controllers
{
    public class TibbiRaporController : Controller
    {
        // GET: TibbiRapor
        prolabsonEntities1 db = new prolabsonEntities1();
        public ActionResult TibbiRaporlar()
        {
            using (prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                List<TibbiRaporlar> TibbiRaporlar = new List<TibbiRaporlar>();
                int id = Int32.Parse(Cookie["id"]);
                TibbiRaporlar = entities.TibbiRaporlar.ToList();
                return View(TibbiRaporlar);
            }

        }
        [HttpGet]
        public ActionResult CreateTibbiRapor()
        {  
            return View(); 
        }
        [HttpPost]
        public ActionResult CreateTibbiRapor(HttpPostedFileBase RaporIcerigi, string HastaAdi)
        {
            try
            {
                if (RaporIcerigi != null && RaporIcerigi.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(RaporIcerigi.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    RaporIcerigi.SaveAs(path);

                    // Burada modelinize göre veritabanına kaydetme işlemini gerçekleştirebilirsiniz.
                    // Örneğin:
                     TibbiRaporlar model = new TibbiRaporlar();
                     HttpCookie Cookie = CookieKontrol.CookieGetir();
                     int id = Int32.Parse(Cookie["id"]);
                     prolabsonEntities1 entities1 = new prolabsonEntities1();
                     Kullanicilar kullanicilar = new Kullanicilar();
                     kullanicilar = entities1.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == HastaAdi);
                     model.DoktorID = id;  
                     model.HastaID = kullanicilar.KullaniciID;
                     model.RaporIcerigi = path;  // veya sadece fileName
                     db.TibbiRaporlar.Add(model);
                     db.SaveChanges();
                }

                return RedirectToAction("DoktorPaneli", "Doktor");  // Başarılı olursa yönlendireceğiniz sayfa.
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                return View();
            }
        }
        /* [HttpPost]
       public ActionResult CreateTibbiRapor(TibbiRaporlar p)
        {
            if (Request.Files.Count > 0) 
            { 
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.RaporIcerigi = "/Image/" + dosyaadi + uzanti;
            }
            db.TibbiRaporlar.Add(p);
            db.SaveChanges();
            return View();
        }
        */
        [HttpGet]
        public ActionResult DeleteTibbiRapor()
        {
            prolabsonEntities1 entities2 = new prolabsonEntities1();
            List<TibbiRaporlar> TibbiRaporlarList = entities2.TibbiRaporlar.ToList();

            return View(TibbiRaporlarList);
        }

        // POST: Hasta/DeleteHasta/id
        // POST: TibbiRapor/DeleteTibbiRaporOnay/5
        [HttpPost]
        public ActionResult DeleteTibbiRaporOnay(int id)
        {
            var tibbiRapor = db.TibbiRaporlar.Find(id);
            if (tibbiRapor != null)
            {
                db.TibbiRaporlar.Remove(tibbiRapor);
                db.SaveChanges();
                TempData["SilmeMesaji"] = "Tibbi Rapor başarıyla silindi.";
            }
            else
            {
                TempData["SilmeMesaji"] = "Silinecek Tibbi Rapor bulunamadı.";
            }
            return RedirectToAction("TibbiRaporlar");
        }
        // GET: TibbiRapor/Details/5
        public ActionResult TibbiRaporDetay(int id)
        {
            return View();
        }

        // GET: TibbiRapor/Create

    }
}
