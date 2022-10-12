using conduit.db.configurations;
using conduit.db.models;
using Microsoft.EntityFrameworkCore;

namespace conduit.db;

public class ConduitDbContext : DbContext
{

#nullable disable
    internal DbSet<User> Users { get; set; }

    internal DbSet<Article> Articles { get; set; }

    internal DbSet<Comment> Comments { get; set; }

#nullable enable

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"
            Data Source=(localdb)\ProjectModels;
            Initial Catalog=ConduitDb;
            Integrated Security=True;
            Connect Timeout=30;
            Encrypt=False;
            TrustServerCertificate=False;
            ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False"
         );

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());

        new ArticleEntityTypeConfiguration().Configure(modelBuilder.Entity<Article>());

        new CommentEntityTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
    }

}