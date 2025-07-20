using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManagerPrism.ViewModels.Manager
{
    public class ManagerEventsViewModel : BindableBase
    {
        private readonly IDataService _dataService;

        public ManagerEventsViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }


        // EVENT LIST //
        private ObservableCollection<Event> eventsList = [new Event(){ Name = "Muj Event", Description=" blabla"}];
        public ObservableCollection<Event> EventsList
        {
            get { return eventsList; }
            set { SetProperty(ref eventsList, value); }
        }











    }
}
