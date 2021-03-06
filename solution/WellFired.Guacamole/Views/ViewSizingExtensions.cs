using System;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// This static class is a bunch of helpfull layout and sizing utilities for views.
    /// </summary>
    public static class ViewSizingExtensions
    {
        /// <summary>
        /// A simple utility method that allows you to publically and programatically resize a view, call this on the parent view and all 
        /// child views will be refreshed automatically.
        /// 
        /// The Layout and sizing algorithm works as a multipass system, each pass is described below.
        /// 
        /// 1. We traverse from leaf nodes to the root node requesting size information for each view. This information is requested and might not be fullfilled.
        /// 2. We traverse from root to leaf nodes now that we have know Rect Requests. As we travers down the tree, we try to fullfill each Views Rect Request, however
        /// this might be impossible due to tight constraints on parents and if this is the case, the view will shrink to fit it's parent.
        /// 3. We finally layout our elements now we know their size data.
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="availableRegion"></param>
        public static void DoSizingAndLayout(IView view, UIRect availableRegion)
        {
            CalculateRectRequest(view);
            AttemptToFullfillRequests(view, availableRegion);

            UpdateContextIfNeeded(view as IBindableObject);
            if(view.Content != null)
                UpdateContextIfNeeded(view.Content as IBindableObject);

            DoLayout(view);
        }
        
        /// <summary>
        /// This method will traverse the tree from leaf -> root, if any of the views are invalid, it will request a resize.
        /// Due to the recursive nature of this algorithm, if the algorithm finds an invalid view, this flag will be bubbled 
        /// towards the root, so every view that should be recalculated will be.
        /// </summary>
        /// <param name="view"></param>
        /// <returns>Has the passed view been invalidated</returns>
        /// <exception cref="Exception"></exception>
        private static bool CalculateRectRequest(IView view)
        {
            if (view == null)
                return false;

            var hasInvalidated = false;

            if (view is IHasChildren hasChildren)
            {
                foreach (var child in hasChildren.Children)
                {
                    if (CalculateRectRequest(child as IView))
                        hasInvalidated = true;
                }
            }

            var content = view.Content;
            
            if(view.Equals(content))
                throw new Exception("Circular view dependency");

            if (CalculateRectRequest(content))
                hasInvalidated = true;

            if (!hasInvalidated && view.ValidRectRequest)
                return false;

            view.RectRequest = CalculateValidRectRequest(view);
            view.ContentRectRequest = view.RectRequest;
            view.ValidRectRequest = true;

            return true;
        }

        /// <summary>
        /// Takes the views Requested Rect and tries to calculate the actual rect request based on view constraints such as padding and min / max size requests.
        /// This might reduce or expand the Requested Rect based on the views constraints. 
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private static UIRect CalculateValidRectRequest(IView view)
        {
            UIRect requestedSize;
                
            switch (view)
            {
                case ICanLayout canLayout:
                    requestedSize = canLayout.Layout.CalculateValidRectRequest(canLayout.Children, view.MinSize);
                    break;
                case IListView listView:
                    requestedSize = ListViewHelper.CalculateValidRectRequest(listView);
                    break;
                default:
                    var defaultSize = UISize.Of(view.MinSize.Width, view.MinSize.Height);

                    var content = view.Content;
                    if (content != null)
                        defaultSize = content.RectRequest.Size;

                    requestedSize = UIRect.From(view.NativeRenderer?.NativeSize ?? defaultSize);
                    break;
            }
            
            var adjustedForPadding = ViewPaddingCalculation.AdjustRectRequestForPadding(view.Padding, requestedSize.Size);
            var size = ConstrainUnder(adjustedForPadding, view.MinSize);
            size = ConstrainOver(size, view.MaxSize);
            return UIRect.With(requestedSize.X, requestedSize.Y, size.Width, size.Height);
        }

        /// <summary>
        /// This method will traverse the tree from root -> leaf, trying to satisfy Requested Rects. It's possible that requested rects cannot be 
        /// fullfilled due to constraints on parents, and in this case, requested rects will shrink to fit.
        /// </summary>
        /// <param name="view">The view to fullfill</param>
        /// <param name="availableSpace">The space that is available to this view</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void AttemptToFullfillRequests(IView view, UIRect availableSpace)
        {
            var rectRequest = view.RectRequest;
            var contentRectRequest = view.ContentRectRequest;

            rectRequest.X = availableSpace.X;
            rectRequest.Y = availableSpace.Y;
            
            switch (view.HorizontalLayout)
            {
                case LayoutOptions.Fill:
                    rectRequest.Width = availableSpace.Width;
                    contentRectRequest.Width = availableSpace.Width;
                    break;
                case LayoutOptions.Center:
                    rectRequest.Width = availableSpace.Width;
                    contentRectRequest.X = availableSpace.Width / 2 - contentRectRequest.Width / 2;
                    break;
                case LayoutOptions.Expand:
                    if (rectRequest.Width > availableSpace.Width)
                        rectRequest.Width = availableSpace.Width;
                    if (contentRectRequest.Width > availableSpace.Width)
                        contentRectRequest.Width = availableSpace.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (view.VerticalLayout)
            {
                case LayoutOptions.Fill:
                    rectRequest.Height = availableSpace.Height;
                    contentRectRequest.Height = availableSpace.Height;
                    break;
                case LayoutOptions.Center:
                    rectRequest.Height = availableSpace.Height;
                    contentRectRequest.Y = availableSpace.Height / 2 - contentRectRequest.Height / 2;
                    break;
                case LayoutOptions.Expand:
                    if (rectRequest.Height > availableSpace.Height)
                        rectRequest.Height = availableSpace.Height;
                    if (contentRectRequest.Height > availableSpace.Height)
                        contentRectRequest.Height = availableSpace.Height;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var isCell = view is ICell;
            if (!isCell)
            {
                view.RectRequest = rectRequest;
                view.ContentRectRequest = contentRectRequest;
            }

            if (view.Content != null)
                AttemptToFullfillRequests(view.Content, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding);

            var layout = view as ICanLayout;
            layout?.Layout.AttemptToFullfillRequests(layout.Children, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding, view.Padding, view.HorizontalLayout, view.VerticalLayout);

            var listView = view as IListView;
            if (listView == null) 
                return;

            foreach (var child in listView.Children)
            {
                ListViewHelper.ConstrainToCell(listView, child);
                AttemptToFullfillRequests(child as IView, UIRect.With(view.ContentRectRequest.Width, view.ContentRectRequest.Height) - view.Padding);
            }
        }

        public static void UpdateContextIfNeeded(IBindableObject bindable)
        {
            if(bindable == null)
                return;

             // If something has set the binding context manually, we shouldn't override it. Otherwise, update it here.
             if (bindable.BindingContext == null)
                 bindable.BindingContext = bindable.BindingContext;

            if (!(bindable is IHasChildren children))
                return;

            foreach(var child in children.Children)
                UpdateContextIfNeeded(child as IBindableObject);
        }

        private static void DoLayout(IView view)
        {
            if (view == null)
                return;

            var layout = view as ICanLayout;
            layout?.Layout.Layout(layout.Children, view.RectRequest, view.Padding);

            if (view is IListView listView)
                ListViewHelper.Layout(listView, view.RectRequest, view.Padding);

            if(view.Content != null)
                DoLayout(view.Content);

            if (!(view is IHasChildren hasChildren))
                return;

            foreach(var child in hasChildren.Children)
                DoLayout(child as IView);
        }

        private static UISize ConstrainUnder(UISize requestedSize, UISize size)
        {
            if (size == UISize.Min)
                return requestedSize;

            if (requestedSize.Width < size.Width)
                requestedSize.Width = size.Width;
            if (requestedSize.Height < size.Height)
                requestedSize.Height = size.Height;

            return requestedSize;
        }

        private static UISize ConstrainOver(UISize requestedSize, UISize size)
        {
            if (size == UISize.Min)
                return requestedSize;

            if (requestedSize.Width > size.Width)
                requestedSize.Width = size.Width;
            if (requestedSize.Height > size.Height)
                requestedSize.Height = size.Height;

            return requestedSize;
        }
    }

    public static class ViewPaddingCalculation
    {
        public static UISize AdjustRectRequestForPadding(UIPadding padding, UISize size)
        {
            return UISize.Of(size.Width + padding.Right + padding.Left, size.Height + padding.Bottom + padding.Top);
        }
    }
}