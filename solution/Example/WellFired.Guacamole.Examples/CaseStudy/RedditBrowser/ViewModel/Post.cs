﻿using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.Model;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.ViewModel
{
    /// <summary>
    /// The Post class is the data backing our PostCell
    /// </summary>
    public class Post : CellBindingContextBase
    {
        private IImageSource _image;
        private string _author;
        private string _title;

        public IImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Post(Child child)
        {
            Image = ImageSource.From(child.Data.Thumbnail);
            Author = child.Data.Author;
            Title = child.Data.Title;
        }
    }
}