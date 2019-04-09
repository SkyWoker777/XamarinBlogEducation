using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<List<GetAllCommentsBlogViewItem>> GetAllComments(long postId);
        Task AddComment(AddCommentBlogViewModel newComment, long postId);
    }
}
