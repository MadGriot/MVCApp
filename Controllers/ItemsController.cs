﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index()
        {
            List<Item> item = await context.Items.Include(s => s.SerialNumber)
                                                    .Include(c =>c.Category)
                                                    .Include(ic => ic.ItemClients)
                                                    .ThenInclude(c => c.Client)
                                                    .ToListAsync();
            return View(item);
        }
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                context.Items.Add(item);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");
            Item item = await context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Item item = await context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Item item = await context.Items.FindAsync(id);
            if (item != null)
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
