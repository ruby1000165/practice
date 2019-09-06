using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        readonly Models.CodeService codeService = new Models.CodeService();
        readonly Models.BookService bookService = new Models.BookService();

        /// <summary>
        /// 圖書資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public JsonResult GetDropDownListDataBookClass()
        {
            // List<SelectListItem> ClassItem = new List<SelectListItem>() 可以不一定要做這個宣告
            List<SelectListItem> classItem = codeService.GetBookClassName();

            return Json(classItem);
        }

        [HttpPost()]
        public JsonResult GetDropDownListDataUserId()
        {
            List<SelectListItem> userItem = codeService.GetUserName();

            return Json(userItem);
        }

        [HttpPost()]
        public JsonResult GetDropDownListDataCodeName()
        {
            List<SelectListItem> codenameItem = codeService.GetCodeName();

            return Json(codenameItem);
        }    

        /// <summary>
        /// 圖書資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            
            return View("Index");
        }

        [HttpPost()]
        public JsonResult GetBookCondtioin(Models.BookSearchArg arg)
        {
            List<Models.Books>  TableData = bookService.GetBookByCondtioin(arg);

            return Json(TableData);
        }

        /// <summary>
        /// 新增圖書畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertBook()
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            return View(new Models.Books());
        }

        /// <summary>
        /// 新增圖書
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertBook(Models.Books book)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(book.BookBoughtDate);
                    int BookID = bookService.InsertBook(book);
                    return RedirectToAction("BookData", new { BookID = BookID });
                }
                catch
                {
                    Response.Write("<script language=javascript>alert('日期格式錯誤')</script>");
                }
            }
            return View(book);
        }

        /// <summary>
        /// 刪除圖書
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(int BookID)
        {
            try
            {
                bookService.DeleteBook(BookID);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 明細圖書畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult BookData(int BookID)
        {
            Models.Books books = bookService.GetBookDetail(BookID).FirstOrDefault();
            return View(books);
        }

        /// <summary>
        /// 修改圖書畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult UpdateBook(int BookID)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            Models.Books books = bookService.GetBookData(BookID).FirstOrDefault();
            return View(books);
        }

        /// <summary>
        /// 修改圖書存檔
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="books"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult UpdateBook(Models.Books books)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(books.BookBoughtDate);
                    bookService.UpdateBookData(books);
                    return RedirectToAction("BookData", new { BookID = books.BookID });
                }
                catch(Exception ex)
                {
                    Response.Write("<script language=javascript>alert('日期格式錯誤')</script>");
                }
            }
            return View(books);
        }

        /// <summary>
        /// 借閱紀錄畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult BookLendRecord(int BookID)
        {
            ViewBag.LendRecord = bookService.GetBookLendRecord(BookID);
            return View("BookLendRecord");
        }
    }
}