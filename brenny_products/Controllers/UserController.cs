using brenny_products.Models;
using Microsoft.AspNetCore.Session;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace brenny_products.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor ses;
        public UserController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _context = db;
            ses = httpContextAccessor;
        }
        public IActionResult Index(int? page)
        {
            int pagesize = 8, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = _context.Products.ToList().OrderBy(x => x.Name);
            IPagedList<Product> stu = (IPagedList<Product>)list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }
        //login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //login
        [HttpPost]
        public IActionResult Login(User user)
        {

            User usr = _context.Users.Where(m => m.Email == user.Email && m.Password == user.Password).SingleOrDefault();

            if (usr != null)

            {

                ViewBag.Message = "Success full login";
                ses.HttpContext.Session.SetString("UserId", "Users.Name");
                return RedirectToAction("Index");
            }

            else

            {

                ViewBag.Message = "Invalid login detail.";

            }

            return View();



        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user, List<IFormFile> imgfile)
        {
//HERE THE USER NEEDS TO SIGN UP AN UPLOAD HIS/HER IMAGE
            User usr = _context.Users.Where(m => m.Email == user.Email).FirstOrDefault();

            if (usr != null)

            {

                ViewBag.Message = "The email already exists";
            }
            else
            {
                string path = Uploading(imgfile);
                if (path.Equals("-1"))
                {
                    ViewBag.error = "image not uploaded";
                }
                else
                {
                    User us = new User();
                    us.Email = user.Email;
                    us.Password = user.Password;
                    us.Image = user.Image;
                    us.Contact = user.Contact;
                    _context.Users.Add(us);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }

               
            }
            return View();

        }

        public string Uploading(List<IFormFile> imgfile)
        {
            try
            {
                if (imgfile.Count > 0)
                {
                    foreach (var file in imgfile)
                    {
                        string filename = file.FileName;
                        filename = Path.GetFileName(filename);
                        string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", filename);
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        file.CopyToAsync(stream);
                    }
                    ViewBag.Message = "FILES ULOADED SUCCESSFULLY";
                }

            }
            catch (Exception e)
            {
                ViewBag.Message = "error uploading the files";
            }
            return null; // you can change the null to anything else also.


        }


        [HttpGet]
        public IActionResult ViewCategory(int? page)
        {
            int pagesize = 8, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = _context.Categories.Where(x => x.Status == 1).OrderByDescending(x => x.CategoryId).ToList();
            //var categories = Category.list.ToPagedList(page ?? 1, 3);
            IPagedList<Category> stu = (IPagedList<Category>)list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }

        public IActionResult Ads( int?id,int? page)
        {
            int pagesize = 8, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = _context.Products.Where(x => x.CategoryId == id).OrderByDescending(x => x.ProductId).ToList();
            //var categories = Category.list.ToPagedList(page ?? 1, 3);
            IPagedList<Product> stu = (IPagedList<Product>)list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }

        public IActionResult ViewAd(int? id )
        {
            ViewAdModel vam = new ViewAdModel();
            Product pr = _context.Products.Where(x => x.ProductId == id).SingleOrDefault();
            vam.Pro_Id = pr.ProductId;
            vam.Pro_name = pr.Name;
            vam.Pro_Price = pr.Price;
            vam.Pro_Image= pr.Image;
            vam.Pro_Description = pr.Description;

            Category ct = _context.Categories.Where(x => x.CategoryId == pr.CategoryId).SingleOrDefault();
            vam.Cat_name = ct.Name;


            User u = _context.Users.Where(x => x.Userid == pr.ProductId).SingleOrDefault();
            vam.U_name = u.Name;
            vam.U_email = u.Email;
            vam.U_image = u.Image;
            vam.U_contact = u.Contact;

            return View();
        }

        public IActionResult DeleteAd(int? id)
        {
            Product p=_context.Products.Where(x=>x.ProductId==id).SingleOrDefault();
            _context.Products.Remove(p);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


    } 
}
