using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;

namespace TravelApp.Data.Seeds
{
    /// <summary>
    /// This class holds Post Configuration.
    /// </summary>
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(CreatePosts());
        }

        private List<Post> CreatePosts()
        {
            var posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "Post about my trip to New York.",
                    Description = "My trip to New York was fun. The city is amazing. I love it.",
                    TripId = 1,
                    Image = "/Photos/NewYork.jpg?a=123456"
                },

                new Post()
                { 
                    Id = 2,
                    Title = "My trip to Los Angeles.",
                    Description = "My trip to Los Angeles is unforgettable. I miss the city",
                    TripId = 2,
                    Image = "/Photos/LosAngeles.jpg?a=123456"
                }
            };

            return posts;
        }
    }
}
