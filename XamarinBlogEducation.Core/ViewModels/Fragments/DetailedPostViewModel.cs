using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Blog;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class DetailedPostViewModel : BaseViewModel
    {
        private IBlogService _blogService;
        public DetailedPostViewModel(IBlogService blogService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _blogService = blogService;
        }
        private MvxObservableCollection<GetDetailsPostBlogView> _detailedPost;
        public MvxObservableCollection<GetDetailsPostBlogView> DetailedPost
        {
            get
            {
                return _detailedPost;
            }
            set
            {
                _detailedPost = value;
                RaisePropertyChanged(() => DetailedPost);
            }
        }
       
        
    }
}
