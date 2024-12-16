using prolabson.Data;
using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Controllers
{
    public class DoktorController : Controller
    {
        // GET: Doktor
       
        [HttpGet]
        public ActionResult DoktorPaneli()
        {

            using (prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                Doktorlar doktorlar = new Doktorlar();


                int id = Int32.Parse(Cookie["id"]);
                doktorlar = entities.Doktorlar.FirstOrDefault(x => x.DoktorID == id);
                return View(doktorlar);
            }
        }
        [HttpGet]
        public ActionResult Read()
        {
            DoktorData doktorData = new DoktorData();
            List<Doktorlar> doktorlar = doktorData.GetAll();

            // View'e göndermek için doktor listesini model olarak kullanın
            return View(doktorlar);
        }

        [HttpGet]
        public ActionResult Updatedoktor()
        {
            DoktorData doktorData = new DoktorData();
            List<Doktorlar> doktorlarList = doktorData.GetAll();

            // Doktor listesini ViewBag yerine bir değişkene aktar
            var doktorlar = doktorlarList.Select(d => new SelectListItem { Value = d.DoktorID.ToString(), Text = d.Ad + " " + d.Soyad }).ToList();
            ViewBag.Doktorlar = doktorlar;

            return View();
        }

        [HttpPost]
        public ActionResult UpdatedoktorOnay(Doktorlar doktor)
        {
            if (ModelState.IsValid)
            {
                DoktorData doktorData = new DoktorData();

                // Update işlemini gerçekleştir
                doktorData.Update(doktor);

                TempData["GuncellemeMesaji"] = "Doktor güncellendi.";
                return RedirectToAction("YoneticiPaneli", "Yonetici");

            }

            return View(doktor);
        }

        [HttpGet]
        public ActionResult ProfilDoktor()
        {
            using (prolabsonEntities1 entities = new prolabsonEntities1())
            {
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                Doktorlar doktorlar = new Doktorlar();


                int id = Int32.Parse(Cookie["id"]);
                doktorlar = entities.Doktorlar.FirstOrDefault(x => x.DoktorID == id);
                return View(doktorlar);
            }
        }


        // POST: Hasta/Update/id
        [HttpPost]
        public ActionResult ProfilDoktor(Doktorlar Doktor)
        {
            if (ModelState.IsValid)
            {
                DoktorData DoktorData = new DoktorData();

                // Update işlemini gerçekleştir
                DoktorData.Profil(Doktor);

                TempData["GuncellemeMesaji"] = "Doktor güncellendi.";
                return RedirectToAction("DoktorPaneli", "Doktor");
            }

            return View(Doktor);
        }
        // GET: Doktor/DeleteDoktor/id

        // GET: Doktor/DeleteDoktor/id
        [HttpGet]
        public ActionResult DeleteDoktor()
        {
            prolabsonEntities1 entities2 = new prolabsonEntities1();
            List<Doktorlar> doktorlarList = entities2.Doktorlar.ToList();

            return View(doktorlarList);
        }

        // POST: Doktor/DeleteDoktor/id
        [HttpPost]
        public ActionResult DeleteDoktor(int id)
        {
            try
            {
                DoktorData doktorData = new DoktorData();
                doktorData.Delete(id);
                TempData["SilmeMesaji"] = "Doktor silindi."; // Silme işlemi
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // SqlException hatası numarası
                {
                    // Referans kısıtlaması hatası olduğunda kullanıcıya mesajı göster
                    ViewBag.ErrorMessage = "Seçilen Doktorun Randevusu Vardır Silinemez";
                    
                }
                else
                {
                    // Diğer hata durumları için varsayılan işlemi yap
                    throw;
                }
            }
            //return RedirectToAction("YoneticiPaneli", "Yonetici");
            //return RedirectToAction("DeleteDoktor", "Doktor");
            prolabsonEntities1 entities2 = new prolabsonEntities1();
            List<Doktorlar> doktorlarList = entities2.Doktorlar.ToList();

            return View(doktorlarList);
        }

        // GET: Doktor/CreateDoktor
        [HttpGet]
        public ActionResult CreateDoktor()
        {
            return View();
        }

        // POST: Doktor/CreateDoktor
        [HttpPost]
        public ActionResult CreateDoktor(Doktorlar doktor)
        {
            if (ModelState.IsValid)
            {
                DoktorData doktorData = new DoktorData();
                prolabsonEntities1 db = new prolabsonEntities1();
                Kullanicilar kullanicilar = new Kullanicilar();
                kullanicilar.KullaniciTipi = "Doktor";
                kullanicilar.KullaniciAdi = doktor.Ad + " " + doktor.Soyad;
                kullanicilar = db.Kullanicilar.Add(kullanicilar);
                db.SaveChanges();

                doktorData.Create(kullanicilar.KullaniciID, doktor.Ad, doktor.Soyad, doktor.UzmanlikAlani, doktor.CalistigiHastane);

                return RedirectToAction("YoneticiPaneli", "Yonetici");
            }

            // Model geçersizse, aynı View'ı model ile birlikte geri döndür
            return View(doktor);
        }
    }
}
