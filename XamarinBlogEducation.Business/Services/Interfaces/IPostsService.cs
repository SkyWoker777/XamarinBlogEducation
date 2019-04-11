using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAll();
        Task CreatePost(CreatePostBlogViewModel post);
        Task DeletePost(long selectedPostId);
        Task EditPostAsync(CreatePostBlogViewModel post);
        Task<Post> GetDetailsPost(int selectedPostId);
        Task<List<GetAllCommentsBlogViewItem>> ShowComments(int selectedPostId);
        Task<Post> GetPost(int postId);
        Task<IEnumerable<Post>> GetPostsByCategory(int categoryId);
        Task<IEnumerable<Post>> GetPostsByKeyWord(string key);
        Task<IEnumerable<Post>> GetPostsByDate(DateTime CreationDate);
        Task<List<Category>> GetAllCategories();
        Task AddCategory(GetAllCategoriesblogViewItem newCategory);
        Task<IEnumerable<Post>> GetUserPosts(string userEmail);
        Task<string> GetCategoryName(long categoryId);
    }
}
