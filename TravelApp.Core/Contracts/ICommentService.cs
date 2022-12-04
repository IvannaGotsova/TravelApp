using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.TripModels;

namespace TravelApp.Core.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<AllCommentsModel>> GetAllComments();
        Task<IEnumerable<Comment>> GetAllCommentsByPost(int postId);
        Task Add(AddCommentModel addCommentModel);
        Task<DetailsCommentModel> GetCommentDetailsById(int commentId);
        Task<EditCommentModel> EditCreateForm(int commentId);
        Task Edit(int commentId, EditCommentModel editCommentModel);
        Task<Comment> GetCommentById(int commentId);
        Task<DeleteCommentModel> DeleteCreateForm(int commentId);
        Task Delete(int commentId);
    }
}
