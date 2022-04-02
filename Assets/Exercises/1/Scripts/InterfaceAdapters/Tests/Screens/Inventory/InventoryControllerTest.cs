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
        [Inject] private readonly InventoryViewModel _inventoryViewModel;
        [Inject] private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<IInventory>().FromMock().AsSingle();
            Container.Bind<InventoryViewModel>().AsSingle();
            Container.Bind<ScreenNavigatorViewModel>().AsSingle();
            Container.Bind<InventoryController>().AsSingle();
            Container.Inject(this);
        }

        [Test]
        public void WhenReceiveCommandOnGoToButtonPressed_CallToSetActualScreen()
        {
            var observer = new Mock<IObserver<IActivable>>();
            _screenNavigatorViewModel.SetActualScreen.Subscribe(observer.Object);
            _inventoryViewModel.OnGoToButtonPressed.Execute();

            observer.Verify(x => x.OnNext(It.IsAny<IActivable>()), Times.Once);
        }

        [Test]
        public void WhenReceiveCommandOnBackButtonPressed_CallToBackToPreviousScreen()
        {
            var observer = new Mock<IObserver<Unit>>();
            _screenNavigatorViewModel.BackToPreviousScreen.Subscribe(observer.Object);
            _inventoryViewModel.OnBackButtonPressed.Execute();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }
    }
}