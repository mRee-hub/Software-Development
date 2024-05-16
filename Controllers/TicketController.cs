using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTicket.Models;

namespace GoTicket.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        GoTicketEntities database = new GoTicketEntities();

        [HttpGet]
        public ActionResult Odeme(int id)
        {
            if (Session["UserID"] != null)
            {
                
                int customerId = Convert.ToInt32(Session["UserID"]);

                var customer = database.Registrations.FirstOrDefault(c => c.customer_id == customerId);
                var vehicle = database.Vehicles.FirstOrDefault(v => v.vehicle_id == id);
                

                if (customer != null && vehicle != null)
                {
                    ViewBag.CustomerName = customer.customer_fname;
                    ViewBag.CustomerSurname = customer.customer_lname;
                    ViewBag.PaymentAmount = vehicle.total_amount;
                    return View();
                }
                else
                {
                    TempData["CustomerVehicleErrorMessage"] = "Kullanıcı bilgileri bulunamadı.";
                    return RedirectToAction("Index", "Home");
                }
            }

            else
            {
                TempData["ErrorMessage"] = "Bu işlemi yapabilmek için önce üye girişi yapmalısınız.";
                return RedirectToAction("Index", "Home");
            }

            
        }

        [HttpPost]
        public ActionResult Odeme(Payment model, int id)
        {
            int customerId = Convert.ToInt32(Session["UserID"]);
            var vehicle = database.Vehicles.FirstOrDefault(v => v.vehicle_id == id);

            if (vehicle != null)
            {
                // Ödeme bilgilerini Payment tablosuna kaydet
                Payment payment = new Payment
                {
                    customer_id = customerId,
                    vehicle_id = vehicle.vehicle_id, // Doğru araç ID'si
                    card_number = model.card_number,
                    card_expiration = model.card_expiration,
                    card_cvc = model.card_cvc,
                    payment_type = model.payment_type,
                    payment_amount = (int)vehicle.total_amount
                };

                database.Payments.Add(payment);
                database.SaveChanges();

                var vehicleId = vehicle.vehicle_id;
                // Ticket tablosuna kayıt yap
                Ticket ticket = new Ticket
                {
                    vehicle_id = vehicleId,
                    deperture_date = vehicle.departure_date,
                    arrival_date = vehicle.arrival_date,
                    vehicle_type = vehicle.vehicle_type,
                    // Diğer bilet bilgileri
                };

                database.Tickets.Add(ticket);
                database.SaveChanges();

                int createdTicketId = ticket.ticket_id;
                TempData["TicketId"] = createdTicketId;

                // Ödeme başarılıysa ViewTicket sayfasına yönlendir
                return RedirectToAction("ViewTicket", "Ticket", new { ticket_id = createdTicketId});
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewTicket()
        {
            int ticketId = (int)TempData["TicketId"];
            int userId = Convert.ToInt32(Session["UserID"]);

            var user = database.Registrations.FirstOrDefault(u => u.customer_id == userId);
            var tickets = database.Tickets.Where(t => t.ticket_id == ticketId).ToList();

            if (user != null && tickets != null)
            {
                var combinedInfo = tickets.Select(ticket => new { TicketInfo = tickets, UserInfo = user }).ToList();
                return View(combinedInfo);
            }

            return RedirectToAction("Index", "Home");
        }




    }
}


// ... (Önceki kodlar)


