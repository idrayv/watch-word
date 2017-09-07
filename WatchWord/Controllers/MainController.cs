using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WatchWord.Models;

namespace WatchWord.Controllers
{
    public abstract class MainController : Controller
    {
        private const string dbError = "Server error. Please try again later.";

        protected static void AddServerError(BaseResponseModel model, Exception ex)
        {
            model.Success = false;
            Trace.TraceError(ex.ToString());
            Trace.TraceError(ex.StackTrace);
            model.Errors.Add(dbError);
        }

        protected static void AddCustomError(BaseResponseModel model, string text)
        {
            model.Success = false;
            Trace.TraceError(text);
            model.Errors.Add(text);
        }
    }
}