using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin;

public class AdminEventTypesViewModel : BindableBase, IRegionAware
{

    private readonly IDataService _dataService;

    private readonly AsyncDelegateCommand OnInitializeCommand;
    public AsyncDelegateCommand SaveChangesCommand { get; }
    public AsyncDelegateCommand AddNewEventTypeCommand { get; }
    public AsyncDelegateCommand<object?> RemoveEventTypeCommand { get; }


    public AdminEventTypesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        SaveChangesCommand = new AsyncDelegateCommand(SaveChanges);
        RemoveEventTypeCommand = new AsyncDelegateCommand<object?>(RemoveEventType);
        AddNewEventTypeCommand = new AsyncDelegateCommand(AddNewEventType);


        OnInitializeCommand.Execute();
    }


    // I-REGION-AWARE //
    public bool IsNavigationTarget(NavigationContext navigationContext) => false;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
    public void OnNavigatedTo(NavigationContext navigationContext) { }


    // ON INITIALIZE //
    private async Task OnInitialize()
    {
        await LoadEventTypeList();
    }


    // LOAD EVENT-TYPES LIST FROM DB //
    private async Task LoadEventTypeList()
    {
        var eventTypesData = await _dataService.GetEventTypeList();
        EventTypeList = new ObservableCollection<EventType>(eventTypesData);
    }


    // EVENT-TYPES LIST //
    private ObservableCollection<EventType> eventTypeList = [];
    public ObservableCollection<EventType> EventTypeList
    {
        get { return eventTypeList; }
        set { SetProperty(ref eventTypeList, value); }
    }


    // NEW EVENT-TYPE //
    private EventType newEventType = new();
    public EventType NewEventType
    {
        get { return newEventType; }
        set { SetProperty(ref newEventType, value); }
    }


    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }



    // SAVE CHANGES //
    private async Task SaveChanges()
    {
        await _dataService.SaveChangesDB();
    }


    // REMOVE EVENT-TYPE //
    private async Task RemoveEventType(object? param)
    {
        if (param is not EventType eventType) return;

        await _dataService.DeleteEventType(eventType);
        await LoadEventTypeList();
    }

    // ADD EVENT-TYPE //
    private async Task AddNewEventType()
    {
        if (string.IsNullOrWhiteSpace(newEventType.Name))
        {
            ErrorMessage = "Chybí název typu eventu.";
            return;
        }

        await _dataService.AddEventType(NewEventType);

        NewEventType = new();
        ErrorMessage = null;
        await LoadEventTypeList();
    }



















}
