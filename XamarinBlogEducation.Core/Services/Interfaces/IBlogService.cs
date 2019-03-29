using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IBlogService
    {
        Task<GetDetailsPostBlogView> ShowDetailedPost();
        Task<List<GetAllPostsBlogViewItem>> GetAllPosts();
        Task<List<GetAllCategoriesblogViewItem>> GetAllCategories();
        Task AddNewPost(CreatePostBlogViewModel model);
        Task<bool> AddNewCategory(GetAllCategoriesblogViewItem category);
        Task NavigatePosts();
        Task UpdatePost();
        Task RemovePost();
    }
}
