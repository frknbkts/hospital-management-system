using prolabson.Data;
using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prolabson.Controllers
{
    public class RandevuController : Controller
    {
        // GET: Randevu
        public ActionResult Randevular()
        {
            prolabsonEntities1 entities = new prolabsonEntities1();
            
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                List<Randevular> randevular = new List<Randevular>();
                int id = Int32.Parse(Cookie["id"]);
                randevular = entities.Randevular.ToList();


                return View(randevular);
           

        }
        [HttpGet]
        public ActionResult DeleteRandevu()
        {
            prolabsonEntities1 entities2 = new prolabsonEntities1();
            List<Randevular> RandevularList = entities2.Randevular.ToList();

            return View(RandevularList);
        }

        // POST: Hasta/DeleteHasta/id
        [HttpPost]
        public ActionResult DeleteRandevuOnay(int id)
        {
            RandevuData RandevuData = new RandevuData();
            RandevuData.DeleteRandevu(id);

            TempData["SilmeMesaji"] = "Randevu silindi.";
            return RedirectToAction("DeleteRandevu", "Randevu");
        }
        // GET: Randevu/Details/5
        public ActionResult RandevuDetay(int id)
        {
            return View();
        }

        // GET: Randevu/Create
        [HttpGet]
        public ActionResult CreateRandevu()
        {
            DoktorData data = new DoktorData();
            List<Doktorlar> list= data.GetAll();

            return View(list);
        }

        // POST: Randevu/CreateRandevu
        [HttpPost]
        public ActionResult CreateRandevu(int DoktorID, string tarih, string saat)
        {
            string tarihSaat = $"{tarih} {saat}";
            DateTime RandevuTarihi;
            if (DateTime.TryParseExact(tarihSaat, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out RandevuTarihi))
            {
                // Randevu oluşturulabilir, işlemi devam ettir
                // Örneğin:
                RandevuData data = new RandevuData();
                HttpCookie Cookie = CookieKontrol.CookieGetir();
                int id = Int32.Parse(Cookie["id"]);
                data.Create(RandevuTarihi, id, DoktorID);
                
                return RedirectToAction("Randevular");
            }
            else
            {
                
                return View("Error");
            }
           

            // Model geçersizse, aynı View'ı model ile birlikte geri döndür
            
        }
    }
}

