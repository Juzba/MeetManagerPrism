using System.Windows;
using System.Windows.Controls;

namespace MeetManagerPrism.Views.Admin;

public partial class AdminPage : UserControl
{
    public AdminPage()
    {
        InitializeComponent();
    }

    private void ComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is ComboBox combo) combo.IsDropDownOpen = true;
    }
}
