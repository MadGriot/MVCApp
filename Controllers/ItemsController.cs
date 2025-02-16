using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MVCAppDbContext context;
        public ItemsController(MVCAppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Overview()
        {
            Item item = new Item() { Name = "keyboard" };
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        public async Task<IActionResult> Index()
        {
            List<Item> item = await context.Items.ToListAsync();
            return View(item);
        }
    }
}
