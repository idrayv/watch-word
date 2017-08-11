using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;

namespace WatchWord.Controllers
{
    public abstract class MainController : Controller
    {
        private const string dbError = "Database query error. Please try later.";

        protected static void AddServerError(BaseResponseModel model, Exception ex)
        {
            model.Success = false;
            Debug.Write(ex.ToString());
            model.Errors.Add(dbError);
        }

        protected static void AddCustomError(BaseResponseModel model, string text)
        {
            model.Success = false;
            Debug.Write(text);
            model.Errors.Add(text);
        }
    }
}