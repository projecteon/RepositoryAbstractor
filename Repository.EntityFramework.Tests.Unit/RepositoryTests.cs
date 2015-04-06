using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Repository.EntityFramework.Tests.Unit
{
	public class RepositoryTests
	{
		private readonly UnitOfWork unitOfWork;
		private readonly IDbSet<TestableEntity> dbSet;
		private readonly IRepository<TestableEntity> repository;
		private readonly IDbContext dbContext;

		public RepositoryTests()
		{
			dbSet = A.Fake<IDbSet<TestableEntity>>();
			dbContext = A.Fake<IDbContext>();
			A.CallTo(() => dbContext.DbSet<TestableEntity>()).Returns(dbSet);
			unitOfWork = new UnitOfWork(dbContext);
			repository = new Repository<TestableEntity>(unitOfWork);
		}

		private TestableEntity CreateFakeEntiy()
		{
			return new TestableEntity {Id = "someString"};
		}

		[Fact]
		public void WhenAddIsCalled_GivenValidEntity_ThenDbSetAddIsCalledOnlyOnce()
		{
			var validEntity = CreateFakeEntiy();

			repository.Add(validEntity);

			A.CallTo(() => dbSet.Add(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Fact]
		public void WhenUpdateIsCalled_GivenValidEntity_ThenSessionStoreIsCalledOnlyOnce()
		{
			var validEntity = CreateFakeEntiy();

			repository.Update(validEntity);

			A.CallTo(() => dbContext.SetModified(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Fact]
		public void WhenUpdateIsCalled_GivenValidEntity_ThenTrueIsReturned()
		{
			var validEntity = CreateFakeEntiy();

			var result = repository.Update(validEntity);

			result.ShouldBeEquivalentTo(true);
		}

		[Fact]
		public void WhenDeleteIsCalled_GivenValidEntity_ThenDbSetRemoveIsCalledOnlyOnce()
		{
			var validEntity = CreateFakeEntiy();

			repository.Delete(validEntity);

			A.CallTo(() => dbSet.Remove(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
		}
	}

	public class TestableEntity : IPersistable
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
