using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace XamarinBlogEducation.Business.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostsService(IPostsRepository postsRepository, ICommentsRepository commentsRepository, ICategoriesRepository categoriesRepository, UserManager<ApplicationUser> userManager)
        {
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
            _categoriesRepository = categoriesRepository;
            _userManager = userManager;
        }

        public async Task CreatePost(CreatePostBlogViewModel postBlog)
        {

            var post = new Post();
            var author = await _userManager.FindByIdAsync(postBlog.AuthorId);
            post.Title = postBlog.Title;
            post.Content = postBlog.Content;
            if (postBlog.Author == null)
            {
                post.Author = (author.FirstName + " " + author.LastName);
            }
            post.Author = postBlog.Author;
            post.Description = postBlog.Description;
            post.CategoryId = postBlog.CategoriesId;
            post.AuthorId = postBlog.AuthorId;
            await _postsRepository.Add(post);
            await _postsRepository.SaveChanges();

        }
        public async Task DeletePost(int selectedPostId)
        {
            _postsRepository.Delete(await _postsRepository.GetPost(selectedPostId));
            await _postsRepository.SaveChanges();
        }
        public async Task AddCategory(GetAllCategoriesblogViewItem newCategory)
        {
            Category category = new Category();
            category.CategoryName = newCategory.Category;
            await _categoriesRepository.Add(category);
            await _categoriesRepository.SaveChanges();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postsRepository.GetPost(postId);
        }

        public async Task EditPostAsync(CreatePostBlogViewModel post)
        {
            var oldPost = await _postsRepository.GetPost(post.Id);
            
            oldPost.Content = post.Content;
            oldPost.Description = post.Description;
            oldPost.Title = post.Title;
            _postsRepository.Edit(oldPost);
        }

        public async Task<List<GetAllPostsBlogViewItem>> GetAll()
        {

            var result = (await _postsRepository.GetList()).Select(x => new GetAllPostsBlogViewItem
            {
                Id= x.Id,
                Title = x.Title,
                Description = x.Description,
                Content=x.Content,
                Author=x.Author,
                AuthorId=x.AuthorId,
                CreationDate=x.CreationDate,
                CategoryId=x.CategoryId
            
            }).ToList();

            return result;
        }
        public async Task<List<GetAllPostsBlogViewItem>> GetUserPosts(string userEmail)
        {
            var author = await _userManager.FindByEmailAsync(userEmail);
            var result = (await _postsRepository.GetByAuthor(author.Id)).Select(x => new GetAllPostsBlogViewItem
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Content = x.Content,
                Author = x.Author,
                CreationDate = x.CreationDate,
                CategoryId = x.CategoryId

            }).ToList();

            return result;
        }
        public async Task<List<GetAllCategoriesblogViewItem>> GetAllCategories()
        {
            var result = (await _categoriesRepository.GetList()).Select(x => new GetAllCategoriesblogViewItem
            {
                Id=x.Id,
                Category = x.CategoryName
            }).ToList();
            return result;
        }

        public Task<IEnumerable<Post>> PostsByCategory(int categoryId)
        {
            return _postsRepository.GetByCategory(categoryId);
        }

        public Task<IEnumerable<Post>> PostsByDate(DateTime CreationDate)
        {
            return _postsRepository.GetByDate(CreationDate);
        }

        public Task<IEnumerable<Post>> PostsByKey(string key)
        {
            return _postsRepository.GetByKey(key);
        }

        public async Task<GetDetailsPostBlogView> GetDetailsPost(int selectedPostId)
        {
            var result = await _postsRepository.GetPost(selectedPostId);
            var detailedPost = new GetDetailsPostBlogView()
            {
                Author = result.Author,
                Content = result.Content,
                CreationDate = result.CreationDate,
                Title = result.Title
                //Comments = result.Comments.Select(r => new GetAllCommentsBlogViewItem()
                //{
                //    Content = r.Content,
                //    UserName = r.UserId.UserName
                //}).ToList()

            };
            return detailedPost;
        }

        public Task<List<GetAllCommentsBlogViewItem>> ShowComments(int selectedPostId)
        {
            throw new NotImplementedException();
        }


    }
}

