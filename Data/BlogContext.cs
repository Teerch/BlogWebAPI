using Microsoft.EntityFrameworkCore;
using Web.Models;
namespace Web.Data;
public class BlogContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Comments> Comments { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options)
    : base(options)
    {

    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionString = 
    //     optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable(nameof(User));
        modelBuilder.Entity<Posts>().ToTable(nameof(Posts));
        modelBuilder.Entity<Comments>().ToTable(nameof(Comments));


        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(x => x.User_name).IsUnique();
        modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.First_name).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Last_name).IsRequired();


        modelBuilder.Entity<Posts>().HasKey(x => x.Id);
        modelBuilder.Entity<Posts>().Property(x => x.Id).ValueGeneratedOnAdd();


        modelBuilder.Entity<Comments>().HasKey(x => x.Id);
        modelBuilder.Entity<Comments>().Property(x => x.Id).ValueGeneratedOnAdd();


        // RELATIONSHIPS
        modelBuilder.Entity<User>()
            .HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<User>()
            .HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Posts>()
            .HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}