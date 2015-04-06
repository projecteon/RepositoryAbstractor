namespace Repository.RavenDb.Tests.Unit
{
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;

    using FizzWare.NBuilder;

    using FluentAssertions;

    using Raven.Client;

    using Xunit;

    public class RepositoryTests
    {
        private readonly IDocumentSession session;

        private readonly IRepository<TestableEntity> repository;

        public RepositoryTests()
        {
            this.session = A.Fake<IDocumentSession>();
            this.repository = new Repository<TestableEntity>(this.session);
        }

        private TestableEntity CreateFakeEntiy()
        {
            return new TestableEntity { Id = "someString" };
        }

        [Fact]
        public void WhenAddIsCalled_GivenValidEntity_ThenSessionStoreIsCalledOnlyOnce()
        {
            var validEntity = CreateFakeEntiy();

            repository.Add(validEntity);

            A.CallTo(() => session.Store(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenUpdateIsCalled_GivenValidEntity_ThenSessionStoreIsCalledOnlyOnce()
        {
            var validEntity = CreateFakeEntiy();

            repository.Update(validEntity);

            A.CallTo(() => session.Store(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenUpdateIsCalled_GivenValidEntity_ThenTrueIsReturned()
        {
            var validEntity = CreateFakeEntiy();

            var result = repository.Update(validEntity);

            result.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void WhenDeleteIsCalled_GivenValidEntity_ThenSessionDeleteIsCalledOnlyOnce()
        {
            var validEntity = CreateFakeEntiy();

            repository.Delete(validEntity);

            A.CallTo(() => session.Delete(validEntity)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenSingleByIsCalled_GivenId_ThenSessionLoadWithIdIsCalledOnlyOnce()
        {
            repository.SingleBy("someId");

            A.CallTo(() => session.Load<TestableEntity>("someId")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void WhenSingleByIsCalled_GivenPredicate_ThenSessionQueryWithPredicateIsCalledOnlyOnce()
        {
            var testableEntities = new List<TestableEntity> { CreateFakeEntiy() };
            A.CallTo(() => session.Query<TestableEntity>()).Returns(new FakeRavenQueryable<TestableEntity>(testableEntities.AsQueryable()));

            repository.SingleBy(x => x.Id == "someId");

            A.CallTo(() => session.Query<TestableEntity>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void GivenGetAllIsCalled_WhenNoPagingIsSpecified_ThenSessionQueryIsCalledOnlyOnce()
        {
            repository.GetAll();

            A.CallTo(() => session.Query<TestableEntity>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void GivenGetAllIsCalled_WhenPageStartIsTwoAndPageSizeIsTwo_ThenObjectsThreeToFiveFromSessionIsReturned()
        {
            var testableEntities = Builder<TestableEntity>.CreateListOfSize(10).Build();;
            A.CallTo(() => session.Query<TestableEntity>()).Returns(new FakeRavenQueryable<TestableEntity>(testableEntities.AsQueryable()));

            var result = repository.GetAll(2, 2);

            Assert.Equal(result, testableEntities.Skip(2).Take(2));
        }

        [Fact]
        public void GivenFilterByIsCalled_WhenFilteredByName_ThenOnlyEntityWithThatNameIsReturned()
        {
            var testableEntities = Builder<TestableEntity>.CreateListOfSize(10)
                                                          .Random(1)
                                                                .With(x => x.Name = "specificName")
                                                          .Build(); ;
            A.CallTo(() => session.Query<TestableEntity>()).Returns(new FakeRavenQueryable<TestableEntity>(testableEntities.AsQueryable()));

            var result = repository.FilterBy(x => x.Name == "specificName");

            Assert.Equal(result.Count(), 1);
            Assert.Equal(result.Single().Name, "specificName");
        }
    }

    public class TestableEntity : IPersistable
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
