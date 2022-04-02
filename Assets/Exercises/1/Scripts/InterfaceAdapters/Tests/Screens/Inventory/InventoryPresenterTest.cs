using Moq;
using NUnit.Framework;
using System;
using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.Utilities.Events;
using Zenject;

namespace UnityExercises.InterfaceAdapters.Tests.Screens.Inventory
{
    [TestFixture]
    public class InventoryPresenterTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly InventoryPresenter _inventoryPresenter;

        private Mock<IEventDispatcherService> _eventDispatcherService;
        private Mock<InventoryViewModel> _inventoryViewModel;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            _inventoryViewModel = new Mock<InventoryViewModel>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<InventoryViewModel>().FromInstance(_inventoryViewModel.Object);
            Container.Bind<InventoryPresenter>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenDispatchInventoryVisibility_UpdateTheViewModel()
        {
            var observer = new Mock<IObserver<bool>>();
            _inventoryViewModel.Object.IsVisible.Subscribe(observer.Object);
            _eventDispatcherService.Object.Dispatch<InventoryVisibility>();

            observer.Verify(x => x.OnNext(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void WhenCallToDispose_UnsubscribeFromEventDispatch()
        {
            _inventoryPresenter.Dispose();

            _eventDispatcherService.Verify(x => x.Unsubscribe(It.IsAny<Action<InventoryVisibility>>()), Times.Once);
        }
    }
}