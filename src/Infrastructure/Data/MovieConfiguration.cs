// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using ApplicationCore.Entities;

// namespace Infrastructure.Data.Config;

// public class MovieConfiguration : IEntityTypeConfiguration<Movie>
// {
//     public void Configure(EntityTypeBuilder<Reservation> builder)
//     {
//         var navigation = builder.Metadata.FindNavigation(nameof(Movie.Id));

//         navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

//         builder.Reservation<>().HasOne(p => p.).WithMany(b => b.Posts)
//     .HasForeignKey(p => p.BlogId)
//     .OnDelete(DeleteBehavior.Cascade);

//     }
// }
