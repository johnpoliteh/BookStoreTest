using BookStoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreTest.Util;


namespace BookStoreTest.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();


        // GET: Home
        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все полученные объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление

            return View();
        }
        //Контекст запроса
        public string Context()
        {

            //Отправка ответа, содержимое передается в качестве параметра
            HttpContext.Response.Write("<h1>Ответ от сервера</h1>");

            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            //Переадресация
            if (id > 3)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>ПростоТекст</h2>"); 
        }

        public ActionResult GetImage()
        {
            string path = "../Images/View 0_3.jpg";
            return new ImageResult(path);
            //return new EmptyResult();
        }

        //Отправка файла
        public FileResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/Сводная_спецификация2.docx");
            // Тип файла - content-type
            string file_type = "docx";
            // Имя файла - необязательно
            string file_name = "Сводная_спецификация2.docx";
            return File(file_path, file_type, file_name);
        }


    
    }
}