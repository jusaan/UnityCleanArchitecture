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
        [Inject] private readonly ShopViewModel _shopViewModel;
        [Inject] private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<IShop>().FromMock().AsSingle();
            Container.Bind<ShopViewModel>().AsSingle();
            Container.Bind<ScreenNavigatorViewModel>().AsSingle();
            Container.Bind<ShopController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.SetActualScreen.Subscribe(observer.Object);
            _shopViewModel.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.BackToPreviousScreen.Subscribe(observer.Object);
            _shopViewModel.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}