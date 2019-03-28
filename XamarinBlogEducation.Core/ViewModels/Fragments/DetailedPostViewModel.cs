using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class DetailedPostViewModel : BaseViewModel<GetAllPostsBlogViewItem>
    {
        private IBlogService _blogService;
        public DetailedPostViewModel(IBlogService blogService, IMvxNavigationService navigationService) :base(navigationService)
        {
           
           _blogService = blogService;
            //AddCommentCommand = new MvxAsyncCommand(AddComment);
        } 
     

        public override Task Initialize()
        {
            return Task.FromResult(0);
        }
        private GetAllPostsBlogViewItem _detailedPost;
        public GetAllPostsBlogViewItem DetailedPost
        {
            get => _detailedPost;
            set
            {
                _detailedPost = value;
                RaisePropertyChanged(() => DetailedPost);
            }
        }
        public override void Prepare(GetAllPostsBlogViewItem parameter)
        {
            DetailedPost = parameter;
            
        }

       

        

    }
}
