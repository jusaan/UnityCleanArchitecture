using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityCleanArchitecture.Entities.Screens.MainMenu;
using UnityCleanArchitecture.InterfaceAdapters.Screens;
using UnityCleanArchitecture.InterfaceAdapters.Screens.MainMenu;
using UnityCleanArchitecture.Utilities.Interactables;
using Zenject;

namespace UnityCleanArchitecture.InterfaceAdapters.Tests.Screens.MainMenu
{
    [TestFixture]
    public class MainMenuControllerTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuController _mainMenuController;

        private Mock<MainMenuViewModel> _mainMenuViewModel;
        private Mock<ScreenNavigatorViewModel> _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            _mainMenuViewModel = new Mock<MainMenuViewModel>();
            _screenNavigatorViewModel = new Mock<ScreenNavigatorViewModel>();   
            Container.Bind<MainMenuViewModel>().FromInstance(_mainMenuViewModel.Object);
            Container.Bind<ScreenNavigatorViewModel>().FromInstance(_screenNavigatorViewModel.Object);
            Container.Bind<IMainMenu>().FromMock();
            Container.Bind<MainMenuController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.Object.SetActualScreen.Subscribe(observer.Object);
            _mainMenuViewModel.Object.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.Object.BackToPreviousScreen.Subscribe(observer.Object);
            _mainMenuViewModel.Object.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}