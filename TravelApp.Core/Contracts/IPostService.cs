using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Post functionality.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        ///  This method returns IEnumerable of all posts.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllPostsModel>> GetAllPosts();
        /// <summary>
        /// This method returns IEnumerable of all posts made about particular trip.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPostsByTrip(int tripId);
        /// <summary>
        /// This method is used to add a post.
        /// </summary>
        /// <param name="addPostModel"></param>
        /// <returns></returns>
        Task Add(AddPostModel addPostModel);
        /// <summary>
        /// This method returns IEnumerable of all Posts used for Select.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetPostsForSelect();
        /// <summary>
        /// This method returns Details of particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<DetailsPostModel> GetPostDetailsById(int postId);
        /// <summary>
        /// This method creates form for editing a post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<EditPostModel> EditCreateForm(int postId);
        /// <summary>
        /// This method is used to edit a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="editPostModel"></param>
        /// <returns></returns>
        Task Edit(int postId, EditPostModel editPostModel);
        /// <summary>
        /// This method returns a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<Post> GetPostById(int postId);
        /// <summary>
        /// This method creates form for deleting a post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<DeletePostModel> DeleteCreateForm(int postId);
        /// <summary>
        /// This method deletes a particular post with given id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task Delete(int postId);
    }
}
