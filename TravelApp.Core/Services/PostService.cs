using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    /// <summary>
    /// Holds Post functionality.
    /// </summary>
    public class PostService : IPostService
    {
        private readonly IRepository data;

        public PostService(IRepository data)
        {
            this.data = data;
        }
        /// <summary>
        /// This method is used to add a post.
        /// </summary>
        /// <param name="addPostModel"></param>
        /// <returns></returns>
        public async Task Add(AddPostModel addPostModel)
        {
            var postToBeAdded = new Post()
            {
                Title = addPostModel.Title,
                TripId = addPostModel.TripId,
                Description = addPostModel.Description,
                Image = addPostModel.Image            
            };

            await this.data.AddAsync(postToBeAdded);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method deletes a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task Delete(int postId)
        {
            await this.data.DeleteAsync<Post>(postId);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for deleting a post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<DeletePostModel> DeleteCreateForm(int postId)
        {
            var postToBeDeleted = await
               GetPostById(postId);

            var deletePostModel = new DeletePostModel()
            {
                Id = postToBeDeleted.Id,
                Title = postToBeDeleted.Title,
                Description = postToBeDeleted.Description,
            };

            return deletePostModel;
        }
        /// <summary>
        /// This method is used to edit a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="editPostModel"></param>
        /// <returns></returns>
        public async Task Edit(int postId, EditPostModel editPostModel)
        {
            var postToBeEdited = await
                     GetPostById(postId);

            postToBeEdited.Title = editPostModel.Title;
            postToBeEdited.Image = editPostModel.Image;
            postToBeEdited.Description = editPostModel.Description;

            this.data.Update(postToBeEdited);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for editing a post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<EditPostModel> EditCreateForm(int postId)
        {
            var postToBeEdited = await
              GetPostById(postId);


            var editPostModel = new EditPostModel()
            {
                Title = postToBeEdited.Title,
                Description = postToBeEdited.Description,
                Image = postToBeEdited.Image,
                Trips = new List<Trip>(),

            };

            return editPostModel;
        }
        /// <summary>
        ///  This method returns IEnumerable of all posts.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllPostsModel>> GetAllPosts()
        {
            var posts = await data
                 .AllReadonly<Post>()
                 .Include(p => p.Trip)
                 .ThenInclude(t => t!.ApplicationUser)
                 .ToListAsync();

            return posts
                .Select(p => new AllPostsModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Image = p.Image,
                    TripName = p.Trip!.Title,
                    TripId = p.TripId,
                    Author = p.Trip!.ApplicationUser!.UserName
                })
                .ToList();
        }
        /// <summary>
        /// This method returns IEnumerable of all posts made about particular trip.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetAllPostsByTrip(int tripId)
        {
            return await
                     this.data
                     .AllReadonly<Post>()
                     .Include(p => p.Trip)
                     .Where(p => p.TripId == tripId)
                     .ToListAsync();

        }
        /// <summary>
        /// This method returns a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<Post> GetPostById(int postId)
        {
            var post = await
               this.data
               .AllReadonly<Post>()
               .Where(p => p.Id == postId)
               .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post;
        }
        /// <summary>
        /// This method returns Details of particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<DetailsPostModel> GetPostDetailsById(int postId)
        {
           var post = await
             this.data
             .AllReadonly<Post>()
             .Include(p => p.Trip)
             .Where(p => p.Id == postId)
             .Select(t => new DetailsPostModel()
             {
                 Id = t.Id,
                 Title = t.Title,
                 TripName = t.Trip!.Title,
                 Image = t.Image,               
                 Description = t.Description,
                 ApplicationUserId = t.Trip.ApplicationUserId
             })
             .FirstOrDefaultAsync();


            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post;
        }
        /// <summary>
        /// This method returns IEnumerable of all Posts used for Select.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetPostsForSelect()
        {
            return await
             this.data
             .AllReadonly<Post>()
             .ToListAsync();
        }
    }
}
