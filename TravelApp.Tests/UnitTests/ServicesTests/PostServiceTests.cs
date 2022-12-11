using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class PostServiceTests : UnitTestsBase
    {
        private IPostService postService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            postService = new PostService(repository);
        }

        [Test]
        public void Test_PostService_Add()
        {
            //Arrange
            int postsCount = this.data.Posts.Count();

            //Act : create and add new post
            var postToAdd = new AddPostModel()
            {

                Title = "Test Post Title",
                Description = "Test Post Description",
                TripId = 1,
                Image = "/Photos/Test"
            };

            postService.Add(postToAdd);

            //Assert : new post is added
            Assert.That(this.data.Posts.Count(), Is.EqualTo(postsCount + 1));
        }

        [Test]
        public void Test_PostService_Delete()
        {
            //Arrange
            int postId = 3;
            int postsCount = data.Posts.Count();

            //Act : deletes the post with given id
            postService.Delete(postId);

            //Assert : number of posts is correct
            Assert.That(data.Posts.Count(), Is.EqualTo(postsCount - 1));
        }


        [Test]
        public void Test_PostService_Edit()
        {
            //Arrange
            int postId = 2;
            var post = this.data.Posts.Where(p => p.Id == postId).First();

            //Act : edit a comment
            var postToEdit = new EditPostModel()
            {
                Title = "Test Post Title",
                Description = "Changed Test Post Description",
                TripId = 1,
                Image = "/Photos/Test"
            };

            postService.Edit(postId, postToEdit);

            //Assert : description of post is changed
            Assert.That(post.Description, Is.EqualTo("Changed Test Post Description"));
        }

        [Test]
        public void Test_PostService_GetAllPosts()
        {
            //Arange
            int postsCount = data.Posts.Count();

            //Act : get all posts
            var posts = postService.GetAllPosts().Result;

            //Assert : number of posts is correct
            Assert.That(posts.Count(), Is.EqualTo(postsCount));
        }


        [Test]
        public void Test_PostService_GetAllPostsByTrip()
        {
            //Arrange
            int tripId = 1;
            int postsCount = data.Posts.Where(p => p.TripId == tripId).Count();

            //Act : gets all posts from trip with given id
            int tripPostsCount = postService.GetAllPostsByTrip(tripId).Result.Count();

            //Assert : number of posts is correct
            Assert.That(postsCount, Is.EqualTo(tripPostsCount));
        }

        [Test]
        public void Test_PostService_GetPostDetailsById()
        {
            //Arange
            int postId = 1;

            //Act : get post
            var post = postService.GetPostDetailsById(postId).Result;

            Assert.Multiple(() =>
            {

                //Assert : post properties are correct
                Assert.That(post.Title, Is.EqualTo("Test Post Title"));
                Assert.That(post.Description, Is.EqualTo("Test Post Description"));
                Assert.That(post.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void TestPostService_GetPostDetailsByIdNull()
        {
            //Arange
            int postId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await postService.GetPostDetailsById(postId));
        }



        [Test]
        public void TestPostService_GetPostDetailsByIdNull_2()
        {
            //Arange
            int postId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await postService.GetPostDetailsById(postId));
        }


        [Test]
        public void Test_PostService_GetPostById()
        {
            //Arange
            int postId = 1;

            //Act : get post
            var post = postService.GetPostById(postId).Result;

            Assert.Multiple(() =>
            {

                //Assert : post properties are correct
                Assert.That(post.Title, Is.EqualTo("Test Post Title"));
                Assert.That(post.Description, Is.EqualTo("Test Post Description"));
                Assert.That(post.TripId, Is.EqualTo(1));
                Assert.That(post.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void Test_CommentService_GetCommentByIdNull()
        {
            //Arange
            int postId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await postService.GetPostById(postId));
        }


        [Test]
        public void Test_CommentService_GetCommentByIdNull_2()
        {
            //Arange
            int postId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await postService.GetPostById(postId));

        }
        [Test]
        public void Test_PostService_GetPostsForSelect()
        {
            //Arange
            int postsCount = data.Posts.Count();

            //Act : get all posts
            var posts = postService.GetPostsForSelect().Result;

            //Assert : number of countries is correct
            Assert.That(posts.Count(), Is.EqualTo(postsCount));
        }


        [Test]
        public void Test_PostService_EditCreateForm()
        {
            //Arrange
            int postId = 2;
            var post = postService.GetPostById(postId).Result;

            //Act
            var postEditForm = postService.EditCreateForm(postId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(post.Title, Is.EqualTo(postEditForm.Title));
                Assert.That(post.Description, Is.EqualTo(postEditForm.Description));
                Assert.That(post.Image, Is.EqualTo(postEditForm.Image));
            });
        }

        [Test]
        public void Test_PostService_DeleteCreateForm()
        {
            //Arrange
            int postId = 4;
            var post = postService.GetPostById(postId).Result;

            //Act
            var postDeleteForm = postService.DeleteCreateForm(postId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(post.Title, Is.EqualTo(postDeleteForm.Title));
                Assert.That(post.Description, Is.EqualTo(postDeleteForm.Description));
            });
        }
    }
}
