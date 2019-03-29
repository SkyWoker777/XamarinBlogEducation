using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Refractored.Controls;
using System;
using System.IO;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class UserProfileView : BaseFragment<UserProfileViewModel>
    {
        private EditText editEmail;
        private EditText editUserName;
        private EditText editLastName;
        private Button applyButton;
        private Button changePasswordButton;
        private Bitmap bitmapImage = null;
        private global::Android.Net.Uri filePath;
        private const int PICK_IMAGE_REQUEST = 71;
        private CircleImageView updateProfileImage;
        protected override int FragmentId => Resource.Layout.UserProfileViewModel;
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            editEmail = view.FindViewById<EditText>(Resource.Id.editEmail);
            editUserName = view.FindViewById<EditText>(Resource.Id.editUserName);
            editLastName = view.FindViewById<EditText>(Resource.Id.editLastName);
            applyButton = view.FindViewById<Button>(Resource.Id.applyButton);
            changePasswordButton = view.FindViewById<Button>(Resource.Id.changePasswordButton);
            updateProfileImage = view.FindViewById<CircleImageView>(Resource.Id.updateProfileImage);

            var set = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();
            set.Bind(editEmail).To(vm => vm.Email);
            set.Bind(editLastName).To(vm => vm.LastName);
            set.Bind(editUserName).To(vm => vm.FirstName);           
            set.Bind(changePasswordButton).To(vm => vm.ChangePasswordCommand);
            set.Apply();
            updateProfileImage.Click += updateProfileImage_OnClickAsync;
            applyButton.Click += applyButton_OnClick;
            return view;
        }

        
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((requestCode == PICK_IMAGE_REQUEST) && (resultCode == (int)Result.Ok) && (data != null) && data.Data != null)
            {
                filePath = data.Data;

                bitmapImage = MediaStore.Images.Media.GetBitmap(Context.ContentResolver, filePath);
                updateProfileImage.SetImageBitmap(bitmapImage);
                ViewModel.UserImage = bitmapToByteArray(bitmapImage);
            }
        }
        private byte[] bitmapToByteArray(Bitmap bitmapImage)
        {
            MemoryStream stream = new MemoryStream();
            bitmapImage.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
            byte[] bitmapData = stream.ToArray();
            return bitmapData;
        }

        private void updateProfileImage_OnClickAsync(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            StartActivityForResult(Intent.CreateChooser(intent, "select pic"), PICK_IMAGE_REQUEST);
        }
        private void applyButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.UpdateCommand.Execute();
            string toast = string.Format("All changes was saved");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }

    }
}