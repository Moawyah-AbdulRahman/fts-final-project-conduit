using conduit.db.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace conduit.db.configurations;

internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasOne(c => c.Article)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId);

        builder
            .HasOne(c => c.Writer)
            .WithMany(u => u.WrittenComments)
            .HasForeignKey(c => c.WriterId);
    }
}
