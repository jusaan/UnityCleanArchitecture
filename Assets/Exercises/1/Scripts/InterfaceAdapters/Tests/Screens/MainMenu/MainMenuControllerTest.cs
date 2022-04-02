using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.Utilities.Interactables;
using Zenject;

namespace UnityExercises.InterfaceAdapters.Tests.Screens.MainMenu
{
    [TestFixture]
    public class MainMenuControllerTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuController _mainMenuController;
        [Inject] private readonly MainMenuViewModel _mainMenuViewModel;
        [Inject] private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<IMainMenu>().FromMock().AsSingle();
            Container.Bind<MainMenuViewModel>().AsSingle();
            Container.Bind<ScreenNavigatorViewModel>().AsSingle();
            Container.Bind<MainMenuController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.SetActualScreen.Subscribe(observer.Object);
            _mainMenuViewModel.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.BackToPreviousScreen.Subscribe(observer.Object);
            _mainMenuViewModel.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}