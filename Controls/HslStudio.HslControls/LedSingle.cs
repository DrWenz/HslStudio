using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HslStudio.HslControls
{
    public enum LedStyle
    {
        Circle, Rect, RoundedSides, Rectangle, Arrow, Diamond, Triangle
    }
    public class LedSingle : ToggleButtonBasic
    {
        public static readonly StyledProperty<LedStyle> LedStyleProperty =
             AvaloniaProperty.Register<LedSingle, LedStyle>(nameof(LedStyle), LedStyle.Circle);

        [Category("HMI")]
        public LedStyle LedStyle
        {
            get => (LedStyle)GetValue(LedStyleProperty);
            set => SetValue(LedStyleProperty, value);
        }
        private bool _isAlarmOn;
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            _isAlarmOn = !_isAlarmOn; // toggle alarm
            PseudoClasses.Set(":alarm", _isAlarmOn);
        }
        public LedSingle()
        {
            //PseudoClasses.Set(":Circle", true);
            //PseudoClasses.Set(":Rectangle", false);
        }
    }
}
