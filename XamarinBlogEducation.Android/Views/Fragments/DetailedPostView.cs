using Android.OS;
using Android.Views;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class DetailedPostView : BaseFragment<DetailedPostViewModel>
    {
        protected override int FragmentId => Resource.Layout.DetailedPostView;
        private readonly IBlogService _blogService;
        private readonly IMvxNavigationService _navigationService;
        public DetailedPostView(IMvxNavigationService navigationService,IBlogService blogService)
        {
            _navigationService = navigationService;
            _blogService = blogService;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
        //public async Task<GetDetailsPostBlogView> GetDetailedPost()
        //{
    
        //   // var obj = await _blogService.ShowDetailedPost();
        //    //var post =  JsonConvert.DeserializeObject<DetailedPostView>(obj);

        //    return obj;
        //}
        
    }
}