using Microsoft.EntityFrameworkCore;

public class Zudor_Iulia_Lab2Context(DbContextOptions<Zudor_Iulia_Lab2Context> options) : DbContext(options)
{
    public DbSet<Zudor_Iulia_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Zudor_Iulia_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Zudor_Iulia_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Zudor_Iulia_Lab2.Models.Category> Category { get; set; } = default!;
    public DbSet<Zudor_Iulia_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}
