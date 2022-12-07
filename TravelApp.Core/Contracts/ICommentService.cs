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
    /// <summary>
    /// Holds Interface for Comments functionality.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// This method returns IEnumerable of all comments.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllCommentsModel>> GetAllComments();
        /// <summary>
        /// This method returns IEnumerable of all comments made about particular post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetAllCommentsByPost(int postId);
        /// <summary>
        /// This method is used to add a comment.
        /// </summary>
        /// <param name="addCommentModel"></param>
        /// <returns></returns>
        Task Add(AddCommentModel addCommentModel);
        /// <summary>
        /// This method returns Details of particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<DetailsCommentModel> GetCommentDetailsById(int commentId);
        /// <summary>
        /// This method creates form for editing a comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<EditCommentModel> EditCreateForm(int commentId);
        /// <summary>
        /// This method is used to edit a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="editCommentModel"></param>
        /// <returns></returns>
        Task Edit(int commentId, EditCommentModel editCommentModel);
        /// <summary>
        /// This method returns a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<Comment> GetCommentById(int commentId);
        /// <summary>
        /// This method creates form for deleting a comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<DeleteCommentModel> DeleteCreateForm(int commentId);
        /// <summary>
        /// This method deletes a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task Delete(int commentId);
    }
}
