using MeetManagerPrism.Data.Model;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace MeetManagerPrism.Common.Converters
{
    public class CalendarDateBoolConverter : IMultiValueConverter
    {


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is DateTime date && values[1] is ObservableCollection<Event> eventList)
            {
                return eventList.Any(p => p.StartDate <= date && p.EndDate >= date);
            }

            return false;
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
