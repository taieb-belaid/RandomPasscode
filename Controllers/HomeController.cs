using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;
using System.CodeDom.Compiler;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost("process")]
    public IActionResult Process()
    {
        // Generate a random code
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var stringChars = new char[14];
        var random = new Random();
        for (int i = 0; i < 14; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        Console.WriteLine(finalString);
        //Setting the session
        HttpContext.Session.SetString("Code",finalString);
        int? number = HttpContext.Session.GetInt32("Num"); 
        if (HttpContext.Session.GetInt32("Num") == null){
            HttpContext.Session.SetInt32("Num",0);
        }
        HttpContext.Session.SetInt32("Num",(int)number + 1 );
        Console.WriteLine(number);
        
        
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
