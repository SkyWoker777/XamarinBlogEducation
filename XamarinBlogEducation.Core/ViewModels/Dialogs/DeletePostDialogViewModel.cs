using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
    public class DeletePostDialogViewModel : BaseViewModel<GetAllPostsBlogViewItem>
    {
        public GetAllPostsBlogViewItem currentPost;
        private readonly IBlogService _blogService;
        public DeletePostDialogViewModel(IMvxNavigationService navigationService, IBlogService blogService) : base(navigationService)
        {
            _blogService = blogService;
            CancelCommand = new MvxAsyncCommand(async()=>await NavigationService.Navigate<UserPostsViewModel>()) ;
            DeleteCommand = new MvxAsyncCommand(DeleteAsync);
        }
        public IMvxCommand CancelCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }

        public override void Prepare(GetAllPostsBlogViewItem parameter)
        {
            currentPost = parameter;
        }     
        private async Task DeleteAsync()
        {
            await _blogService.DeletePost(currentPost.Id);
        }

    }
}
