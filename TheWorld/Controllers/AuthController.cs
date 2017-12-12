using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;
        private UserManager<WorldUser> _userManager;

        public AuthController(SignInManager<WorldUser> signInManager, UserManager<WorldUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (vm.Email != null)
            {
                if (await _userManager.FindByEmailAsync(vm.Email) == null)
                {
                    var user = new WorldUser()
                    {
                        UserName = vm.Username,
                        Email = vm.Email
                    };

                    await _userManager.CreateAsync(user, vm.Password);
                }
            }

            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                      vm.Password,
                                                                      true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    { 
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(LoginViewModel vm, string returnUrl)
        {
            if (await _userManager.FindByEmailAsync("newloganskidmore91@gmail.com") == null)
            {
                var user = new WorldUser()
                {
                    UserName = "newloganskidmore",
                    Email = "newloganskidmore91@gmail.com"
                };

                await _userManager.CreateAsync(user, "P@ssw1rd!");
            }

            return View();

            //if (ModelState.IsValid)
            //{
            //    var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
            //                                                          vm.Password,
            //                                                          true, false);
            //    if (signInResult.Succeeded)
            //    {
            //        if (string.IsNullOrWhiteSpace(returnUrl))
            //        {
            //            return RedirectToAction("Trips", "App");
            //        }
            //        else
            //        {
            //            return Redirect(returnUrl);
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Username or password incorrect");
            //    }
            //}

            //return View();
        }



        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}
