using conduit.db.configurations;
using conduit.db.models;
using Microsoft.EntityFrameworkCore;

namespace conduit.db;

public class ConduitDbContext : DbContext
{
    private readonly string connectionString;

#nullable disable
    internal DbSet<UserEntity> Users { get; set; }

    internal DbSet<ArticleEntity> Articles { get; set; }

    internal DbSet<CommentEntity> Comments { get; set; }

#nullable enable

    public ConduitDbContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<UserEntity>());

        new ArticleEntityTypeConfiguration().Configure(modelBuilder.Entity<ArticleEntity>());

        new CommentEntityTypeConfiguration().Configure(modelBuilder.Entity<CommentEntity>());
    }

}