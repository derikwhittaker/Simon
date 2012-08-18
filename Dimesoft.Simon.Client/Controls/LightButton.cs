using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dimesoft.Simon.Client.Controls
{
    class LightButton: Button
    {

        public LightButton()
        {
            DefaultStyleKey = typeof(LightButton);
            this.ButtonColor = Colors.White;
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
        public static readonly DependencyProperty IsLitProperty =
            DependencyProperty.Register("IsLit", typeof(bool), typeof(LightButton), new PropertyMetadata(false));

        public static void OnIsLitChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is LightButton)
            {
                var Sender = (LightButton)sender;

                if (e.NewValue is bool && (bool)e.NewValue)
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
