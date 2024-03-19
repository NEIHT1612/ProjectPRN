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
        IOrderTblRepository orderTblRepository;
        IOrderDetailRepository orderDetailRepository;
        ICategoryRepository categoryRepository;

        public BookController() 
        { 
            bookRepository = new BookRepository(); 
            memberRepository = new MemberRepository();
            orderTblRepository = new OrderTblRepository();
            orderDetailRepository = new OrderDetailRepository();
            categoryRepository = new CategoryRepository();
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.GetBooks();           
            return View(books);
        }

        public ActionResult List()
        {
            var books = bookRepository.GetBooks();
            var categories = categoryRepository.GetCategories();
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(string searchBook, string priceFilter, string CategoryId)
        {
            var bookList = new List<Book>();

            if (!string.IsNullOrEmpty(searchBook) && string.IsNullOrEmpty(priceFilter))
            {
                bookList = bookRepository.GetBooksByName(searchBook);
            }
            else if (string.IsNullOrEmpty(searchBook) && !string.IsNullOrEmpty(priceFilter))
            {
                string[] priceRange = priceFilter.Split('-');
                if (priceRange.Length == 2)
                {
                    decimal minPrice = decimal.Parse(priceRange[0]);
                    decimal maxPrice = decimal.Parse(priceRange[1]);
                    bookList = bookRepository.GetBooksByPrice(minPrice, maxPrice);
                }
                else
                {
                    bookList = (List<Book>)bookRepository.GetBooks();
                }
            }
            else if (!string.IsNullOrEmpty(searchBook) && !string.IsNullOrEmpty(priceFilter))
            {
                string[] priceRange = priceFilter.Split('-');
                if (priceRange.Length == 2)
                {
                    decimal minPrice = decimal.Parse(priceRange[0]);
                    decimal maxPrice = decimal.Parse(priceRange[1]);
                    bookList = bookRepository.GetBooksByNameAndPrice(searchBook, minPrice, maxPrice);
                }
                else
                {
                    bookList = (List<Book>)bookRepository.GetBooks();
                }
            }
            else
            {
                bookList = (List<Book>)bookRepository.GetBooks();
            }
            if (!string.IsNullOrEmpty(CategoryId))
            {
                if (CategoryId.Equals("All"))
                {
                    bookList = (List<Book>)bookRepository.GetBooks();
                }
                else
                {
                    int categoryId = int.Parse(CategoryId);
                    bookList = bookRepository.GetBooksByCategory(categoryId);
                }
            }
            else
            {
                bookList = (List<Book>)bookRepository.GetBooks();

            }
            return View(bookList);
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
            return View(book);
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

        public ActionResult Buy(int id)
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

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(Book book, int bookId, int quantity)
        {
            var memberId = int.Parse(HttpContext.Session.GetString("MemberId"));
            var member = memberRepository.GetMemberByID(memberId);
            var book1 = bookRepository.GetBookByID(bookId);
            try
            {
                if (ModelState.IsValid)
                {
                    OrderTbl orderTbl = new OrderTbl();
                    orderTbl.OrderDate = DateTime.Now;
                    orderTbl.MemberId = member.MemberId;
                    orderTbl.Freight = book1.Price;
                    orderTblRepository.InsertOrder(orderTbl);

                    int orderid = 0;
                    List<OrderTbl> orders = (List<OrderTbl>)orderTblRepository.GetOrders();
                    if (orders.Count > 0)
                    {
                        orderid = orders[orders.Count - 1].OrderId;
                    }

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = orderid;
                    orderDetail.BookId = book1.BookId;
                    orderDetail.OrderQuantity = quantity;
                    orderDetail.TotalPrice = quantity * book1.Price;
                    orderDetailRepository.InsertOrderDetail(orderDetail);

                }
                return RedirectToAction("Index", "Cart", new {memberId = member.MemberId});
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(book);
            }
        }
    }
}
