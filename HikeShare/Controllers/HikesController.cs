using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HikeShare.Data;
using HikeShare.Models;

namespace HikeShare.Controllers
{
    public class HikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hikes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hike.ToListAsync());
        }

        // GET: Hikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hike = await _context.Hike
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hike == null)
            {
                return NotFound();
            }

            return View(hike);
        }

        // GET: Hikes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,LengthInMiles")] Hike hike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hike);
        }

        // GET: Hikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hike = await _context.Hike.SingleOrDefaultAsync(m => m.ID == id);
            if (hike == null)
            {
                return NotFound();
            }
            return View(hike);
        }

        // POST: Hikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,LengthInMiles")] Hike hike)
        {
            if (id != hike.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HikeExists(hike.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hike);
        }

        // GET: Hikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hike = await _context.Hike
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hike == null)
            {
                return NotFound();
            }

            return View(hike);
        }

        // POST: Hikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hike = await _context.Hike.SingleOrDefaultAsync(m => m.ID == id);
            _context.Hike.Remove(hike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HikeExists(int id)
        {
            return _context.Hike.Any(e => e.ID == id);
        }
    }
}
