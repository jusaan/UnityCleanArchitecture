using Moq;
using NUnit.Framework;
using UnityExercises.Entities.Screens.Shop;
using UnityExercises.UseCases.Screens;
using UnityExercises.Utilities.Events;
using Zenject;

namespace UnityExercises.UseCases.Tests.Screens
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