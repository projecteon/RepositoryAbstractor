using System;
using FakeItEasy;
using Xunit;

namespace Repository.EntityFramework.Tests.Unit
{
	public class UnitOfWorkTests
	{
		private readonly IDbContext documentSession;
		private readonly UnitOfWork unitOfWork;

		public UnitOfWorkTests()
		{
			documentSession = A.Fake<IDbContext>();

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
