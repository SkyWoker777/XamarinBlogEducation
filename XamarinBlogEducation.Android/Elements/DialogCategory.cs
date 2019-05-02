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

        public DialogCategory()
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
            set.Bind(btnApplyNewCategory).To(vm => vm.AddCategoryCommand);

            set.Apply();
            
            return view;
        }
     
    }
}