using conduit.db.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace conduit.db.configurations;

internal class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .HasIndex(a => new { a.Title, a.CreatorId })
            .IsUnique();

        builder
            .HasOne(a => a.Creator)
            .WithMany(u => u.PostedArticles)
            .HasForeignKey(a => a.CreatorId);
    }
}
