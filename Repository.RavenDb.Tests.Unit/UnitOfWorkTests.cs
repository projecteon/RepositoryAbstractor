using System;
using FakeItEasy;
using Raven.Client;
using Xunit;

namespace Repository.RavenDb.Tests.Unit
{
    public class UnitOfWorkTests
    {
        private readonly IDocumentSession documentSession;
        private readonly UnitOfWork unitOfWork;

        public UnitOfWorkTests()
        {
            documentSession = A.Fake<IDocumentSession>();

            unitOfWork = new UnitOfWork(documentSession);
        }

        [Fact]
        public void WhenDisposeIsCalled_ThenSessionDisposeIsCalled()
        {
            unitOfWork.Dispose();

            A.CallTo(() => documentSession.Dispose()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenCommitIsCalled_ThenSessionSaveChangesIsCalled()
        {
            unitOfWork.Commit();

            A.CallTo(() => documentSession.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenRollbackIsCalled_ThenNotImplementedExceptionIsThrown()
        {
            Assert.Throws<NotImplementedException>(() => unitOfWork.Rollback());
        }
    }
}
