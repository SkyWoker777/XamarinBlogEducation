using AutoMapper;
using System.Collections.Generic;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Account;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<List<Post>, List<GetAllPostsBlogViewItem>>();
            CreateMap<Post, GetDetailsPostBlogView>();
            CreateMap<CreatePostBlogViewModel, Post>();
            CreateMap<ApplicationUser, EditAccountViewModel>().ReverseMap();
            CreateMap<RegisterAccountViewModel, ApplicationUser>();
            CreateMap<List<Category>, List<GetAllCategoriesblogViewItem>>();
            CreateMap<AddCommentBlogViewModel, Comment>();
        }
    }
}
