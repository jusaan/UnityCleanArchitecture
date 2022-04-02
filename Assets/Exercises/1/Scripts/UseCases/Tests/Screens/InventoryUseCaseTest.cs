using Moq;
using NUnit.Framework;
using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.UseCases.Screens;
using UnityExercises.Utilities.Events;
using Zenject;

namespace UnityExercises.UseCases.Tests.Screens
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