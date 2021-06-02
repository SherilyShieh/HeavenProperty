using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeavenProperty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeavenProperty.Controllers
{

    public class SellerController : Controller
    {


        private readonly HeavenPropertyContext _context;

        public SellerController()
        {
            this._context = new HeavenPropertyContext();
        }
        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Seller seller)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Response.Cookies.Append("IsRegister", "true");

                try
                {
                    this._context.Sellers.Add(seller);
                    var result = await this._context.SaveChangesAsync();
                    HttpContext.Response.Cookies.Append("IsSuccess", "true");
                    HttpContext.Response.Cookies.Append("Messages", "");
                    HttpContext.Response.Cookies.Append("Info", "Congratulations, Registration Success!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                } catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("Info", "Woops, Registration Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        public IActionResult Result()
        {
            string flag = HttpContext.Request.Cookies["IsSuccess"];
            ViewBag.IsSuccess = flag.Equals("true");
            ViewBag.ErrorMsg = HttpContext.Request.Cookies["Messages"];
            ViewBag.IsRegister = HttpContext.Request.Cookies["IsRegister"].Equals("true");
            ViewBag.Info = HttpContext.Request.Cookies["Info"];
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = this._context.Sellers.First(t => t.Email.Equals(loginViewModel.Email) && t.Password.Equals(loginViewModel.Password));
                    if (query != null)
                    {
                        string currentUser = JsonConvert.SerializeObject(query);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        await Task.Delay(0);
                        return Redirect(url: "/Seller/Profile/" + query.Id);
                    } else
                    {
                        ViewBag.LoginError = "Account or password incorrect, please try again!";
                        
                    }
                } catch (Exception ex)
                {

                    ViewBag.LoginError = "Some Error occur:" + ex.Message + " , please try again!";
                    
                }

            }
            return View();


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult LogOut()
        {
            //HttpContext.
            HttpContext.Response.Cookies.Delete("CurrentUser");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Profile(string id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/Profile/" + cur.Id);
                }
                else
                {

                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    ViewBag.currentUser = cur;
                    return View();
                }
            } catch (Exception ex)
            {
                ViewBag.cookieError = "Some Error occur:" + ex.Message + "!";
                return View();
            }
        }

        public IActionResult Property(String id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/Property/" + cur.Id);
                }
                else
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    List<Property> list = this._context.Properties.Where(l => l.Seller_Id == cur.Id).ToList<Property>();
                    
                    ViewBag.properties = list;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.cookieError = "Some Error occur:" + ex.Message + "!";
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdateName(string id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/UpdateName/" + cur.Id);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.Cookies.Append("IsSuccess", "false");
                HttpContext.Response.Cookies.Append("Messages", ex.Message);
                HttpContext.Response.Cookies.Append("IsRegister", "false");
                HttpContext.Response.Cookies.Append("Info", "Woops, the current login user is invalid, please re- log in!");
                return RedirectToAction(nameof(SellerController.Result), "Seller");
            }
            //return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateName(UpdateNameViewModel seller, string id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    } else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Seller ss = this._context.Sellers.First<Seller>(t => t.Id == cur.Id);
                        ss.FirstName = seller.FirstName;
                        ss.LastName = seller.LastName;
                        var result = await this._context.SaveChangesAsync();
                        cur.FirstName = seller.FirstName;
                        cur.LastName = seller.LastName;
                        string currentUser = JsonConvert.SerializeObject(cur);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        return RedirectToAction(nameof(SellerController.Profile), "Seller");
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Update Name failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdateEmail(String id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/UpdateEmail/" + cur.Id);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.Cookies.Append("IsSuccess", "false");
                HttpContext.Response.Cookies.Append("Messages", ex.Message);
                HttpContext.Response.Cookies.Append("IsRegister", "false");
                HttpContext.Response.Cookies.Append("Info", "Woops, the current login user is invalid, please re- log in!");
                return RedirectToAction(nameof(SellerController.Result), "Seller");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmail(UpdateEmailViewModel seller, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    }
                    else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Seller ss = this._context.Sellers.First<Seller>(t => t.Id == cur.Id);
                        ss.Email = seller.Email;
                        var result = await this._context.SaveChangesAsync();
                        cur.Email = seller.Email;
                        string currentUser = JsonConvert.SerializeObject(cur);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        return RedirectToAction(nameof(SellerController.Profile), "Seller");
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Update Email Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdatePhone(string id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/UpdatePhone/" + cur.Id);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.Cookies.Append("IsSuccess", "false");
                HttpContext.Response.Cookies.Append("Messages", ex.Message);
                HttpContext.Response.Cookies.Append("IsRegister", "false");
                HttpContext.Response.Cookies.Append("Info", "Woops, the current login user is invalid, please re- log in!");
                return RedirectToAction(nameof(SellerController.Result), "Seller");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePhone(UpdatePhoneViewModel seller, String id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    }
                    else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Seller ss = this._context.Sellers.First<Seller>(t => t.Id == cur.Id);
                        ss.Phone = seller.Phone;
                        var result = await this._context.SaveChangesAsync();
                        cur.Phone = seller.Phone;
                        string currentUser = JsonConvert.SerializeObject(cur);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        return RedirectToAction(nameof(SellerController.Profile), "Seller");
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Update Phone Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdateAddress(string id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/UpdateAddress/" + cur.Id);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.Cookies.Append("IsSuccess", "false");
                HttpContext.Response.Cookies.Append("Messages", ex.Message);
                HttpContext.Response.Cookies.Append("IsRegister", "false");
                HttpContext.Response.Cookies.Append("Info", "Woops, the current login user is invalid, please re- log in!");
                return RedirectToAction(nameof(SellerController.Result), "Seller");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(UpdateAddressViewModel seller, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    }
                    else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Seller ss = this._context.Sellers.First<Seller>(t => t.Id == cur.Id);
                        ss.Address = seller.Address;
                        var result = await this._context.SaveChangesAsync();
                        cur.Address = seller.Address;
                        string currentUser = JsonConvert.SerializeObject(cur);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        return RedirectToAction(nameof(SellerController.Profile), "Seller");
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Update Address Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UpdatePwd(string id)
        {
            try
            {
                string flag = "";
                HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                ViewBag.currentUser = null;

                if (string.IsNullOrEmpty(flag))
                {
                    return RedirectToAction(nameof(SellerController.Login), "Seller");
                }
                else if (string.IsNullOrEmpty(id))
                {
                    Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                    return Redirect(url: "/Seller/UpdatePwd/" + cur.Id);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.Cookies.Append("IsSuccess", "false");
                HttpContext.Response.Cookies.Append("Messages", ex.Message);
                HttpContext.Response.Cookies.Append("IsRegister", "false");
                HttpContext.Response.Cookies.Append("Info", "Woops, the current login user is invalid, please re- log in!");
                return RedirectToAction(nameof(SellerController.Result), "Seller");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePwd(UpdatePwdViewModel seller)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    }
                    else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Seller ss = this._context.Sellers.First<Seller>(t => t.Id == cur.Id);
                        ss.Password = seller.Password;
                        var result = await this._context.SaveChangesAsync();
                        cur.Password = seller.Password;
                        string currentUser = JsonConvert.SerializeObject(cur);
                        HttpContext.Response.Cookies.Append("CurrentUser", currentUser);
                        return RedirectToAction(nameof(SellerController.Profile), "Seller");
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Update Password Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPropertyViewModel property)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string flag = "";
                    HttpContext.Request.Cookies.TryGetValue("CurrentUser", out flag);
                    if (string.IsNullOrEmpty(flag))
                    {
                        return RedirectToAction(nameof(SellerController.Login), "Seller");
                    }
                    else
                    {
                        Seller cur = JsonConvert.DeserializeObject<Seller>(flag);
                        Property p = new Property
                        {
                            Title = property.Title,
                            Location = property.Location,
                            Rooms = property.Rooms,
                            BathRooms = property.BathRooms,
                            CarParkings = property.CarParkings,
                            Type = property.Type,
                            FloorArea = property.FloorArea,
                            LandArea = property.LandArea,
                            RV = property.RV,
                            Email = property.Email,
                            Seller_Id = cur.Id

                        };
                        this._context.Properties.Add(p);
                        await this._context.SaveChangesAsync();
                        return RedirectToAction(nameof(SellerController.Property), "Seller");
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Woops, Add Property Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (DeleteViewModel deleteViewModel)
        {
            var ss = this._context.Properties.Find(deleteViewModel.PropertyId);

            this._context.Properties.Remove(ss);

            await this._context.SaveChangesAsync();
            return RedirectToAction(nameof(SellerController.Property), "Seller");
        }

        [HttpGet]
        public IActionResult ForgetPwd()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPwd(ForgetPwdViewModel forget)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var query = this._context.Sellers.Where(t => t.Email.Equals(forget.Email)).First();
                    if (query != null)
                    {
                        query.Password = forget.Password;
                        await this._context.SaveChangesAsync();
                        HttpContext.Response.Cookies.Append("IsSuccess", "true");
                        HttpContext.Response.Cookies.Append("Messages", "");
                        HttpContext.Response.Cookies.Append("IsRegister", "false");
                        HttpContext.Response.Cookies.Append("Info", "Password Reset Successful!");
                        return RedirectToAction(nameof(SellerController.Result), "Seller");
                    }
                } catch (Exception ex)
                {
                    HttpContext.Response.Cookies.Append("IsSuccess", "false");
                    HttpContext.Response.Cookies.Append("Messages", ex.Message);
                    HttpContext.Response.Cookies.Append("IsRegister", "false");
                    HttpContext.Response.Cookies.Append("Info", "Password Reset Failed!");
                    return RedirectToAction(nameof(SellerController.Result), "Seller");
                }
            }
                
            return View();
        }



    }




}
