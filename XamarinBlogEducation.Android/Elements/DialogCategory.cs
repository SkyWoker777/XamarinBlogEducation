using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using Android.InputMethodServices;

namespace XamarinBlogEducation.Android.Elements
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(DialogCategory))]
    public class DialogCategory : MvxDialogFragment<CategoryDialogViewModel>
    {
        private EditText txtCategory;
        private Button btnCancelAddCategory;
        private Button btnApplyNewCategory;
        public DialogCategory(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.DialogCategory, null);
            
            txtCategory = view.FindViewById<EditText>(Resource.Id.inputCategory);
            btnCancelAddCategory = view.FindViewById<Button>(Resource.Id.cancelCategoryButton);
            btnApplyNewCategory = view.FindViewById<Button>(Resource.Id.applyCategoryButton);

            var set = this.CreateBindingSet<DialogCategory, CategoryDialogViewModel>();
            set.Bind(txtCategory).To(vm => vm.NewCategory);
            set.Bind(btnCancelAddCategory).To(vm => vm.GoBackCommand);
            set.Apply();
            
            btnApplyNewCategory.Click += btnApplyNewCategory_OnClick;
            return view;
        }

        private void btnApplyNewCategory_OnClick(object sender, EventArgs e)
        {
           ViewModel.AddCategoryCommand.Execute();
           HideSoftKeyboard();
           var toast = string.Format("New category {0} was added", txtCategory.Text);         
           Toast.MakeText(Context, toast, ToastLength.Long).Show();
           Dialog.Cancel();
           ViewModel.GoBackCommand.Execute();
        }

        private  void HideSoftKeyboard()
        {
            InputMethodManager inputMethodManager =
                (InputMethodManager)Context.GetSystemService(
                   global::Android.Content.Context.InputMethodService);

                inputMethodManager.HideSoftInputFromWindow(
                    Activity.CurrentFocus.WindowToken, 0);
            
        }
    }
}