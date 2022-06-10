using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data.Entities;
namespace ReviewBook.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<MyBooks> myBooks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Propose> Proposes { get; set; }
        public DbSet<Book_Tag> BookTags { get; set; }
        public DbSet<Propose_Tag> ProposeTags { get; set; }
        public DbSet<ReviewChildren> reviewChildrens { get; set; }
        public DbSet<MyTags> myTags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Account>()
                .HasOne(s => s.role)
                .WithMany(r => r.Accounts)
                .HasForeignKey("ID_Role");

            builder.Entity<Follow>(e =>
            {
                e.HasOne(c => c.Following)
                .WithMany(d => d.myFollowers)
                .HasForeignKey("ID_Following")
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(c => c.Follower)
                .WithMany(d => d.myFollowings)
                .HasForeignKey("ID_Follower")
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<MyBooks>(e =>
            {
                e.HasOne(s => s.Acc)
                .WithMany(a => a.myBooks)
                .HasForeignKey("ID_Acc");
                e.HasOne(s => s.book)
                .WithMany(b => b.Accounts)
                .HasForeignKey("ID_Book");
            });
            builder.Entity<MyTags>(e =>
            {
                e.HasOne(s => s.Account)
                .WithMany(a => a.myTags)
                .HasForeignKey("ID_Acc");
                e.HasOne(s => s.Tag)
                .WithMany(b => b.myTags)
                .HasForeignKey("ID_Tag");
            });
            builder.Entity<Review>(e =>
            {
                e.HasOne(c => c.Account)
                .WithMany(a => a.reviews)
                .HasForeignKey("ID_Acc");

                e.HasOne(c => c.Book)
                .WithMany(b => b.reviews)
                .HasForeignKey("ID_Book");
            });

            builder.Entity<Book>(e =>
            {
                e.HasOne(c => c.author)
                .WithMany(d => d.Books)
                .HasForeignKey("ID_Aut");
                e.HasOne(c => c.publisher)
                .WithMany(p => p.Books)
                .HasForeignKey("ID_Pub");
            });

            builder.Entity<Book_Tag>(e =>
            {
                e.HasOne(c => c.tag)
                .WithMany(b => b.Books)
                .HasForeignKey("ID_Tag");

                e.HasOne(c => c.book)
                .WithMany(t => t.Tags)
                .HasForeignKey("ID_Book");
            });

            builder.Entity<Propose>(e =>
            {
                e.HasOne(c => c.Author)
                .WithMany(a => a.Proposes)
                .HasForeignKey("ID_Aut");

                e.HasOne(c => c.Publisher)
                .WithMany(p => p.Proposes)
                .HasForeignKey("ID_Pub");

                e.HasOne(c => c.AccountRequest)
                .WithMany(a => a.Proposes)
                .HasForeignKey("ID_Acc_Request");
            });

            builder.Entity<Propose_Tag>(e =>
            {
                e.HasOne(c => c.propose)
                .WithMany(p => p.Tags)
                .HasForeignKey("ID_Propose");
                e.HasOne(c => c.tag)
                .WithMany(t => t.Proposes)
                .HasForeignKey("ID_Tag");
            });
            builder.Entity<ReviewChildren>(e =>
            {
                e.HasOne(c => c.ReviewParent)
                .WithMany(d => d.reviewChildrens)
                .HasForeignKey("Id_parent");

                e.HasOne(c => c.Account)
                .WithMany(d => d.ReviewChildrens)
                .HasForeignKey("ID_Acc");
            });
            base.OnModelCreating(builder);
        }
    }
}