using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Regents.Controllers
{
    public class BaseController : Controller
    {
	    public bool IsLoggedIn()
	    {
		    return User.Identity.IsAuthenticated;
	    }


	    public string UserId => User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
	   
	    public string UserName => User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

		//public IActionResult Error(ErrorModel errorModel)
  //      {
  //          return View(nameof(Error), errorModel);
  //      }

  //      public IActionResult Error(string errorMsg, string redirectUrl)
  //      {
  //          return Error(new ErrorModel()
  //          {
  //              ErrorMessage = errorMsg,
  //              RedirectUrl = redirectUrl
  //          });
  //      }
    }
}