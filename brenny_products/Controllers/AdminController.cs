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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor ses;
        public AdminController(ApplicationDbContext db,IHttpContextAccessor httpContextAccessor)
        {
            _context = db;
            ses = httpContextAccessor;
        }
        //login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //login
        [HttpPost]
        public IActionResult Index(Admin admin)
        {

            Admin adm = _context.Admins.Where(m => m.Username == admin.Username && m.Password == admin.Password).SingleOrDefault();

            if (adm != null)

            {

                ViewBag.Message = "Success full login";
                ses.HttpContext.Session.SetString("AdminId", "Admins.Name");
                return RedirectToAction("ViewCategory");
            }

            else

            {

                ViewBag.Message = "Invalid login detail.";

            }

            return View();



        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Admin admin)
        {

            Admin adm = _context.Admins.Where(m => m.Username == admin.Username).SingleOrDefault();

            if (adm != null)

            {

                ViewBag.Message = "The username already exists";


            }

            else

            {
                Admin ad = new Admin();
                ad.Username = admin.Username;
                ad.Password = admin.Password;
                _context.Admins.Add(ad);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();



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


        [HttpGet]
        public IActionResult CreateCategory()
        {
       if(ses.HttpContext.Session.GetString("AdminId")==null)

            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category cat,IFormFile imgfiles)
        {
            try
            {
                if (imgfiles != null)
                {
                    string filename = imgfiles.FileName;
                    filename = Path.GetFileName(filename);
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", filename);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    imgfiles.CopyToAsync(stream);
                    string uploadedDbPath = "uploads\\" + filename;
                    //var uploadingImage = new UploadImage() { 
                    cat.Image = uploadedDbPath;

                    Category ct = new Category();
                    ct.Name = cat.Name;
                    ct.Image = cat.Image;

                    ct.Status = 1;
                    ct.AdminId = Convert.ToInt32(ses.HttpContext.Session.GetString("AdminId"));
                    _context.Categories.Add(ct);
                    _context.SaveChanges();
                    ViewBag.Message = "FILES UPLOADED SUCCESSFULLY";

                    return RedirectToAction("ViewCategory");


                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "error uploading the files";
            }

            return View();

        }




    }

        
           

    }

