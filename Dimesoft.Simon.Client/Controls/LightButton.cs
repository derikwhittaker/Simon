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
            set 
            {
                
                if (value)
                {
                    VisualStateManager.GoToState(this, "Lit", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Normal", true);
                }
            }
        }
     

        
    }
}
