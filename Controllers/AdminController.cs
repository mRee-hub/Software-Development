using GoTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoTicket.Controllers
{
    public class AdminController : Controller
    {
        GoTicketEntities db = new GoTicketEntities();
        // GET: Admin

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var AdminList = db.Admins.ToList();
            if (ModelState.IsValid)
            {
                foreach (var item in AdminList)
                {
                    if (admin.admin_username == item.admin_username && admin.admin_password == item.admin_password)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewData["HataMesaji"] = "Formu doğru bir şekilde doldurunuz.";
                    }
                }
            }
            return View();
        }
        public ActionResult Index()
        {
            var degerler = db.Registrations.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult NewCustomer()
        {

            return View();

        }

        [HttpPost]
        public ActionResult NewCustomer(Registration r)
        {
            db.Registrations.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");

        }

        public ActionResult DeleteCustomer(int id)
        {
            var b = db.Registrations.Find(id);
            db.Registrations.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");

        }

        public ActionResult TakeCustomerInfo(int id)
        {
            var b = db.Registrations.Find(id);
            return View("TakeCustomerInfo", b);
        }

        public ActionResult UpdateCustomer(Registration r)
        {
            var blg = db.Registrations.Find(r.customer_id);

            if (blg != null)
            {
                blg.customer_fname = r.customer_fname;
                blg.customer_lname = r.customer_lname;
                blg.customer_pnumber = r.customer_pnumber;
                blg.customer_address = r.customer_address;
                blg.customer_email = r.customer_email;
                blg.customer_password = r.customer_password;
                db.SaveChanges();
            }

            return View(r);

        }

        public ActionResult Vehicles()
        {
            var degerler = db.Vehicles.ToList();
            return View(degerler);

        }

        [HttpGet]
        public ActionResult NewVehicle()
        {

            return View();

        }

        [HttpPost]
        public ActionResult NewVehicle(Vehicle v)
        {
            db.Vehicles.Add(v);
            db.SaveChanges();
            return RedirectToAction("Vehicles", "Admin");

        }

        public ActionResult DeleteVehicle(int id)
        {
            var v = db.Vehicles.Find(id);
            db.Vehicles.Remove(v);
            db.SaveChanges();
            return RedirectToAction("Vehicles", "Admin");

        }

        public ActionResult TakeVehicleInfo(int id)
        {
            var v = db.Vehicles.Find(id);
            return View("TakeVehicleInfo", v);
        }

        public ActionResult UpdateVehicle(Vehicle v)
        {
            var blg = db.Vehicles.Find(v.vehicle_id);

            if (blg != null)
            {
                blg.vehicle_type = v.vehicle_type;
                blg.vehicle_condition = v.vehicle_condition;
                blg.vehicle_attribute = v.vehicle_attribute;
                blg.departure_location = v.departure_location;
                blg.arrival_location = v.arrival_location;
                blg.departure_date = v.departure_date;
                blg.arrival_date = v.arrival_date;
                blg.total_amount = v.total_amount;
                db.SaveChanges();
            }

            return View(v);

        }

        public ActionResult Tickets()
        {
            var degerler = db.Tickets.ToList();
            return View(degerler);

        }

        [HttpGet]
        public ActionResult NewTicket()
        {

            return View();

        }

        [HttpPost]
        public ActionResult NewTicket(Ticket t)
        {

            if (ModelState.IsValid)
            {

                db.Tickets.Add(t);
                db.SaveChanges();
                return RedirectToAction("Tickets", "Admin");

            }
            else
            {
                return View();
            }

        }

        public ActionResult DeleteTicket(int id)
        {
            var t = db.Tickets.Find(id);
            db.Tickets.Remove(t);
            db.SaveChanges();
            return RedirectToAction("Tickets", "Admin");

        }
        public ActionResult TakeTicketInfo(int id)
        {
            var t = db.Tickets.Find(id);
            return View("TakeTicketInfo", t);
        }

        public ActionResult UpdateTicket(Ticket t)
        {
            var blg = db.Tickets.Find(t.vehicle_id);

            if (blg != null)
            {
                blg.vehicle_type = t.vehicle_type;
                blg.deperture_date = t.deperture_date;
                blg.arrival_date = t.arrival_date;
                db.SaveChanges();
            }

            return View(t);

        }
    }
}