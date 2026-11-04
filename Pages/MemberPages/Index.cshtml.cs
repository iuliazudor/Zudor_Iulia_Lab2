using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zudor_Iulia_Lab2.Models;

namespace Zudor_Iulia_Lab2.Pages.MemberPages;

public class IndexModel : PageModel
{
    private readonly Zudor_Iulia_Lab2Context _context;

    public IndexModel(Zudor_Iulia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Member> Member { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Member = await _context.Member.ToListAsync();
    }
}
