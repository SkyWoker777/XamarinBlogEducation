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
        private EditText inputCategory;
        private Button cancelCategoryButton;
        private Button applyCategoryButton;

        public DialogCategory()
        {

        }

        public DialogCategory(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.DialogCategory, null);
            
            inputCategory = view.FindViewById<EditText>(Resource.Id.inputCategory);
            cancelCategoryButton = view.FindViewById<Button>(Resource.Id.cancelCategoryButton);
            applyCategoryButton = view.FindViewById<Button>(Resource.Id.applyCategoryButton);

            var set = this.CreateBindingSet<DialogCategory, CategoryDialogViewModel>();
            set.Bind(inputCategory).To(vm => vm.NewCategory);
            set.Apply();
            cancelCategoryButton.Click += cancelCategoryButton_OnClick;
            applyCategoryButton.Click += applyCategoryButton_OnClick;
            return view;
        }

        private void applyCategoryButton_OnClick(object sender, EventArgs e)
        {
           ViewModel.AddCategoryCommand.Execute(null);
           HideSoftKeyboard();
           string toast = string.Format("New category {0} was added", inputCategory.Text);         
           Toast.MakeText(Context, toast, ToastLength.Long).Show();
           Dialog.Cancel();
           ViewModel.GoBackCommand.Execute();
        }

        private void cancelCategoryButton_OnClick(object sender, EventArgs e)
        {
            Dialog.Cancel();
        }
        public  void HideSoftKeyboard()
        {
            InputMethodManager inputMethodManager =
                (InputMethodManager)Context.GetSystemService(
                   global::Android.Content.Context.InputMethodService);

                inputMethodManager.HideSoftInputFromWindow(
                    Activity.CurrentFocus.WindowToken, 0);
            
        }
    }
}