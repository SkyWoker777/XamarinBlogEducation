using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class AboutFragmentModel: BaseViewModel
    {
        public AboutFragmentModel(IMvxNavigationService navigationService) : base(navigationService)
        {

            GoBackCommand = new MvxAsyncCommand(GoBack);
        }
        public IMvxCommand GoBackCommand { get; private set; }
        public async Task GoBack()
        {
            await this.NavigationService.Close(this);

        }
    }
}
