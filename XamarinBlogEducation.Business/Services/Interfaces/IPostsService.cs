using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAll();
        Task CreatePost(CreatePostRequestModel post);
        Task DeletePost(long selectedPostId);
        Task EditPostAsync(EditPostBlogRequestModel post);
        Task<Post> GetDetailsPost(int selectedPostId);
        Task<List<GetAllCommentResponseModel>> ShowComments(int selectedPostId);
        Task<Post> GetPost(int postId);
        Task<IEnumerable<Post>> GetPostsByCategory(int categoryId);
        Task<IEnumerable<Post>> GetPostsByDate(DateTime CreationDate);
        Task<List<Category>> GetAllCategories();
        Task AddCategory(AddNewCategoryRequestModel newCategory);
        Task<IEnumerable<Post>> GetUserPosts(string userEmail);
        Task<string> GetCategoryName(long categoryId);
    }
}
