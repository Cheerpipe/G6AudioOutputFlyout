
using System.ComponentModel;

namespace G6AudioOutputFlyout.Services.TrayIconServices
{
    public interface ITrayIconService
    {
        void Show();
        void Hide();
        void UpdateIcon(string iconName);
        void UpdateTooltip(string tooltipText);
        void Refresh();
    }
}
