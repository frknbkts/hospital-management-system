using prolabson.Data;
using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Controllers
{
    public class HastaController : Controller
    {
        // GET: Hasta
        [HttpGet]
        public ActionResult HastaPaneli()
        {
            
            using(prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                Hastalar hastalar = new Hastalar();
              
               
                int id = Int32.Parse(Cookie["id"]);
                hastalar = entities.Hastalar.FirstOrDefault( x => x.HastaID == id);
                return View(hastalar);
            }
        }
        [HttpGet]
        public ActionResult Read()
        {
            HastaData HastaData = new HastaData();
            List<Hastalar> Hastalar = HastaData.GetAll();

            // View'e göndermek için Hasta listesini model olarak kullanın
            return View(Hastalar);
        }

        // GET: Hasta/Update/id
        [HttpGet]
        public ActionResult UpdateHasta()
        {
            HastaData HastaData = new HastaData();
            List<Hastalar> HastalarList = HastaData.GetAll();

            // Hasta listesini ViewBag yerine bir değişkene aktar
            //ViewBag.Hastalar = HastalarList.Select(d => new SelectListItem { Value = d.HastaID.ToString(), Text = d.Ad + " " + d.Soyad }).ToList();
            var hastalar = HastalarList.Select(d => new SelectListItem { Value = d.HastaID.ToString(), Text = d.Ad + " " + d.Soyad }).ToList();
            ViewBag.Hastalar = hastalar;
            return View();
        }


        // POST: Hasta/Update/id
        [HttpPost]
        public ActionResult UpdateHastaOnay(Hastalar Hasta)
        {
            if (ModelState.IsValid)
            {
                HastaData HastaData = new HastaData();

                // Update işlemini gerçekleştir
                HastaData.Update(Hasta);

                TempData["GuncellemeMesaji"] = "Hasta güncellendi.";
                return RedirectToAction("YoneticiPaneli", "Yonetici");
            }

            return View(Hasta);
        }
        [HttpGet]
        public ActionResult LaboratuvarSonuc()
        {
            using (prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                Hastalar hasta = new Hastalar();
                int hastaID = Int32.Parse(Cookie["id"]);

                hasta = entities.Hastalar.FirstOrDefault(x => x.HastaID == hastaID);

                var tibbiRaporlar = entities.TibbiRaporlar
                                          .Where(r => r.HastaID == hastaID)
                                          .ToList(); // Veya ToListAsync()

                ViewBag.Hasta = hasta; // ViewBag ile hasta bilgilerini gönder
                ViewBag.TibbiRaporlar = tibbiRaporlar; // ViewBag ile tıbbi raporları gönder

                return View();
            }
        }
        [HttpGet]
        public ActionResult ProfilHasta()
        {
            using (prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                Hastalar hastalar = new Hastalar();


                int id = Int32.Parse(Cookie["id"]);
                hastalar = entities.Hastalar.FirstOrDefault(x => x.HastaID == id);
                return View(hastalar);
            }
        }


        // POST: Hasta/Update/id
        [HttpPost]
        public ActionResult ProfilHasta(Hastalar Hasta)
        {
            if (ModelState.IsValid)
            {
                HastaData HastaData = new HastaData();

                // Update işlemini gerçekleştir
                HastaData.Profil(Hasta);

                TempData["GuncellemeMesaji"] = "Hasta güncellendi.";
                return RedirectToAction("HastaPaneli", "Hasta");
            }

            return View(Hasta);
        }

        // GET: Hasta/DeleteHasta/id

        // GET: Hasta/DeleteHasta/id
        [HttpGet]
        public ActionResult DeleteHasta()
        {
            prolabsonEntities1 entities2 = new prolabsonEntities1();
            List<Hastalar> HastalarList = entities2.Hastalar.ToList();

            return View(HastalarList);
        }

        // POST: Hasta/DeleteHasta/id
        [HttpPost]
        public ActionResult DeleteHastaOnay(int id)
        {
            HastaData HastaData = new HastaData();
            HastaData.Delete(id);

            TempData["SilmeMesaji"] = "Hasta silindi.";
            return RedirectToAction("YoneticiPaneli", "Yonetici");
        }

        // GET: Hasta/CreateHasta
        [HttpGet]
        public ActionResult CreateHasta()
        {
            return View();
        }

        // POST: Hasta/CreateHasta
        [HttpPost]
        public ActionResult CreateHasta(Hastalar Hasta)
        {
            if (ModelState.IsValid)
            {
                HastaData HastaData = new HastaData();
                prolabsonEntities1 db = new prolabsonEntities1();
                Kullanicilar kullanicilar = new Kullanicilar();
                kullanicilar.KullaniciTipi = "Hasta";
                kullanicilar.KullaniciAdi = Hasta.Ad + " " + Hasta.Soyad;
                kullanicilar = db.Kullanicilar.Add(kullanicilar);
                db.SaveChanges();

                HastaData.Create(kullanicilar.KullaniciID, Hasta.Ad, Hasta.Soyad, Hasta.DogumTarihi, Hasta.Cinsiyet, Hasta.TelefonNumarasi, Hasta.Adres);

                return RedirectToAction("YoneticiPaneli", "Yonetici");
            }

            // Model geçersizse, aynı View'ı model ile birlikte geri döndür
            return View(Hasta);
        }
    }
}
