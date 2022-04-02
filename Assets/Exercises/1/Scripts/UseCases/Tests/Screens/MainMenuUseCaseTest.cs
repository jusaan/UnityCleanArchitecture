using Moq;
using NUnit.Framework;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.UseCases.Screens;
using UnityExercises.Utilities.Events;
using Zenject;

namespace UnityExercises.UseCases.Tests.Screens
{
    [TestFixture]
    public class MainMenuUseCaseTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuUseCase _mainMenuUseCase;

        private Mock<IEventDispatcherService> _eventDispatcherService;

        [SetUp]
        public void SetUp()
        {
            _eventDispatcherService = new Mock<IEventDispatcherService>();
            Container.Bind<IEventDispatcherService>().FromInstance(_eventDispatcherService.Object);
            Container.Bind<MainMenuUseCase>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenCallToSetActive_DispatchMainMenuVisibility()
        {
            _mainMenuUseCase.SetActive(It.IsAny<bool>());

            _eventDispatcherService.Verify(x => x.Dispatch(It.IsAny<MainMenuVisibility>()), Times.Once);
        }
    }
}