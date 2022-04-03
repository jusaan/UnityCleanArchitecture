using Moq;
using NUnit.Framework;
using UnityCleanArchitecture.Entities.Screens.Inventory;
using UnityCleanArchitecture.UseCases.Screens;
using UnityCleanArchitecture.Utilities.Events;
using Zenject;

namespace UnityCleanArchitecture.UseCases.Tests.Screens
{
    [TestFixture]
    public class InventoryUseCaseTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly InventoryUseCase _inventoryUseCase;

        private Mock<IEventDispatcherService> _eventDispatcherService;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<InventoryUseCase>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenCallToSetActive_DispatchInventoryVisibility()
        {
            _inventoryUseCase.SetActive(It.IsAny<bool>());

            _eventDispatcherService.Verify(x => x.Dispatch(It.IsAny<InventoryVisibility>()), Times.Once);
        }
    }
}