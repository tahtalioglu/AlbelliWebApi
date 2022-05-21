using System;
using System.Collections.Generic;
using AutoFixture;
using Moq;

namespace AlbelliWebApi.Tests
{
    public abstract class TestBase : IDisposable
    {
        private readonly MockRepository _mockRepository;
        protected IFixture FixtureRepository { get; set; }
        protected bool VerifyAll { get; set; }

        public TestBase()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            FixtureRepository = new Fixture();
            VerifyAll = true;
            FinalizeSetUp();
        }

        protected Mock<T> MockFor<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }
        protected Mock<T> MockFor<T>(params object[] @params) where T : class
        {
            return _mockRepository.Create<T>(@params);
        }
        protected T Create<T>()
        {
            return FixtureRepository.Create<T>();
        }
        protected IEnumerable<T> CreateMany<T>()
        {
            return FixtureRepository.CreateMany<T>();
        }
        protected IEnumerable<T> CreateMany<T>(int count)
        {
            return FixtureRepository.CreateMany<T>(count);
        }
        protected void EnableCustomization(ICustomization customization)
        {
            customization.Customize(FixtureRepository);
        }
        protected virtual void FinalizeTearDown()
        {
        }
        protected virtual void FinalizeSetUp()
        {

        }

        public void Dispose()
        {
            if (VerifyAll)
            {
                _mockRepository.VerifyAll();
            }
            else
            {
                _mockRepository.Verify();
            }

            FinalizeTearDown();
        }
    }
}
