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
using XamarinBlogEducation.Core.ViewModels.Dialogs;

namespace XamarinBlogEducation.Android.Elements
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(DeletePostDialog))]
    public class DeletePostDialog : MvxDialogFragment<DeletePostDialogViewModel>
    {

        private Button Delete;
        private Button Cancel;

        public DeletePostDialog()
        {

        }
        public DeletePostDialog(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.DeletePostDialog, null);
            Cancel = view.FindViewById<Button>(Resource.Id.CancelDeleteButton);
            Delete = view.FindViewById<Button>(Resource.Id.DeletePostButton);
            var set = this.CreateBindingSet<DeletePostDialog, DeletePostDialogViewModel>();

            Delete.Click += Delete_OnClick;
            Cancel.Click += Cancel_OnClick;
            return view;
        }

        private void Cancel_OnClick(object sender, EventArgs e)
        {
            Dialog.Cancel();
        }

        private void Delete_OnClick(object sender, EventArgs e)
        {
            ViewModel.DeleteCommand.Execute();
            string toast = string.Format("Your post was deleted");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.CancelCommand.Execute();
           
        }
    }
}