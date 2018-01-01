using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PreProcessorTest.Messages;
using PreProcessorTest.Messages.Commands;
using PreProcessorTest.Messages.Queries;
using PreProcessorTest.Models;

namespace PreProcessorTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            string result = "Result initialized but not modified.";

            CommonResponse commandResult = await _mediator.Send(new MyCommand());
            CommonResponse queryResult = await _mediator.Send(new MyQuery());

            ViewData["CommandResult"] = commandResult.Succeeded.ToString();
            ViewData["QueryResult"] = commandResult.Succeeded.ToString();
            

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
