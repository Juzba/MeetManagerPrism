using System.Windows;
using System.Windows.Controls;

namespace MeetManagerPrism.Views.UserControls;

public partial class Input : UserControl
{
    public Input()
    {
        InitializeComponent();
    }


    // PLACEHOLDER
    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(Input), new PropertyMetadata("nic"));


    // BACKGROUND
    public new string Background
    {
        get { return (string)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    public static new readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register("Background", typeof(string), typeof(Input), new PropertyMetadata("#008CDB"));


    // FOREGROUND
    public new string Foreground
    {
        get { return (string)GetValue(ForegroundProperty); }
        set { SetValue(ForegroundProperty, value); }
    }

    public static new readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register("Foreground", typeof(string), typeof(Input), new PropertyMetadata("#FFD340"));


    // TEXT INPUT
    public string TextInp
    {
        get { return (string)GetValue(TextInpProperty); }
        set { SetValue(TextInpProperty, value); }
    }

    public static readonly DependencyProperty TextInpProperty =
        DependencyProperty.Register("TextInp", typeof(string), typeof(Input), new PropertyMetadata(""));



    private void Button_Clear(object sender, RoutedEventArgs e)
    {
        TextBoxInput.Clear();
    }


    private void TextBoxInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(TextBoxInput.Text)) TextBlockPlaceHolder.Visibility = Visibility.Visible;
        else { TextBlockPlaceHolder.Visibility = Visibility.Hidden; }
    }
}
