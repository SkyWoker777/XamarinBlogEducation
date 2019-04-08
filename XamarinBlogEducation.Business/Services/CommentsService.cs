using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;
using AutoMapper;

namespace XamarinBlogEducation.Business.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }

        public async Task AddComment(AddCommentBlogViewModel newComment, int postId)
        {
            var comment = _mapper.Map<Comment>(newComment);
            comment.PostId = postId;
            await _commentsRepository.Add(comment);
        }

        public async Task<List<Comment>> GetAllComments(int postId)
        {
            var allComments = await _commentsRepository.GetList(postId);
            return allComments;
        }
    }
}
