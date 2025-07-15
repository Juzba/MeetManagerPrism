
namespace MeetManagerWPF.Services;

public class NavigateMessage(Type pageType, string frameName)
{
    public Type PageType { get; } = pageType;
    public string FrameName { get; } = frameName;
}

