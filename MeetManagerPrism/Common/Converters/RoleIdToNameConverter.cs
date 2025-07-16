using MeetManagerPrism.Data.Model;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace MeetManagerPrism.Common.Converters
{
    public class RoleIdToNameConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Kontrola na null a správné typy:
            if (values[0] is string roleId && values[1] is ObservableCollection<Role> roles)
            {
                var role = roles.FirstOrDefault(r => r.Id == roleId);
                return role?.RoleName ?? "";
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();


    }
}
