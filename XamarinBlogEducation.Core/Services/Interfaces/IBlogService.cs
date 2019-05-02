using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IBlogService
    {
        Task<GetDetailsPostResponseModel> ShowDetailedPost();
        Task<List<GetAllPostResponseModel>> GetAllPosts();
        Task<List<GetAllCategoryResponseModel>> GetAllCategories();
        Task<List<GetAllCommentResponseModel>> GetAllComments(long postId);
        Task<bool> AddNewPost(CreatePostRequestModel model);
        Task<bool> AddComment(AddCommentRequestBlogView model);
        Task<bool> AddNewCategory(AddNewCategoryRequestModel category);
        Task<List<GetAllUserPostResponseModel>> GetUserPosts(string userEmail);
        Task<bool> UpdatePost(EditPostBlogRequestModel model);
        Task<bool> DeletePost(long postId);
    }
}
