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
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddUser(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void AddEvent_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddEvent(null!));
          
            Assert.That(exception.ParamName, Is.EqualTo("newEwent"));
            Assert.That(exception.Message, Is.EqualTo("newEwent cannot be null! (Parameter 'newEwent')"));
        }


        [Test]
        public void AddEventType_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddEventType(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("eventType"));
            Assert.That(exception.Message, Is.EqualTo("eventType cannot be null! (Parameter 'eventType')"));
        }


        [Test]
        public void AddRoom_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddRoom(null!));
          
            Assert.That(exception.ParamName, Is.EqualTo("room"));
            Assert.That(exception.Message, Is.EqualTo("room cannot be null! (Parameter 'room')"));
        }


        [Test]
        public void AddInvitation_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.AddInvitation(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("invitation"));
            Assert.That(exception.Message, Is.EqualTo("invitation cannot be null! (Parameter 'invitation')"));
        }


        [Test]
        public void UpdateEvent_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _dataService.UpdateEvent(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("updEvent"));
            Assert.That(exception.Message, Is.EqualTo("updEvent cannot be null! (Parameter 'updEvent')"));
        }


        [Test]
        public void UpdateInvitedUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _dataService.UpdateInvitedUser(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("invitedUser"));
            Assert.That(exception.Message, Is.EqualTo("invitedUser cannot be null! (Parameter 'invitedUser')"));
        }


        [Test]
        public void UpdateInvitation_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _dataService.UpdateInvitation(null!));
            
            Assert.That(exception.ParamName, Is.EqualTo("invitation"));
            Assert.That(exception.Message, Is.EqualTo("invitation cannot be null! (Parameter 'invitation')"));
        }

        [Test]
        public void DeleteEvent_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.DeleteEvent(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("delEvent"));
            Assert.That(exception.Message, Is.EqualTo("delEvent cannot be null! (Parameter 'delEvent')"));
        }


        [Test]
        public void DeleteRoom_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.DeleteRoom(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("delRoom"));
            Assert.That(exception.Message, Is.EqualTo("delRoom cannot be null! (Parameter 'delRoom')"));
        }


        [Test]
        public void DeleteEventType_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.DeleteEventType(null!));
          
            Assert.That(exception.ParamName, Is.EqualTo("delEventType"));
            Assert.That(exception.Message, Is.EqualTo("delEventType cannot be null! (Parameter 'delEventType')"));
        }

        [Test]
        public void DeleteUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.DeleteUser(null!));
            Assert.That(exception.ParamName, Is.EqualTo("delUser"));
            Assert.That(exception.Message, Is.EqualTo("delUser cannot be null! (Parameter 'delUser')"));
        }


        [Test]
        public void GetUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetUser(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("email"));
            Assert.That(exception.Message, Is.EqualTo("email cannot be null or empty! (Parameter 'email')"));
        }


        [Test]
        public void GetUser_Empty_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetUser(""));
          
            Assert.That(exception.ParamName, Is.EqualTo("email"));
            Assert.That(exception.Message, Is.EqualTo("email cannot be null or empty! (Parameter 'email')"));
        }


        [Test]
        public void GetInvitedUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetInvitedUser(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void GetInvitation_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetInvitation(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("myEvent"));
            Assert.That(exception.Message, Is.EqualTo("myEvent cannot be null! (Parameter 'myEvent')"));
        }


        [Test]
        public void GetEventsList_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetEventsList(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void GetAceptedEventsList_byInvitedUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetAceptedEventsList_byInvitedUser(null!));
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void GetEventsList_byInvitedUser_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetEventsList_byInvitedUser(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void GetTodayEventsList_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetTodayEventsList(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }


        [Test]
        public void GetUpcomingEventsList_Null_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _dataService.GetUpcomingEventsList(null!));
           
            Assert.That(exception.ParamName, Is.EqualTo("user"));
            Assert.That(exception.Message, Is.EqualTo("user cannot be null! (Parameter 'user')"));
        }
    }
}
