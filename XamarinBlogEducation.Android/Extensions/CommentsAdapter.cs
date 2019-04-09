using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace XamarinBlogEducation.Android.Extensions
{
    public class CommentsAdapter : MvxRecyclerAdapter
    {
        public override int ItemCount => throw new NotImplementedException();
        public CommentsAdapter(IMvxAndroidBindingContext bindingContext):base(bindingContext)
        {

        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CommentsViewHolder viewHolder = holder as CommentsViewHolder;
            viewHolder.Author.Text = "";
            viewHolder.Content.Text = "";
            viewHolder.CreationDate.Text = "";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, this.BindingContext.LayoutInflaterHolder);
            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.DetailedPostView, parent, false);
            CommentsViewHolder viewHolder = new CommentsViewHolder(itemView,itemBindingContext);
            return viewHolder;

        }
    }
}