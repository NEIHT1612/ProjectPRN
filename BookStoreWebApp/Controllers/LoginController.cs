using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
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
            if (ModelState.IsValid)
            {
                var _member = memberRepository.CheckAccount(member.Email, member.Password);
                if (_member != null)
                {
                    HttpContext.Session.SetString("MemberId", _member.MemberId.ToString());
                    return RedirectToAction("List", "Book");
                }
                else if (member.Email.Equals("admin", StringComparison.OrdinalIgnoreCase) && member.Password.Equals("123"))
                {
                    HttpContext.Session.SetString("admin", "admin");
                    return RedirectToAction("Index", "Member");
                }
                else
                {
                    ViewBag.Message = "Email or Password is wrong !!!";
                    return View(member);
                }
            }
            else
            {
                ViewBag.Message = "Invalid input data.";
                return View(member);
            }
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
            HttpContext.Session.Remove("MemberId");
            return RedirectToAction(nameof(Login));
        }
    }
}
