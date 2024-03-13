using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStoreWebApp.Controllers
{
    public class LoginController : Controller
    {
        IMemberRepository memberRepository = null;
        public LoginController() => memberRepository = new MemberRepository();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Member member)
        {
            var _member = memberRepository.CheckAccount(member.Email, member.Password);
            if ( _member != null)
            {
                // Lưu trữ đối tượng Member trong TempData dưới dạng chuỗi JSON
                TempData["MemberData"] = JsonConvert.SerializeObject(_member);
                return RedirectToAction("Index", "Home");
            }
            if(member.Email.Equals("admin") && member.Password.Equals("123"))
            {
                return RedirectToAction("Index", "Member");
            }
            ViewBag.Message = "Email or Password is wrong !!!";
            return View(member);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (memberRepository.GetMemberByEmail(member.Email) == null)
                    {
                        if (member.Gender.Equals("Male") || member.Gender.Equals("Female"))
                        {
                            memberRepository.InsertMember(member);
                        }
                        else
                        {
                            ViewBag.GenderMessage = "You should choose Male/Female";
                            return View(member);
                        }
                    }
                    else
                    {
                        ViewBag.EmailMessage = "Email is already exist";
                        return View(member);
                    }
                }
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(member);
            }
        }
        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Login));
        }
    }
}
