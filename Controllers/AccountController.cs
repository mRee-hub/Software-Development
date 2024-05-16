using System;
using System.Linq;
using System.Web.Mvc;
using GoTicket.Models;
using System.Security.Cryptography;
using System.Text;

namespace GoTicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly GoTicketEntities _database;

        public AccountController()
        {
            _database = new GoTicketEntities();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(); // Boş form sayfasını göster
        }

        [HttpPost]
            public ActionResult Register(Registration customer)
            {
                if (ModelState.IsValid)
                {
                string hashedPassword = HashPassword(customer.customer_password);


                // Hashlenmiş şifreyi veritabanına kaydetme
                var newCustomer = new Registration
                    {
                        customer_fname = customer.customer_fname,
                        customer_lname = customer.customer_lname,
                        customer_pnumber = customer.customer_pnumber,
                        customer_address = customer.customer_address,
                        customer_email = customer.customer_email,
                        customer_password = hashedPassword // Hashlenmiş şifre
                    };


                    _database.Registrations.Add(newCustomer);
                    _database.SaveChanges();

                

                return RedirectToAction("Index", "Home"); // Kayıt başarılı, anasayfaya yönlendir
                
                
                }

                return View(); // Model doğrulama hatası varsa kayıt sayfasını tekrar göster
            }

            //[HttpGet]
            public ActionResult Login()
            {
              return View();
            }

        [HttpPost]
        public ActionResult Login(Registration user)
        {
            var userInDb = _database.Registrations.FirstOrDefault(u => u.customer_email == user.customer_email);

            if (userInDb != null)
            {
                // Şifre doğrulama
                if (userInDb.customer_password == HashPassword(user.customer_password))
                {
                    TempData["İsim"] = userInDb.customer_fname;
                    TempData["Soyisim"] = userInDb.customer_lname;

                    Session["UserID"] = userInDb.customer_id;
                    // Kullanıcı oturum açtı, gerekli işlemler yapılabilir
                    // Örnek olarak, bir oturum açma kimliği oluşturulabilir
                    // Ve ardından kullanıcı başka bir sayfaya yönlendirilebilir

                    return RedirectToAction("Index", "Home");
                }
            }

            // Giriş başarısız, kullanıcıyı giriş sayfasında tut
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Index", "Home");
            
        }

        private string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }

                    return builder.ToString();
                }
            }
    }
}
