using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class TestPageController : Controller {
     private readonly ILogger<TestPageController> _logger;

    public TestPageController(ILogger<TestPageController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Calendar()
    {
        return View();
    }
}