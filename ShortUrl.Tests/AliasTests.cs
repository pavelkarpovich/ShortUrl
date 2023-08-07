using Moq;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;

namespace ShortUrl.Tests
{
    public class Tests
    {
        private Mock<IRepository<AliasUrl>> mockRepo;

        [SetUp]
        public void Setup()
        {
            mockRepo = new Mock<IRepository<AliasUrl>>();

        }

        [Test]
        public void GetUrl()
        {
            mockRepo.Setup(m => m.Items).Returns((new AliasUrl[] {
                new AliasUrl ("alias1", "url1", "user1"),
                new AliasUrl ("alias2", "url2", "user2"),
                new AliasUrl ("alias3", "url3", "user3"),
                new AliasUrl ("alias4", "url4", "user4"),
                new AliasUrl ("alias5", "url5", "user5"),
            }).AsQueryable());

            var aliasUrlItem = mockRepo.Object.Items.Where(x => x.Alias == "alias4").FirstOrDefault();

            var actualUrl = aliasUrlItem.Url;

            Assert.That(actualUrl, Is.EqualTo("url4"));
        }

        [Test]
        public void DeleteAlias()
        {
            mockRepo.Setup(m => m.Items).Returns((new AliasUrl[] {
                new AliasUrl ("alias1", "url1", "user1"),
                new AliasUrl ("alias2", "url2", "user2"),
                new AliasUrl ("alias3", "url3", "user3"),
                new AliasUrl ("alias4", "url4", "user4"),
            }).AsQueryable());

            var aliasUrl = mockRepo.Object.Items.Where(x => x.Alias == "alias3");
            var deleted = mockRepo.Object.Items.Except(aliasUrl);

            var count = deleted.Where(x => x.Alias == "alias3").Count();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllAliasesOfUser()
        {
            mockRepo.Setup(m => m.Items).Returns((new AliasUrl[] {
                new AliasUrl ("alias1", "url1", "user1"),
                new AliasUrl ("alias2", "url2", "user2"),
                new AliasUrl ("alias3", "url3", "user1"),
                new AliasUrl ("alias4", "url4", "user4"),
                new AliasUrl ("alias5", "url5", "user1"),
                new AliasUrl ("alias6", "url6", "user6"),
                new AliasUrl ("alias7", "url7", "user7"),
            }).AsQueryable());

            var count = mockRepo.Object.Items.Where(x => x.UserId == "user1").Count();

            Assert.That(count, Is.EqualTo(3));
        }
    }
}