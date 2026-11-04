using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zudor_Iulia_Lab2.Models;

namespace Zudor_Iulia_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Zudor_Iulia_Lab2Context _context;

    public EditModel(Zudor_Iulia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        Borrowing = borrowing;

        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + (x.Author != null ? " - " + x.Author.LastName + " " + x.Author.FirstName : "")
            });

        var memberList = _context.Member
            .Select(x => new
            {
                x.ID,
                MemberDisplay = (!string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.LastName))
                                ? x.FirstName + " " + x.LastName
                                : x.Email
            });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
        ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberDisplay");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Borrowing.Member");
        ModelState.Remove("Borrowing.Book");

        if (!ModelState.IsValid)
        {
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    BookFullName = x.Title + (x.Author != null ? " - " + x.Author.LastName + " " + x.Author.FirstName : "")
                });

            var memberList = _context.Member
                .Select(x => new
                {
                    x.ID,
                    MemberDisplay = (!string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.LastName))
                                    ? x.FirstName + " " + x.LastName
                                    : x.Email
                });

            ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
            ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberDisplay");
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }
}