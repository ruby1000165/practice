using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBook.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {   //ViewBag.BookClassItem1 = codeService1.GetBookClass1() 這行只需要寫一次，可以全部都用GetBookClass1
            Models.CodeService codeService1 = new Models.CodeService();
            ViewBag.BookClassItem1 = codeService1.GetBookClass1();
            Models.CodeService codeService2 = new Models.CodeService();
            ViewBag.BookClassItem2 = codeService2.GetBookClass2();
            Models.CodeService codeService3 = new Models.CodeService();
            ViewBag.BookClassItem3 = codeService3.GetBookClass3();
            return View();
        }

        [HttpPost()] 
        public ActionResult Index(Models.BookSearchArg arg)
        {
            Models.CodeService codeService1 = new Models.CodeService();
            ViewBag.BookClassItem1 = codeService1.GetBookClass1();

            Models.CodeService codeService2 = new Models.CodeService();
            ViewBag.BookClassItem2 = codeService2.GetBookClass2();

            Models.CodeService codeService3 = new Models.CodeService();
            ViewBag.BookClassItem3 = codeService3.GetBookClass3();

            Models.BookService bookService = new Models.BookService();
            ViewBag.SearchResult = bookService.GetBookByCondition(arg);


            return View();
        }
    }
}