using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class OrderDetailController : Controller
    {
        IMemberRepository memberRepository = null;
        IBookRepository bookRepository = null;
        IOrderDetailRepository orderDetailRepository = null;
        public OrderDetailController()
        {
            memberRepository = new MemberRepository();
            bookRepository = new BookRepository();
            orderDetailRepository = new OrderDetailRepository();
        }
        // GET: OrderDetailController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderDetailController/Details/5
        public ActionResult Details(int id)
        {
            var order = orderDetailRepository.GetOrderDetailByID(id);
            return View(order);
        }
        public ActionResult DetailsAdmin(int id)
        {
            var order = orderDetailRepository.GetOrderDetailByID(id);
            return View(order);
        }

        // GET: OrderDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailController/Create
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

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderDetailController/Edit/5
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

        // GET: OrderDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderDetailController/Delete/5
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
