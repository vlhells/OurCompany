using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_1.Admin.Models;
using Practice_1.Areas.Admin.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Practice_1.Admin.Controllers
{
    [Area("Admin")]
	//[Authorize(Roles = "Admin")]
	[Authorize(Policy = "AdminArea")]
	public class HomeController: Controller
	{
		//IEnumerable<Role> roles = new List<Role>
		//{
		//	new Role("User"),
		//	new Role("Admin")
		//};

		ApplicationContext db;

		public HomeController(ApplicationContext db)
		{
			this.db = db;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await db.Users.ToListAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			//ViewBag.Roles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roles, "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(User user)
		{
			db.Users.Add(user); // = SQL INSERT;
			await db.SaveChangesAsync(); // Добавляет данные в базу.
			return RedirectToAction("Index");
		}

		[HttpPost]//!!!
		public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
				if (user != null)
				{
					db.Users.Remove(user);
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id != null)
			{
				User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
				if (user != null)
					return View(user);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(User user)
		{
			db.Users.Update(user);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}


	}
}
