using conduit.db.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace conduit.db.configurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => u.Username)
            .IsUnique();

        builder.HasCheckConstraint("ck_username_length_gte_3", "LEN(Username) >= 3");

        builder.HasCheckConstraint("ck_password_length_gte_8", "LEN(Password) >= 8");

        builder
            .HasMany(u => u.FollowedUsers)
            .WithMany(u => u.Followers);

        builder
            .HasMany(u => u.FavoriteArticles)
            .WithMany(a => a.FavouritingUsers);
    }
}
