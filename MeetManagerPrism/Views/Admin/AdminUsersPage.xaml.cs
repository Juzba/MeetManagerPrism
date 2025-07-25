using System.Windows;
using System.Windows.Controls;

namespace MeetManagerPrism.Views.Admin;

public partial class AdminUsersPage : UserControl
{
    public AdminUsersPage()
    {
        InitializeComponent();
    }

    private void ComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is ComboBox combo) combo.IsDropDownOpen = true;
    }
}
