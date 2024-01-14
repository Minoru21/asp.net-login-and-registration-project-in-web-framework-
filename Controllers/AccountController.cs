using five_mvc_project.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;



namespace five_mvc_project.Controllers
{
    public class AccountController : Controller
    {
        loginEntities db = new loginEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tbluser ur)
        {
            if(ModelState.IsValid)
            {
                if(db.tblusers.Any(x => x.email == ur.email))
                {
                    ViewBag.Message = "Email Already Exits";
                }
                else
                {
                    db.tblusers.Add(ur);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registered successfuly') </script>");
                    return RedirectToAction("login");

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(loginmodel model)
        {
            var query = db.tblusers.SingleOrDefault(m  => m.email == model.email && m.password == model.password);
            if(query != null)
            {
                
                Response.Write("<script>alert('Loged  successfuly') </script>");
                ModelState.Clear();
                TempData["Username"] = query.username;
                return RedirectToAction("Index");


            }
            else
            {
                Response.Write("<script>alert('Invalid credentials') </script>");

            }
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
            
        }



    }
}