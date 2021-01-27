using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicCollaboration.Data;
using MusicCollaboration.Models;
using MusicCollaboration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using MusicCollaboration.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace MusicCollaboration.Controllers
{
    //You need to be signed in to access this controller
    [Authorize]
    public class CollaborationsController : Controller
    {
        private readonly MusicCollaborationContext _context;
        private UserManager<MusicCollaborationUser> _userManager;
        private IAuthorizationService _authorizationService;

        public CollaborationsController(MusicCollaborationContext context , 
            UserManager<MusicCollaborationUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }


      
        // GET: Collaborations
        public async Task<IActionResult> Index(CollaborationGenre searchGenre, string searchString)
        {
            //LINQ query to select the collaborations
            var collaborations = from m in _context.Collaboration
                                 select m;
            //If searchString parameter contains a string
            if (!String.IsNullOrEmpty(searchString))
            {
                //query modified to filter on the value of the search string
                    //Contains is run on the db, maps to SQL LIKE
                collaborations = collaborations.Where(s => s.Title.Contains(searchString));
            }

            if (searchGenre != 0)
            {
                collaborations = collaborations.Where(x => x.Genre == searchGenre);
            }
                

            return View(await collaborations.ToListAsync());
            //return View(await _context.Collaboration.ToListAsync());
        }
        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }


        // GET: Collaborations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }

        // GET: Collaborations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collaborations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Bpm,SongKey,Genre,Type")] Collaboration collaboration)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = await _userManager.GetUserAsync(User);
                collaboration.Owner = applicationUser;

                _context.Add(collaboration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collaboration);
        }

        // GET: Collaborations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration.FindAsync(id);
            if (collaboration == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, collaboration,
                ProjectOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return View(collaboration);
        }

        // POST: Collaborations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Bpm,SongKey,Genre,Type")] Collaboration collaboration)
        {
            if (id != collaboration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                 User, collaboration,
                                                 ProjectOperations.Update);
                    if (!isAuthorized.Succeeded)
                    {
                        return Forbid();
                    }

                    _context.Update(collaboration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationExists(collaboration.ID))
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
            return View(collaboration);
        }

        // GET: Collaborations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (collaboration == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                 User, collaboration,
                                                 ProjectOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return View(collaboration);
        }

        // POST: Collaborations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaboration = await _context.Collaboration.FindAsync(id);
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, collaboration,
                                                ProjectOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            _context.Collaboration.Remove(collaboration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationExists(int id)
        {
            return _context.Collaboration.Any(e => e.ID == id);
        }
    }
}
