using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Drawing;
using Android.Net;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;
using Android.Content;
using Android.Provider;
using Android.Graphics;
using Android.App;
using Refractored.Controls;
using System.Threading.Tasks;
using System.IO;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class RegisterView : BaseFragment<RegisterViewModel>
    {
        private EditText inputEmail;
        private EditText inputPassword;
        private EditText confirmPassword;
        private EditText inputUserName;
        private EditText inputLastName;
        private Button signUpButton;
        private Bitmap bitmapImage=null;
        private global::Android.Net.Uri filePath;       
        private const int PICK_IMAGE_REQUEST=71;
        private CircleImageView profileImage;
        protected override int FragmentId => Resource.Layout.RegisterView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);          
            inputEmail = view.FindViewById<EditText>(Resource.Id.inputEmail);
            inputPassword = view.FindViewById<EditText>(Resource.Id.inputPassword);
            confirmPassword = view.FindViewById<EditText>(Resource.Id.confirmPassword);
            inputUserName = view.FindViewById<EditText>(Resource.Id.inputUserName);
            inputLastName = view.FindViewById<EditText>(Resource.Id.inputLastName);
            signUpButton = view.FindViewById<Button>(Resource.Id.signUpButton);  
            profileImage = view.FindViewById<CircleImageView>(Resource.Id.profile_image);

            var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(inputLastName).To(vm => vm.LastName);
            set.Bind(confirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(inputUserName).To(vm => vm.FirstName);
            set.Bind(signUpButton).To(vm => vm.RegistrateCommand);
            set.Apply();
            signUpButton.Click += signUpButton_OnClickAsync;                  
            profileImage.Click += profileImage_OnClickAsync; ;
            return view;
        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
         
            base.OnActivityResult(requestCode, resultCode, data);   
            if ((requestCode==PICK_IMAGE_REQUEST)&&(resultCode==(int) Result.Ok) && (data != null)&&data.Data!=null)
            {
                filePath = data.Data;

                bitmapImage = MediaStore.Images.Media.GetBitmap(Context.ContentResolver, filePath);   
                profileImage.SetImageBitmap(bitmapImage);
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
        private void signUpButton_OnClickAsync(object sender, EventArgs e)
        {
            
            ViewModel.RegistrateCommand.Execute();
        }
     
        private void profileImage_OnClickAsync(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
             StartActivityForResult(Intent.CreateChooser(intent, "select pic"), PICK_IMAGE_REQUEST);
        }   
    }
}