using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}

