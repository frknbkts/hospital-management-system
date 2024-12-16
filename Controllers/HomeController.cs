using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using prolabson.Data;

namespace prolabson.Controllers
{
    public class HomeController : Controller
    {
        //test için
        public ActionResult Index()
        {
            DoktorData doktor = new DoktorData();
             
            Doktorlar doktor1= doktor.Read(1);
            
            return Content(doktor1.Ad);
        }

        public ActionResult Login() {
            

            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanicilar kullanici)
        {
            // Veritabanı bağlamını kullanın
            using (var db = new prolabsonEntities1()) // KullaniciContext sınıfınızı kullanın
            {
                // Kullanıcıyı sorgulayın
                var gelenKullanici = db.Kullanicilar
                    .Where(k => k.KullaniciAdi == kullanici.KullaniciAdi && k.Parola == kullanici.Parola)
                    .FirstOrDefault();
                int id = gelenKullanici.KullaniciID;
                
           
                CookieKontrol.CookieKaydet(id.ToString());

                // Kullanıcıyı bulduysanız
                if (gelenKullanici != null)
                {
                    // Kullanıcı tipine göre yönlendirme
                    if (gelenKullanici.KullaniciTipi == "Doktor")
                    {

                        return RedirectToAction("DoktorPaneli", "Doktor");
                    }
                    else if (gelenKullanici.KullaniciTipi == "Yonetici")
                    {
                        return RedirectToAction("YoneticiPaneli", "Yonetici");
                    }
                    else if (gelenKullanici.KullaniciTipi == "Hasta")
                    {
                        return RedirectToAction("HastaPaneli", "Hasta");
                    }
                }
            }

            // Hatalı giriş için mesaj
            ViewBag.Error = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}