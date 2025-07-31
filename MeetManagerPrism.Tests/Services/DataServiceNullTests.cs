using MeetManagerPrism.Data;
using MeetManagerPrism.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Tests.Services
{
    public class DataServiceNullTests
    {
        private DataService _dataService;



        [SetUp]
        public void Setup()
        {

            var option = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(option);
            _dataService = new DataService(context);
        }

        [Test]
        public void AddUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddUser(null!));
        }


        [Test]
        public void AddEvent_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddEvent(null!));
        }


        [Test]
        public void AddEventType_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddEventType(null!));
        }


        [Test]
        public void AddRoom_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddRoom(null!));
        }


        [Test]
        public void AddInvitation_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddInvitation(null!));
        }


        [Test]
        public void UpdateEvent_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dataService.UpdateEvent(null!));
        }


        [Test]
        public void UpdateInvitedUser_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dataService.UpdateInvitedUser(null!));
        }


        [Test]
        public void UpdateInvitation_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dataService.UpdateInvitation(null!));
        }


        [Test]
        public void DeleteEvent_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.DeleteEvent(null!));
        }


        [Test]
        public void DeleteRoom_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.DeleteRoom(null!));
        }


        [Test]
        public void DeleteEventType_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.DeleteEventType(null!));
        }


        [Test]
        public void DeleteUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.DeleteUser(null!));
        }


        [Test]
        public void GetUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetUser(null!));
        }


        [Test]
        public void GetUser_Empty_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetUser(""));
        }


        [Test]
        public void GetInvitedUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetInvitedUser(null!));
        }


        [Test]
        public void GetInvitation_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetInvitation(null!));
        }


        [Test]
        public void GetInvitedUsersList_FromEvent_Zero_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _dataService.GetInvitedUsersList_FromEvent(0));
        }


        [Test]
        public void GetEventsList_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetEventsList(null!));
        }


        [Test]
        public void GetAceptedEventsList_byInvitedUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetAceptedEventsList_byInvitedUser(null!));
        }


        [Test]
        public void GetEventsList_byInvitedUser_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetEventsList_byInvitedUser(null!));
        }


        [Test]
        public void GetTodayEventsList_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetTodayEventsList(null!));
        }


        [Test]
        public void GetUpcomingEventsList_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _dataService.GetUpcomingEventsList(null!));
        }


    }
}
