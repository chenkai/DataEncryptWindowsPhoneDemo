using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace DataEncryptBuildDemo.UserControls
{
    public partial class PopupCotainer : UserControl
    {
        #region Constructor

        public PopupCotainer(PhoneApplicationPage basePage)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PopupCotainer_Loaded);
            _BasePage = basePage;
            _BasePage.BackKeyPress += BasePage_BackKeyPress;
        }

        #endregion

        #region Property

        private PhoneApplicationPage _BasePage;
        private FrameworkElement _PopupContent { get; set; }
        Popup _Popup;
        CubicEase _EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };

        public bool IsShown
        {
            get
            {
                return _Popup.IsOpen;
            }
        }

        #endregion

        #region Private Method

        private void PopupCotainer_Loaded(object sender, RoutedEventArgs e)
        {
            var story = PrepareShowStory();
            story.Begin();
        }

        private void BasePage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
            e.Cancel = true;
        }

        private Storyboard PrepareShowStory()
        {
            contentArea.Children.Add(_PopupContent);
            UpdateLayout();

            Storyboard story = new Storyboard();
            DoubleAnimation animation;
            animation = new DoubleAnimation();
            animation.From = 0 - popupArea.ActualHeight;
            animation.To = SystemTray.IsVisible ? 32 : 0;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            animation.EasingFunction = _EasingFunction;
            Storyboard.SetTarget(animation, popupTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
            story.Children.Add(animation);

            animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 0.5;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(animation, mask);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Opacity)"));
            story.Children.Add(animation);

            return story;
        }

        private Storyboard PrepareCloseStory()
        {
            Storyboard story = new Storyboard();
            DoubleAnimation animation;

            story.Completed += new EventHandler(StoryReverse_Completed);
            animation = new DoubleAnimation();
            animation.From = popupTransform.TranslateY;
            animation.To = 0 - popupArea.ActualHeight;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            animation.EasingFunction = _EasingFunction;
            Storyboard.SetTarget(animation, popupTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
            story.Children.Add(animation);

            animation = new DoubleAnimation();
            animation.From = mask.Opacity;
            animation.To = 0;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(animation, mask);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Opacity)"));
            story.Children.Add(animation);

            return story;
        }

        private void StoryReverse_Completed(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void ClosePopup()
        {
            contentArea.Children.Clear();
            _PopupContent = null;
            _BasePage = null;

            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }
        }

        private void mask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        #endregion

        #region Public Method

        public void Show(FrameworkElement popupContent)
        {
            _PopupContent = popupContent;
            _Popup = new Popup();
            _Popup.Child = this;
            _Popup.IsOpen = true;
        }

        public void Close()
        {
            _BasePage.BackKeyPress -= BasePage_BackKeyPress;
            var story = PrepareCloseStory();
            story.Begin();
        }

        #endregion
    }

    public static class PopupManager
    {
        public static void CloseMeAsPopup(this FrameworkElement popupContent)
        {
            var contentArea = popupContent.Parent as Grid;
            if (contentArea != null)
            {
                var popupArea = contentArea.Parent as Grid;
                if (popupArea != null)
                {
                    var layoutRoot = popupArea.Parent as Grid;
                    if (layoutRoot != null)
                    {
                        var container = layoutRoot.Parent as PopupCotainer;
                        if (container != null)
                        {
                            container.Close();
                        }
                    }
                }
            }
        }
    }
}
