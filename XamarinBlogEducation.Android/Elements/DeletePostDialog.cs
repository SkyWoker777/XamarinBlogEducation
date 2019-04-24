using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.Core.Resources;
namespace XamarinBlogEducation.Android.Elements
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(DeletePostDialog))]
    public class DeletePostDialog : MvxDialogFragment<DeletePostDialogViewModel>
    {

        private Button btnDelete;
        private Button btnCancel;

        public DeletePostDialog(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.DeletePostDialog, null);
            btnCancel = view.FindViewById<Button>(Resource.Id.CancelDeleteButton);
            btnDelete = view.FindViewById<Button>(Resource.Id.DeletePostButton);

            var set = this.CreateBindingSet<DeletePostDialog, DeletePostDialogViewModel>();
            set.Bind(btnCancel).To(vm => vm.CancelCommand);
            set.Apply();

            btnDelete.Click += btnDelete_OnClick;
            return view;
        }

        private void btnDelete_OnClick(object sender, EventArgs e)
        {
            ViewModel.DeleteCommand.Execute();
            string toast = string.Format("Your post was deleted");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.CancelCommand.Execute();

        }
    }
}