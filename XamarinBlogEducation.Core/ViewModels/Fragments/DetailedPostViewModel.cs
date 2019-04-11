using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class DetailedPostViewModel : BaseViewModel<GetAllPostsBlogViewItem>
    {
        private string _content;
        private string _commentContent;
        private string _commentAuthor;
        private string _creationDate;
        private IBlogService _blogService;
        private AddCommentBlogViewModel comment;
        private GetAllPostsBlogViewItem _detailedPost;
        private MvxObservableCollection<GetAllCommentsBlogViewItem> _allComments;
        public DetailedPostViewModel(IBlogService blogService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            GoBackCommand = new MvxAsyncCommand(GoBackAsync);
            AddCommentCommand = new MvxAsyncCommand(AddComment);
            AllComments = new MvxObservableCollection<GetAllCommentsBlogViewItem>();
        }
        public override void Prepare(GetAllPostsBlogViewItem parameter)
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
            var result = await _blogService.GetAllComments(DetailedPost.Id);
            AllComments.AddRange(result);

        }
        private async Task GoBackAsync()
        {
            await this.NavigationService.Close(this);
        }
        private async Task AddComment()
        {
            comment = new AddCommentBlogViewModel()
            {
                Content = _content,
                PostId = _detailedPost.Id
            };
            await _blogService.AddComment(comment);
            await LoadComments();
        }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand AddCommentCommand { get; private set; }
        public GetAllPostsBlogViewItem DetailedPost
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
        public MvxObservableCollection<GetAllCommentsBlogViewItem> AllComments
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
