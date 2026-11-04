using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options) : IdentityDbContext<Zudor_Iulia_Lab2.Data.ApplicationUser>(options)
{
}
