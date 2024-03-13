using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreWebApp.Controllers
{
    public class BookController : Controller
    {
        IBookRepository bookRepository;
        IMemberRepository memberRepository;
        public BookController() 
        { 
            bookRepository = new BookRepository(); 
            memberRepository = new MemberRepository();
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.GetBooks();           
            return View(books);
        }

        public ActionResult List(int id)
        {
            var books = bookRepository.GetBooks();
            var member = memberRepository.GetMemberByID(id);
            TempData["MemberData"] = JsonConvert.SerializeObject(member);
            return View(books);
        }
        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetBookByID(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bookRepository.InsertBook(book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetBookByID(id);
            if(book == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            try
            {
                if (id != book.BookId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    bookRepository.UpdateBook(book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetBookByID(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bookRepository.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
