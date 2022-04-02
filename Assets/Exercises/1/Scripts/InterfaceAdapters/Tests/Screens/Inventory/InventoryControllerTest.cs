using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.Utilities.Interactables;
using Zenject;

namespace UnityExercises.InterfaceAdapters.Tests.Screens.Inventory
{
    [TestFixture]
    public class InventoryControllerTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly InventoryController _inventoryController;

        private Mock<InventoryViewModel> _inventoryViewModel;
        private Mock<ScreenNavigatorViewModel> _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            _inventoryViewModel = new Mock<InventoryViewModel>();
            _screenNavigatorViewModel = new Mock<ScreenNavigatorViewModel>();
            Container.Bind<InventoryViewModel>().FromInstance(_inventoryViewModel.Object);
            Container.Bind<ScreenNavigatorViewModel>().FromInstance(_screenNavigatorViewModel.Object);
            Container.Bind<IInventory>().FromMock();
            Container.Bind<InventoryController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.Object.SetActualScreen.Subscribe(observer.Object);
            _inventoryViewModel.Object.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.Object.BackToPreviousScreen.Subscribe(observer.Object);
            _inventoryViewModel.Object.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}