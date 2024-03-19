using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{

    public class CartController : Controller
    {
        IMemberRepository memberRepository = null;
        IBookRepository bookRepository = null;
        IOrderTblRepository orderTblRepository = null;
        public CartController()
        {
            memberRepository = new MemberRepository();
            bookRepository = new BookRepository();
            orderTblRepository = new OrderTblRepository();
        }
        // GET: CartController
        public ActionResult Index(int memberId)
        {
            var carts = orderTblRepository.GetOrdersByMemberId(memberId);
            return View(carts);
        }

        public ActionResult List()
        {
            int memberId = int.Parse(HttpContext.Session.GetString("MemberId"));
            var carts = orderTblRepository.GetOrdersByMemberId(memberId);
            return View(carts);
        }
        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
