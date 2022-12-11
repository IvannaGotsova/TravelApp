using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class CommentServiceTests : UnitTestsBase
    {
        private ICommentService commentService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            commentService = new CommentService(repository);

        }


        [Test]
        public void Test_CommentService_Add()
        {
            //Arrange
            int commentsCount = this.data.Comments.Count();

            //Act : create and add new comment
            var commentToAdd = new AddCommentModel()
            {

                Title = "Test Comment Title",
                Description = "Test Comment Description",
                PostId = 1,
                Author = "test@test.com",
            };

            commentService.Add(commentToAdd);
            //Assert : new comment is added
            Assert.That(this.data.Comments.Count() == commentsCount + 1);
        }


        [Test]
        public void Test_CommentService_GetAllComments()
        {
            //Arange
            int commentsCount = data.Comments.Count();

            //Act : get all comments
            var comments = commentService.GetAllComments().Result;

            //Assert : number of comments is correct
            Assert.That(comments.Count() == commentsCount);
        }

        [Test]
        public void Test_CommentService_GetCommentDetailsById()
        {
            //Arange
            int commentId = 1;

            //Act : get comment
            var comment = commentService.GetCommentDetailsById(commentId).Result;

            //Assert : comment properties are correct
            Assert.That(comment.Title == "Test Comment Title");
            Assert.That(comment.Description == "Test Comment Description");
            Assert.That(comment.PostId == 1);
            Assert.That(comment.Author == "test@test.com");
        }


        [Test]
        public void TestCommentService_GetCommentDetailsByIdNull()
        {
            //Arange
            int commentId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await commentService.GetCommentDetailsById(commentId));
        }

        [Test]
        public void TestCommentService_GetCommentDetailsByIdNull_2()
        {
            //Arange
            int commentId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await commentService.GetCommentDetailsById(commentId));
        }

        [Test]
        public void Test_CommentService_GetCommentById()
        {
            //Arange
            int commentId = 1;

            //Act : get comment
            var comment = commentService.GetCommentById(commentId).Result;

            //Assert : comment properties are correct
            Assert.That(comment.Title == "Test Comment Title");
            Assert.That(comment.Description == "Test Comment Description");
            Assert.That(comment.PostId == 1);
            Assert.That(comment.Author == "test@test.com");
        }

        [Test]
        public void Test_CommentService_GetCommentByIdNull()
        {
            //Arange
            int commentId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await commentService.GetCommentById(commentId));
        }

        [Test]
        public void Test_CommentService_GetCommentByIdNull_2()
        {
            //Arange
            int commentId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await commentService.GetCommentById(commentId));
        }

        [Test]
        public void Test_CommentService_Delete()
        {
            //Arrange
            int commentId = 2;
            int commentsCount = data.Comments.Count();

            //Act : deletes the comment with given id
            commentService.Delete(commentId);

            //Assert : number of comments is correct
            Assert.That(data.Comments.Count(), Is.EqualTo(commentsCount - 1));
        }

        [Test]
        public void Test_CommentService_GetAllCommentsByPost()
        {
            //Arrange
            int postId = 1;
            int commentsCount = data.Comments.Where(c => c.PostId == postId).Count();

            //Act : gets all comments from post with given id
            int postCommentsCount = commentService.GetAllCommentsByPost(postId).Result.Count();

            //Assert : number of comments is correct
            Assert.That(commentsCount == postCommentsCount);
        }

        [Test]
        public void Test_CommentService_EditCreateForm()
        {
            //Arrange
            int commentId = 3;
            var comment = commentService.GetCommentById(commentId).Result;

            //Act
            var commentEditForm = commentService.EditCreateForm(commentId).Result;

            //Assert
            Assert.That(comment.Title == commentEditForm.Title);
            Assert.That(comment.Description == commentEditForm.Description);
        }

        [Test]
        public void Test_CommentService_DeleteCreateForm()
        {
            //Arrange
            int commentId = 4;
            var comment = commentService.GetCommentById(commentId).Result;

            //Act
            var commentDeleteForm = commentService.DeleteCreateForm(commentId).Result;

            //Assert
            Assert.That(comment.Id == commentDeleteForm.Id);
            Assert.That(comment.Title == commentDeleteForm.Title);
            Assert.That(comment.Description == commentDeleteForm.Description);
        }

        [Test]
        public void Test_CommentService_Edit()
        {
            //Arrange
            int commentId = 2;
            var comment = this.data.Comments.Where(c => c.Id == commentId).First();

            //Act : edit a comment
            var commentToEdit = new EditCommentModel()
            {

                Title = "Test Comment Title",
                Description = "Changed Test Comment Description",
            };

            
            commentService.Edit(commentId, commentToEdit);

            //Assert : description of comment is changed
            Assert.That(comment.Description, Is.EqualTo("Changed Test Comment Description"));

        }
    }
}
