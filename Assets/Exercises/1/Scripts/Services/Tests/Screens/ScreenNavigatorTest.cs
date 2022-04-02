using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityExercises.InterfaceAdapters.Screens;
using UnityExercises.Utilities.Helpers;
using UnityExercises.Utilities.Interactables;
using Zenject;

namespace UnityExercises.Services.Screens
{
    [TestFixture]
    public class ScreenNavigatorTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ScreenNavigator _screenNavigator;

        private Mock<ScreenNavigatorViewModel> _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            _screenNavigatorViewModel = new Mock<ScreenNavigatorViewModel>();
            Container.Bind<ScreenNavigator>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .WithArguments(_screenNavigatorViewModel.Object);

            Container.Inject(this);

            ReflectionHelper.InvokeInstanceMethod(_screenNavigator, "Awake");
        }

        [Test]
        public void WhenActualScreenIsNotNullAndReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var screensHistory = ReflectionHelper.GetInstanceField<Stack<IActivable>>(_screenNavigator, "_screensHistory");
            var oldActualScreen = new Mock<IActivable>();
            ReflectionHelper.SetInstanceField(_screenNavigator, "_actualScreen", oldActualScreen.Object);
            var newActualScreen = new Mock<IActivable>();
            _screenNavigatorViewModel.Object.SetActualScreen.Execute(newActualScreen.Object);

            oldActualScreen.Verify(x => x.SetActive(false), Times.Once);
            Assert.IsTrue(screensHistory.Any(item => item == oldActualScreen.Object));
            var actualScreenFromInstance = ReflectionHelper.GetInstanceField<IActivable>(_screenNavigator, "_actualScreen");
            Assert.IsTrue(actualScreenFromInstance == newActualScreen.Object);
            newActualScreen.Verify(x => x.SetActive(true), Times.Once);
        }

        [Test]
        public void WhenActualScreenIsNullAndReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var screensHistory = ReflectionHelper.GetInstanceField<Stack<IActivable>>(_screenNavigator, "_screensHistory");
            var newActualScreen = new Mock<IActivable>();
            _screenNavigatorViewModel.Object.SetActualScreen.Execute(newActualScreen.Object);

            Assert.IsFalse(screensHistory.Any());
            var actualScreenFromInstance = ReflectionHelper.GetInstanceField<IActivable>(_screenNavigator, "_actualScreen");
            Assert.IsTrue(actualScreenFromInstance == newActualScreen.Object);
            newActualScreen.Verify(x => x.SetActive(true), Times.Once);
        }

        [Test]
        public void WhenScreensHistoryIsNotEmptyAndReceiveCommandOnGoToButtonPressed_CallToBackToPreviousScreen()
        {
            var screensHistory = ReflectionHelper.GetInstanceField<Stack<IActivable>>(_screenNavigator, "_screensHistory");
            var previousScreen = new Mock<IActivable>();
            screensHistory.Push(previousScreen.Object);

            var actualScreen = new Mock<IActivable>();
            ReflectionHelper.SetInstanceField(_screenNavigator, "_actualScreen", actualScreen.Object);
            _screenNavigatorViewModel.Object.BackToPreviousScreen.Execute();

            actualScreen.Verify(x => x.SetActive(false), Times.Once);
            var actualScreenFromInstance = ReflectionHelper.GetInstanceField<IActivable>(_screenNavigator, "_actualScreen");
            Assert.IsTrue(actualScreenFromInstance == previousScreen.Object);
            previousScreen.Verify(x => x.SetActive(true), Times.Once);
        }

        [Test]
        public void WhenScreensHistoryIsEmptyAndReceiveCommandOnGoToButtonPressed_CallToBackToPreviousScreen()
        {
            var screensHistory = ReflectionHelper.GetInstanceField<Stack<IActivable>>(_screenNavigator, "_screensHistory");

            var actualScreen = new Mock<IActivable>();
            ReflectionHelper.SetInstanceField(_screenNavigator, "_actualScreen", actualScreen.Object);
            _screenNavigatorViewModel.Object.BackToPreviousScreen.Execute();

            actualScreen.Verify(x => x.SetActive(It.IsAny<bool>()), Times.Never);
        }
    }
}