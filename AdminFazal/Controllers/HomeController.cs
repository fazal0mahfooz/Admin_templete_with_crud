using AdminFazal.Db_Context;
using AdminFazal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdminFazal.Controllers
{
    public class HomeController : Controller
    {
       

       
     
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Tablee()
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
            var sh = eobj.Tcruds.ToList();
      
            return View(sh);

        }
        [Authorize]
        [HttpGet]
        public ActionResult Forms()
        {
           
            return View();
        }
        [Authorize] 
        [HttpPost]
        public ActionResult Forms(Class1 cobj)
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
            Tcrud tobj = new Tcrud();

            tobj.Id = cobj.Id;
            tobj.Name = cobj.Name;
            tobj.Email = cobj.Email;
            tobj.Date_Of_Birth = cobj.Date_Of_Birth;
            tobj.Address = cobj.Address;

            if (cobj.Id == 0)
            {
                eobj.Tcruds.Add(tobj);
                eobj.SaveChanges();
            }
            else {
                eobj.Entry(tobj).State = System.Data.Entity.EntityState.Modified;
                eobj.SaveChanges();
            }



            return RedirectToAction("Tablee" , "home");
            
        }
        [Authorize]
        public ActionResult Edit(int Id)
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
           
            Class1 cobj = new Class1();

            var editi = eobj.Tcruds.Where(m => m.Id == Id).First();

            cobj.Id = editi.Id;
            cobj.Name = editi.Name;
            cobj.Email = editi.Email;
            cobj.Date_Of_Birth = editi.Date_Of_Birth;
            cobj.Address = editi.Address;

            return View("Forms" , cobj);

        }
        [Authorize]
        public ActionResult Delete(int Id)
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
            var del = eobj.Tcruds.Where(m => m.Id == Id).First();
            eobj.Tcruds.Remove(del);
            eobj.SaveChanges();
            return RedirectToAction("Tablee");


        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Lclass cobj)
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
            var re = eobj.logins.Where(m => m.Email == cobj.Email).FirstOrDefault();

            if (re == null)
            {
                TempData["msg"] = "Email not Found!";
            }
            else {

                if (re.Email == cobj.Email && re.Password == cobj.Password)
                {
                    FormsAuthentication.SetAuthCookie(re.Email, false);
                    Session["email"] = re.Email;
                    return RedirectToAction("Index");
                
                }
                else
                {

                    TempData["wrong"] = "Email and Password does'nt matched ! ";
                    return View();

                }
             

            }
            return View();


        }


        public ActionResult Signup()
        {

            return View();

        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }

        [HttpPost]
        public ActionResult Signup(Lclass cobj)
        {
            FazalProjEntities1 eobj = new FazalProjEntities1();
            login tobj = new login();
            tobj.Id = cobj.Id;
            tobj.Name = cobj.Name;
            tobj.Email = cobj.Email; 
            tobj.Password = cobj.Password;


            eobj.logins.Add(tobj);
                eobj.SaveChanges();
           
               
               
          
            return RedirectToAction("Login");
        }


    
}
}