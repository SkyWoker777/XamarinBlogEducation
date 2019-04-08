using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetAllComments(int postId);
        Task AddComment(AddCommentBlogViewModel newComment, int postId);
    }
}
