using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace XamarinBlogEducation.Business.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public PostsService(IPostsRepository postsRepository, ICommentsRepository commentsRepository, ICategoriesRepository categoriesRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
            _categoriesRepository = categoriesRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreatePost(CreatePostRequestModel postBlog)
        {
            var post = _mapper.Map<Post>(postBlog);
            var author = await _userManager.FindByIdAsync(postBlog.AuthorId);
            if (postBlog.Author == null)
            {
                post.AuthorName = (author.FirstName + " " + author.LastName);
            }
            await _postsRepository.Add(post);
        }
        public async Task DeletePost(long selectedPostId)
        {
            _postsRepository.DeleteAsync(await _postsRepository.GetPost(selectedPostId));
        }
        public async Task AddCategory(AddNewCategoryRequestModel newCategory)
        {
            Category category = new Category();
            category.Name = newCategory.Category;
            await _categoriesRepository.Add(category);
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postsRepository.GetPost(postId);
        }

        public async Task EditPostAsync(EditPostBlogRequestModel post)
        {
            var oldPost = await _postsRepository.GetPost(post.Id);
            oldPost.Content = post.Content;
            oldPost.Description = post.Description;
            oldPost.Title = post.Title;
            _postsRepository.Edit(oldPost);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {   //TODO: 12312321
            var result = (await _postsRepository.GetList());
            return result;
        }
        public async Task<IEnumerable<Post>> GetUserPosts(string userEmail)
        {
            var author = await _userManager.FindByEmailAsync(userEmail);
            var result = (await _postsRepository.GetByAuthor(author.Id));
            return result;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var result = (await _categoriesRepository.GetList()).ToList();
            return result;
        }

        public Task<IEnumerable<Post>> GetPostsByCategory(int categoryId)
        {
            return _postsRepository.GetByCategory(categoryId);
        }

        public Task<IEnumerable<Post>> GetPostsByDate(DateTime CreationDate)
        {
            return _postsRepository.GetByDate(CreationDate);
        }

        public async Task<Post> GetDetailsPost(int selectedPostId)
        {
            var result = await _postsRepository.GetPost(selectedPostId);      
            return result;
        }
        public async Task<string> GetCategoryName(long categoryId)
        {
            var result = await _postsRepository.GetCategoryName(categoryId);
            return result;
        }
        public Task<List<GetAllCommentResponseModel>> ShowComments(int selectedPostId)
        {
            throw new NotImplementedException();
        }


    }
}

