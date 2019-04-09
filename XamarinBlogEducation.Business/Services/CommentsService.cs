using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Blog;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;
using AutoMapper;
using XamarinBlogEducation.ViewModels.Blog.Items;
using Microsoft.AspNetCore.Identity;

namespace XamarinBlogEducation.Business.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddComment(AddCommentBlogViewModel newComment, long postId)
        {
            var comment = _mapper.Map<Comment>(newComment);
            comment.PostId = postId;
            await _commentsRepository.Add(comment);
        }

        public async Task<List<GetAllCommentsBlogViewItem>> GetAllComments(long postId)
        {
            var allComments = await _commentsRepository.GetList(postId);
            var parsedComments = _mapper.Map<List<GetAllCommentsBlogViewItem>>(allComments);
            foreach(var comment in parsedComments)
            {
                var author = await _userManager.FindByIdAsync(comment.UserId);
                comment.UserName = (author.FirstName+" "+author.LastName);
            }
            return parsedComments;
        }
    }
}
