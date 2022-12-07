using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;

namespace TravelApp.Data.Seeds
{
    /// <summary>
    /// This class holds Comment Configuration.
    /// </summary>
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(CreateComments());
        }

        private static List<Comment> CreateComments()
        {
            var comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Title = "New York is the best!",
                    Description = "My trip to NY was cool. I had amazing experience. I am coming back very soon.",
                    PostId = 1,
                    Author = "guest1@guest.com"

                },

                new Comment()
                {
                    Id = 2,
                    Title = "Amazing New York",
                    Description = "Your trip looks fun. I will visit New York in two months. You have some tips.",
                    PostId = 1,
                    Author = "guest2@guest.com"
                },

                new Comment()
                {
                    Id = 3,
                    Title = "My journey in Los Angeles!",
                    Description = "Los Angeles looks great. I have great time there. Hope to be back soon.",
                    PostId = 2,
                    Author = "guest2@guest.com"
                },
            };

            return comments;
        }
    }
}
