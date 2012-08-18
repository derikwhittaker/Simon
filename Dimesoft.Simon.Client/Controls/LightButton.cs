using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Dimesoft.Simon.Client.Controls
{
    class LightButton: Button
    {
        private Path _glow = null;

        public LightButton()
        {
            DefaultStyleKey = typeof(LightButton);
            this.ButtonColor = Colors.White;
        }

        protected override void OnTapped(Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            base.OnTapped(e);

            if (_glow != null)
            {
                Storyboard b = new Storyboard();

               
                DoubleAnimation da = new DoubleAnimation();
                da.To = 0.75;
                da.From = 0;
                da.Duration = new Duration(new TimeSpan(0,0,0,0,200));
                da.AutoReverse = true;

                Storyboard.SetTarget(da,_glow);
                Storyboard.SetTargetProperty(da,"Opacity");

                b.Children.Add(da);

                b.Begin();
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_glow == null)
            {
                object g = this.GetTemplateChild("Lightbulb");
                if (g is Path)
                {
                    _glow = (Path)g;
                }

            }
        }


        public Color ButtonColor
        {
            get { return (Color)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(Color), typeof(LightButton), new PropertyMetadata(null));




        public bool IsLit
        {
            get { return (bool)GetValue(IsLitProperty); }
            set { SetValue(IsLitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLitProperty =
            DependencyProperty.Register("IsLit", typeof(bool), typeof(LightButton), new PropertyMetadata(false, IsLitChanged));

        
        public static void IsLitChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if(sender is LightButton)
            {
                var Sender = (LightButton)sender;

                if (e.NewValue != null && (bool)e.NewValue)
                {
                    VisualStateManager.GoToState(Sender, "Lit", true);
                }
                else
                {
                    VisualStateManager.GoToState(Sender, "Normal", true);
                }

            }
        }
     

        
    }
}
