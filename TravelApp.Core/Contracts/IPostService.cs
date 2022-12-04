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
    public interface IPostService
    {
        Task<IEnumerable<AllPostsModel>> GetAllPosts();
        Task<IEnumerable<Post>> GetAllPostsByTrip(int tripId);
        Task Add(AddPostModel addPostModel);
        Task<IEnumerable<Post>> GetPostsForSelect();
        Task<DetailsPostModel> GetPostDetailsById(int postId);
        Task<EditPostModel> EditCreateForm(int postId);
        Task Edit(int postId, EditPostModel editPostModel);
        Task<Post> GetPostById(int postId);
        Task<DeletePostModel> DeleteCreateForm(int postId);
        Task Delete(int postId);
    }
}
