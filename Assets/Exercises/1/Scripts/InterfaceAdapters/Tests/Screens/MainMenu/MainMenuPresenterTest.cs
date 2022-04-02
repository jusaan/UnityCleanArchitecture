using Moq;
using NUnit.Framework;
using System;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.Utilities.Events;
using Zenject;

namespace UnityExercises.InterfaceAdapters.Tests.Screens.MainMenu
{
    [TestFixture]
    public class MainMenuPresenterTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuPresenter _mainMenuPresenter;

        private Mock<IEventDispatcherService> _eventDispatcherService;
        private Mock<MainMenuViewModel> _mainMenuViewModel;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            _mainMenuViewModel = new Mock<MainMenuViewModel>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<MainMenuViewModel>().FromInstance(_mainMenuViewModel.Object);
            Container.Bind<MainMenuPresenter>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenDispatchMainMenuVisibility_UpdateTheViewModel()
        {
            var observer = new Mock<IObserver<bool>>();
            _mainMenuViewModel.Object.IsVisible.Subscribe(observer.Object);
            _eventDispatcherService.Object.Dispatch<MainMenuVisibility>();

            observer.Verify(x => x.OnNext(It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void WhenCallToDispose_UnsubscribeFromEventDispatch()
        {
            _mainMenuPresenter.Dispose();

            _eventDispatcherService.Verify(x => x.Unsubscribe(It.IsAny<Action<MainMenuVisibility>>()), Times.Once);
        }
    }
}