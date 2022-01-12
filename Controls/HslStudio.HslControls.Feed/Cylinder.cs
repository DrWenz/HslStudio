using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace HslStudio.HslControls.Feed
{
    public  class Cylinder: ToggleButton
    {
        private bool _isAlarmOn;
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            _isAlarmOn = !_isAlarmOn; // toggle alarm
            PseudoClasses.Set(":alarm", _isAlarmOn);
        }
    }
}
