using Avalonia.Markup.Xaml;
using Application = Avalonia.Application;

namespace G6AudioOutputFlyout
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
