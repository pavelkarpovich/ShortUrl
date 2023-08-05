using Microsoft.EntityFrameworkCore;
using Moq;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;
using ShortUrl.Web.Services;

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
                new AliasUrl ("alias2", "url2", "user2")
            }).AsQueryable());

            var aliasUrl = mockRepo.Object.Items.Where(x => x.AliasName == "alias1").FirstOrDefault();

            var actualUrl = aliasUrl.Url;

            Assert.That(actualUrl, Is.EqualTo("url1"));
        }

        [Test]
        public void DeleteAlias()
        {
            mockRepo.Setup(m => m.Items).Returns((new AliasUrl[] {
                new AliasUrl ("alias1", "url1", "user1"),
                new AliasUrl ("alias2", "url2", "user2")
            }).AsQueryable());

            var aliasUrl = mockRepo.Object.Items.Where(x => x.AliasName == "alias1");
            var deleted = mockRepo.Object.Items.Except(aliasUrl);

            var count = deleted.Where(x => x.AliasName == "alias1").Count();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllAliasesOfUser()
        {
            mockRepo.Setup(m => m.Items).Returns((new AliasUrl[] {
                new AliasUrl ("alias1", "url1", "user1"),
                new AliasUrl ("alias2", "url2", "user2"),
                new AliasUrl ("alias3", "url3", "user1")
            }).AsQueryable());

            var count = mockRepo.Object.Items.Where(x => x.UserId == "user1").Count();

            Assert.That(count, Is.EqualTo(2));
        }
    }
}