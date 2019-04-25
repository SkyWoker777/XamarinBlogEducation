using AutoMapper;
using System.Collections.Generic;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

          
            CreateMap<Post, GetAllPostResponseModel>();
            CreateMap<Post, GetDetailsPostResponseModel>();
            CreateMap<CreatePostBlogRequestModel, Post>();
            CreateMap<ApplicationUser, EditAccountRequestModel>().ReverseMap();
            CreateMap<ApplicationUser, GetInfoAccountResponseModel>().ReverseMap();
            CreateMap<RegisterAccountRequestModel, ApplicationUser>();
            CreateMap<Category, GetAllCategoryResponseModel>().ForMember(
                ct=>ct.Category, 
                opt=>opt.MapFrom(src=>src.Name));
                
            CreateMap<AddCommentRequestBlogView, Comment>();
            CreateMap<GetAllCommentResponseModel, Comment>().ReverseMap();
        }
    }
}
