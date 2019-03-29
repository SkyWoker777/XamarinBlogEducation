using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Core.ViewModels.Dialogs;

namespace XamarinBlogEducation.Android.Elements
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(PasswordDialog))]
    public class PasswordDialog : MvxDialogFragment<ChangePasswordDialogViewModel>
    {
        private EditText inputOldPassword;
        private EditText inputNewPassword;
        private EditText inputComfirmPassword;
        private Button cancelPasswordChangeButton;
        private Button applyPaswordChangeButton;

        public PasswordDialog()
        {

        }

        public PasswordDialog(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.PasswordDialog, null);

            inputOldPassword = view.FindViewById<EditText>(Resource.Id.inputOldPassword);
            inputNewPassword = view.FindViewById<EditText>(Resource.Id.inputNewPassword);
            inputComfirmPassword = view.FindViewById<EditText>(Resource.Id.inputComfirmPassword);
            cancelPasswordChangeButton = view.FindViewById<Button>(Resource.Id.cancelPasswordChangeButton);
            applyPaswordChangeButton = view.FindViewById<Button>(Resource.Id.applyPaswordChangeButton);

            var set = this.CreateBindingSet<PasswordDialog, ChangePasswordDialogViewModel>();
            set.Bind(inputOldPassword).To(vm => vm.OldPassword);
            set.Bind(inputNewPassword).To(vm => vm.NewPassword);
            set.Bind(inputComfirmPassword).To(vm => vm.ComfirmPassword);
            set.Apply();
            cancelPasswordChangeButton.Click += cancelPasswordChangeButton_OnClick;
            applyPaswordChangeButton.Click+= applyPaswordChangeButton_OnClick;
            return view;
        }

        private void applyPaswordChangeButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.ChangePasswordCommand.Execute();
            string toast = string.Format("Your password was changed");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            Dialog.Cancel();
            ViewModel.GoBackCommand.Execute();
        }

        private void cancelPasswordChangeButton_OnClick(object sender, EventArgs e)
        {
            Dialog.Cancel();
        }

    }
}