using Moq;
using NUnit.Framework;
using System;
using UnityCleanArchitecture.Entities.Screens.Shop;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Shop;
using UnityCleanArchitecture.Utilities.Events;
using Zenject;

namespace UnityCleanArchitecture.InterfaceAdapters.Tests.Screens.Shop
{
    [TestFixture]
    public class ShopPresenterTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ShopPresenter _shopPresenter;

        private Mock<IEventDispatcherService> _eventDispatcherService;
        private Mock<ShopViewModel> _shopViewModel;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            _shopViewModel = new Mock<ShopViewModel>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<ShopViewModel>().FromInstance(_shopViewModel.Object);
            Container.Bind<ShopPresenter>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenDispatchShopVisibility_UpdateTheViewModel()
        {
            var observer = new Mock<IObserver<bool>>();
            _shopViewModel.Object.IsVisible.Subscribe(observer.Object);
            _eventDispatcherService.Object.Dispatch<ShopVisibility>();

            observer.Verify(x => x.OnNext(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void WhenCallToDispose_UnsubscribeFromEventDispatch()
        {
            _shopPresenter.Dispose();

            _eventDispatcherService.Verify(x => x.Unsubscribe(It.IsAny<Action<ShopVisibility>>()), Times.Once);
        }
    }
}