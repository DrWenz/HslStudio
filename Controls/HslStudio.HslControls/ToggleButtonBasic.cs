using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HslStudio.HslControls
{
    public class ToggleButtonBasic : ToggleButton
    {
        public static readonly StyledProperty<bool> ValueProperty =
           AvaloniaProperty.Register<ToggleButtonBasic,bool>(nameof(Value));


        [Category("HMI")]
        public bool Value
        {
            get => (bool)base.GetValue(ValueProperty);
            set => base.SetValue(ValueProperty, value);
        }
        #region DependencyProperty

        /// <summary>
        /// Color
        /// </summary>
        public static readonly StyledProperty<Color> OnColorProperty =
        AvaloniaProperty.Register<ToggleButtonBasic, Color>(nameof(OnColor), Colors.Green);

        public static readonly StyledProperty<Color> OffColorProperty =
        AvaloniaProperty.Register<ToggleButtonBasic, Color>(nameof(OffColor), Colors.Red);

        public static readonly StyledProperty<string> OnTextProperty =
        AvaloniaProperty.Register<ToggleButtonBasic, string>(nameof(OnText), "On");

        public static readonly StyledProperty<string> OffTextProperty =
        AvaloniaProperty.Register<ToggleButtonBasic, string>(nameof(OffText), "Off");

      
 

        #endregion

        #region Public Properties

        /// <summary>
        /// Color on
        /// </summary>
        [Category("HMI")]
        public Color OnColor
        {
            set => SetValue(OnColorProperty, value);
            get => (Color)GetValue(OnColorProperty);
        }
        /// <summary>
        /// Color off
        /// </summary>
        [Category("HMI")]
        public Color OffColor
        {
            set => SetValue(OffColorProperty, value);
            get => (Color)GetValue(OffColorProperty);
        }
        /// <summary>
        /// OnText
        /// </summary>
        [Category("HMI")]
        public string OnText
        {
            set => SetValue(OnTextProperty, value);
            get => (string)GetValue(OnTextProperty);
        }
        /// <summary>
        /// OnText
        /// </summary>
        [Category("HMI")]
        public string OffText
        {
            set => SetValue(OffTextProperty, value);
            get => (string)GetValue(OffTextProperty);
        }
        #endregion
    }
}
