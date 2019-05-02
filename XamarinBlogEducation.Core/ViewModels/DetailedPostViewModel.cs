using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class DetailedPostViewModel : BaseViewModel<GetAllPostResponseModel>
    {
        private string _content;
        private string _commentContent;
        private string _commentAuthor;
        private string _creationDate;
        private readonly IBlogService _blogService;
        private readonly IUserDialogs _userDialogs;
        private AddCommentRequestBlogView comment;
        private GetAllPostResponseModel _detailedPost;
        private MvxObservableCollection<GetAllCommentResponseModel> _allComments;

        public DetailedPostViewModel(IBlogService blogService, IUserDialogs userDialogs, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            _userDialogs = userDialogs;
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
            AddCommentCommand = new MvxAsyncCommand(AddComment);
            AllComments = new MvxObservableCollection<GetAllCommentResponseModel>();
        }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand AddCommentCommand { get; private set; }

        public override void Prepare(GetAllPostResponseModel parameter)
        {
            DetailedPost = parameter;
        }
        public override Task Initialize()
        {
            LoadCommentsTask = MvxNotifyTask.Create(LoadComments);
            return Task.FromResult(0);
        }

        public MvxNotifyTask LoadCommentsTask { get; private set; }

        private async Task LoadComments()
        {
            AllComments.Clear();
            List<GetAllCommentResponseModel> result = await _blogService.GetAllComments(DetailedPost.Id);
            AllComments.AddRange(result);

        }
        private async Task AddComment()
        {
            comment = new AddCommentRequestBlogView()
            {
                Content = _content,
                PostId = _detailedPost.Id
            };
            if (comment.Content == null)
            {
                _userDialogs.Toast(Strings.EmptyComment);
            }
            if (comment.Content != null)
            {
                var isResultSuccessful= await _blogService.AddComment(comment);
                if (isResultSuccessful) {_userDialogs.Toast(Strings.SuccessComment);}
                if (!isResultSuccessful) { _userDialogs.Toast(Strings.ErrorComment); }
            }
            await LoadComments();
        }


        public GetAllPostResponseModel DetailedPost
        {
            get => _detailedPost;
            set
            {
                _detailedPost = value;
                RaisePropertyChanged(() => DetailedPost);
            }
        }
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        public MvxObservableCollection<GetAllCommentResponseModel> AllComments
        {
            get => _allComments;
            set
            {
                _allComments = value;
                RaisePropertyChanged(() => AllComments);
            }
        }
        public string CommentContent
        {
            get => _commentContent;
            set
            {
                _commentContent = value;
                RaisePropertyChanged();
            }
        }
        public string CommentAuthor
        {
            get => _commentAuthor;
            set
            {
                _commentAuthor = value;
                RaisePropertyChanged();
            }
        }
        public string CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                RaisePropertyChanged();
            }
        }



    }
}
