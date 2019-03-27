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
        Task<List<GetAllPostsBlogViewItem>> GetAll();//список всех постов. title+discription
        Task CreatePost(CreatePostBlogViewModel post);//должен собрать пост
        Task DeletePost(int selectedPostId);
        Task EditPostAsync(CreatePostBlogViewModel post, int postId);
        Task<GetDetailsPostBlogView> GetDetailsPost(int selectedPostId);
        Task<List<GetAllCommentsBlogViewItem>> ShowComments(int selectedPostId);
        Task<Post> GetPost(int postId);
        Task<IEnumerable<Post>> PostsByCategory(int categoryId);
        Task<IEnumerable<Post>> PostsByKey(string key);
        Task<IEnumerable<Post>> PostsByDate(DateTime CreationDate);
        Task<List<GetAllCategoriesblogViewItem>> GetAllCategories();
        Task AddCategory(GetAllCategoriesblogViewItem newCategory);

    }
}
