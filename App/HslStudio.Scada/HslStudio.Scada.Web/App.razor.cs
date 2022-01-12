using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;

namespace HslStudio.Scada.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        WebAppBuilder.Configure<HslStudio.Scada.App>()
            .UseReactiveUI()
            .SetupWithSingleViewLifetime();
    }
}