using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityExercises.Entities.Screens.Shop;
using UnityExercises.InterfaceAdapters.Screens;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Utilities.Interactables;
using Zenject;

namespace UnityExercises.InterfaceAdapters.Tests.Screens.Shop
{
    [TestFixture]
    public class ShopControllerTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ShopController _shopController;

        private Mock<ShopViewModel> _shopViewModel;
        private Mock<ScreenNavigatorViewModel> _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            _shopViewModel = new Mock<ShopViewModel>();
            _screenNavigatorViewModel = new Mock<ScreenNavigatorViewModel>();
            Container.Bind<ShopViewModel>().FromInstance(_shopViewModel.Object);
            Container.Bind<ScreenNavigatorViewModel>().FromInstance(_screenNavigatorViewModel.Object);
            Container.Bind<IShop>().FromMock();
            Container.Bind<ShopController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.Object.SetActualScreen.Subscribe(observer.Object);
            _shopViewModel.Object.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.Object.BackToPreviousScreen.Subscribe(observer.Object);
            _shopViewModel.Object.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}