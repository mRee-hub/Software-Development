using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTicket.Models;

namespace GoTicket.Controllers
{
    public class HomeController : Controller
    {
        GoTicketEntities database = new GoTicketEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rota(string tasitTipi)
        {
            ViewBag.TasitTipi = tasitTipi;

            // Örnek olarak Türkiye'nin illeri
            var iller = new List<string>()
{
    "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin",
    "Aydın", "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa",
    "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan",
    "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta",
    "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
    "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla",
    "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop",
    "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van",
    "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman",
    "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"
};

            ViewBag.Iller = new SelectList(iller);
            return View();
        }

        [HttpGet]
        public ActionResult SeferiListele(string tasitTipi, string departure_location, string arrival_location, DateTime arrival_date, DateTime departure_date)
        {
            var rotaVerileri = database.Vehicles
                .Where(v => v.vehicle_type == tasitTipi && v.departure_location == departure_location && v.arrival_location == arrival_location && v.arrival_date == arrival_date && v.departure_date == departure_date)
                .ToList();

            UpdateVehicleCondition(rotaVerileri);

            if (rotaVerileri.Any())
            {
                return View(rotaVerileri);
            }
            else
            {
                ViewBag.Message = "Aradığınız kriterlere uygun sefer bulunamadı.";
                ViewBag.TasitTipi = tasitTipi; // Kullanıcının seçtiği taşıt tipini tekrar gönderiyoruz
                var iller = new List<string>()
{
    "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin",
    "Aydın", "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa",
    "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan",
    "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta",
    "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
    "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla",
    "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop",
    "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van",
    "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman",
    "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"
};
                ViewBag.Iller = new SelectList(iller);

                return View("Rota");
            }
        }

        public ActionResult GetTicketInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListTicketInfo(int ticketId)
        {
            var ticket = database.Tickets.FirstOrDefault(t => t.ticket_id == ticketId);

            return PartialView("_TicketInfoPartial", ticket);
        }

        private void UpdateVehicleCondition(List<Vehicle> aracVerisiListesi)
        {
            foreach (var aracVerisi in aracVerisiListesi)
            {
                var ticketCounter = database.Tickets.Count(x => x.vehicle_type == aracVerisi.vehicle_type && x.vehicle_id == aracVerisi.vehicle_id);

                if (ticketCounter == 0)
                {
                    aracVerisi.vehicle_condition = "Boş";
                }
                else if (ticketCounter == 1)
                {
                    aracVerisi.vehicle_condition = "Orta Dolu";
                }
                else if (ticketCounter == 2)
                {
                    aracVerisi.vehicle_condition = "Dolu";
                }
            }
        }

    }
}