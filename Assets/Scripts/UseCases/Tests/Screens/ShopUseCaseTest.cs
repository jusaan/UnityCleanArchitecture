using Moq;
using NUnit.Framework;
using UnityCleanArchitecture.Entities.Screens.Shop;
using UnityCleanArchitecture.UseCases.Screens;
using UnityCleanArchitecture.Utilities.Events;
using Zenject;

namespace UnityCleanArchitecture.UseCases.Tests.Screens
{
    [TestFixture]
    public class ShopUseCaseTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ShopUseCase _shopUseCase;

        private Mock<IEventDispatcherService> _eventDispatcherService;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<ShopUseCase>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenCallToSetActive_DispatchShopVisibility()
        {
            _shopUseCase.SetActive(It.IsAny<bool>());

            _eventDispatcherService.Verify(x => x.Dispatch(It.IsAny<ShopVisibility>()), Times.Once);
        }
    }
}