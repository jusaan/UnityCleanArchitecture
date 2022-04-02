using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Utilities.Helpers;
using UnityExercises.Views.Screens;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class ShopViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ShopView _shopView;

        private Mock<ShopViewModel> _shopViewModel;
        private Mock<InventoryViewModel> _inventoryViewModel;
        private Button _backButton;
        private Button _inventoryButton;

        [SetUp]
        public void SetUp()
        {
            _shopViewModel = new Mock<ShopViewModel>();
            _inventoryViewModel = new Mock<InventoryViewModel>();
            Container.Bind<ShopView>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .WithArguments(_shopViewModel.Object, _inventoryViewModel.Object);

            Container.Inject(this);

            _backButton = new GameObject().AddComponent<Button>();
            _inventoryButton = new GameObject().AddComponent<Button>();
            ReflectionHelper.SetInstanceField(_shopView, "_backButton", _backButton);
            ReflectionHelper.SetInstanceField(_shopView, "_inventoryButton", _inventoryButton);

            ReflectionHelper.InvokeInstanceMethod(_shopView, "Awake");
        }

        [Test]
        public void WhenClickOnBackButton_ExecuteOnBackButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _shopViewModel.Object.OnBackButtonPressed.Subscribe(observer.Object);
            _backButton.onClick.Invoke();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }

        [Test]
        public void WhenClickOnInventoryButton_ExecuteOnGoToButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _inventoryViewModel.Object.OnGoToButtonPressed.Subscribe(observer.Object);
            _inventoryButton.onClick.Invoke();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void WhenUpdateVisibilityOnTheViewModel_ShowOrHideTheGameObject(bool expectedValue)
        {
            _shopView.gameObject.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _shopView.gameObject.activeSelf);
            _shopViewModel.Object.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _shopView.gameObject.activeSelf);
        }
    }
}