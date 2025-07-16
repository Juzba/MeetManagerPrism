/*

using MeetManagerWPF.Services;
using System.Windows;

namespace MeetManagerWPF.ViewModel
{
    public partial class HomeViewModel(IHashService hashService) : ObservableObject
    {
        private readonly IHashService _hashService = hashService;


        [ObservableProperty]
        private string text = "fsfd";


        [RelayCommand]
        private void ButtonClick()
        {
            Text = _hashService.HashPassword("123456");
            
        }







    }
}
*/