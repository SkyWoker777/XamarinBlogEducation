using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<List<GetAllCommentResponseModel>> GetAllComments(long postId);
        Task AddComment(AddCommentRequestBlogView newComment, long postId);
    }
}
