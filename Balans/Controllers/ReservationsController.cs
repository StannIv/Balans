using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Balans.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Balans.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var dataorder = _context.Reservations
                                    .Include(o => o.Clients)
                                    .Include(o => o.Services);
                return View(await dataorder.ToListAsync());
            }
            else
            {
                var dataorder = _context.Reservations
                                    .Include(o => o.Clients)
                                    .Include(o => o.Services)
                                    .Where(x => x.ClientsId == _userManager.GetUserId(User)); ;
                return View(await dataorder.ToListAsync());

            }
           
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        } 
        // GET: Reservations/Create
        public IActionResult Create()
        {
            //ViewData["ClientsId"] = new SelectList(_context.Set<Client>(), "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,Dateregister")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.ClientsId = _userManager.GetUserId(User);
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClientsId"] = new SelectList(_context.Set<Client>(), "Id", "", reservation.ClientsId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServiceId);
            return View(reservation);
        }
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            //ViewData["ClientsId"] = new SelectList(_context.Set<Client>(), "Id", "Id", reservation.ClientsId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServiceId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceId,Dateregister")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            //ViewData["ClientsId"] = new SelectList(_context.Set<Client>(), "Id", "Id", reservation.ClientsId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServiceId);
            return View(reservation);
        }
        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
