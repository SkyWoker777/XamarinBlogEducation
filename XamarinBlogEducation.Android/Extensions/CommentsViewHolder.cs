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
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Extensions
{
    public class CommentsViewHolder : MvxRecyclerViewHolder
    {
        public TextView Content { get; private set; }
        public TextView CreationDate { get; private set; }
        public TextView Author { get; private set; }

        public CommentsViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            Content = itemView.FindViewById<TextView>(Resource.Id.txt_comment);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.txt_date);
            Author = itemView.FindViewById<TextView>(Resource.Id.txt_author);
            this.DelayBind(() =>
           {
               var set = this.CreateBindingSet<CommentsViewHolder, DetailedPostViewModel>();
               set.Bind(this.Content).To(x => x.CommentContent);
               set.Bind(this.CreationDate).To(x => x.CreationDate);
               set.Bind(this.Author).To(x => x.CommentAuthor);
               set.Apply();
           });
        }
    }
}