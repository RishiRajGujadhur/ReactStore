using Moq;
using API.BL;
using API.Entities;
using API.Data;
using Microsoft.AspNetCore.Identity;

namespace Store.Store.UnitTests.BL
{
    [TestFixture]
    public class LikeBLTests
    {
        private Mock<StoreContext> _context;
        public Mock<UserManager<User>> _userManager { get; }
        private LikeBL _likeBL;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<StoreContext>();
            //_userManager = new Mock<UserManager<User>>();
            _likeBL = new LikeBL(_context.Object, _userManager.Object);
        }

        [Test]
        public async Task GetAllLikes_Test()
        {
            await _likeBL.GetLikes();
            Assert.Pass();
        }
    }
}