using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.Business.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task AddComment(AddCommentBlogViewModel newComment, int postId)
        {
            var comment = new Comment();
            comment.Content = newComment.Content;
            comment.UserId = newComment.UserId;
            comment.PostId = postId;

            await _commentsRepository.Add(comment);
        }

        public async Task<List<Comment>> GetAll(int postId)
        {

            var allComments = await _commentsRepository.GetList(postId);
            return allComments;
        }
    }
}
