using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class CommentService : ICommentService
    {
        private readonly IRepository data;
        public CommentService(IRepository data)
        {
            this.data = data;
        }

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

        public async Task Edit(int commentId, EditCommentModel editCommentModel)
        {
            var commentToBeEdited = await 
                GetCommentById(commentId);

            commentToBeEdited!.Title = editCommentModel.Title;
            commentToBeEdited.Description = editCommentModel.Description;

            this.data.Update(commentToBeEdited);
            await this.data.SaveChangesAsync();

        }

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

        public async Task<DetailsCommentModel> GetCommentDetailsById(int commentId)
        {
            return await
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
               }).FirstAsync();
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await
               this.data
               .AllReadonly<Comment>()
               .Include(c => c.Post)
               .Where(c => c.Id == commentId)
               .FirstAsync();
        }

        public async Task Delete(int commentId)
        {
            await this.data.DeleteAsync<Comment>(commentId);
            await this.data.SaveChangesAsync();
        }

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
