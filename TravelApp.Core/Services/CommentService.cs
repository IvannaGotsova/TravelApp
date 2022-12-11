using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    /// <summary>
    /// Holds Comments functionality.
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly IRepository data;
        public CommentService(IRepository data)
        {
            this.data = data;
        }
        /// <summary>
        /// This method is used to add a comment.
        /// </summary>
        /// <param name="addCommentModel"></param>
        /// <returns></returns>
        public async Task Add(AddCommentModel addCommentModel)
        {
            var commentToBeAdded = new Comment()
            {
                Title = addCommentModel.Title,
                Description = addCommentModel.Description, 
                PostId = addCommentModel.PostId,
                Author = addCommentModel.Author!
            };

            await this.data.AddAsync(commentToBeAdded);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for editing a comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<EditCommentModel> EditCreateForm(int commentId)
        {
            var commentToBeEdited = await
                GetCommentById(commentId);

            var editCommentModel = new EditCommentModel()
            {
                Title = commentToBeEdited.Title,
                Description = commentToBeEdited.Description,
            };

            return editCommentModel;
        }
        /// <summary>
        /// This method is used to edit a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="editCommentModel"></param>
        /// <returns></returns>
        public async Task Edit(int commentId, EditCommentModel editCommentModel)
        {
            var commentToBeEdited = await 
                GetCommentById(commentId);

            commentToBeEdited!.Title = editCommentModel.Title;
            commentToBeEdited.Description = editCommentModel.Description;

            this.data.Update<Comment>(commentToBeEdited);
            await this.data.SaveChangesAsync();
        }

        /// <summary>
        /// This method returns IEnumerable of all comments.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllCommentsModel>> GetAllComments()
        {
            var comments = await data
                .AllReadonly<Comment>()
                .Include(c => c.Post)
                .ThenInclude(p => p!.Trip)
                .ThenInclude(t => t!.ApplicationUser)
                .ToListAsync();

            return comments
                .Select(c => new AllCommentsModel()
                {
                    Id = c.Id,
                    Title = c.Title,                   
                    Author = c.Author,
                    PostId = c.PostId
                })
                .ToList();
        }
        /// <summary>
        /// This method returns Details of particular comment with given id..
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<DetailsCommentModel> GetCommentDetailsById(int commentId)
        {
           var comment = await
               this.data
               .AllReadonly<Comment>()
               .Include(c => c.Post)
               .ThenInclude(p => p!.Trip)
               .Where(c => c.Id == commentId)
               .Select(c => new DetailsCommentModel()
               {
                   Id = c.Id,
                   Title = c.Title,
                   Description = c.Description,
                   Author = c.Author,
                   PostId = c.PostId
               }).FirstOrDefaultAsync();

            //check if comment is null
            if (comment == null)
            {
                throw new ArgumentNullException(null, nameof(comment));
            }

            return comment;
        }
        /// <summary>
        /// This method returns a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await
               this.data
               .AllReadonly<Comment>()
               .Include(c => c.Post)
               .Where(c => c.Id == commentId)
               .FirstOrDefaultAsync();

            //check if comment is null
            if (comment == null)
            {
                throw new ArgumentNullException(null, nameof(comment));
            }

            return comment;
        }
        /// <summary>
        /// This method deletes a particular comment with given id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task Delete(int commentId)
        {
            await this.data.DeleteAsync<Comment>(commentId);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for deleting a comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<DeleteCommentModel> DeleteCreateForm(int commentId)
        {
            var commentToBeDeleted = await
               GetCommentById(commentId);

            var deleteCommentModel = new DeleteCommentModel()
            {
                Id = commentToBeDeleted.Id,
                Title = commentToBeDeleted.Title,
                Description = commentToBeDeleted.Description,
            };

            return deleteCommentModel;
        }
        /// <summary>
        /// This method returns IEnumerable of all comments made about particular post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Comment>> GetAllCommentsByPost(int postId)
        { 
            return await
                     this.data
                     .AllReadonly<Comment>()
                     .Where(c => c.PostId == postId)
                     .ToListAsync();

        }
    }
}
