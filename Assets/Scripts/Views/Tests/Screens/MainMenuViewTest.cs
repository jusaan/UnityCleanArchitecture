using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Inventory;
using UnityCleanArchitecture.InterfaceAdapters.Screens.MainMenu;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Shop;
using UnityCleanArchitecture.Utilities.Helpers;
using UnityCleanArchitecture.Views.Screens;
using Zenject;

namespace UnityCleanArchitecture.Views.Tests.Screens
{
    [TestFixture]
    public class MainMenuViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuView _mainMenuView;

        private Mock<MainMenuViewModel> _mainMenuViewModel;
        private Mock<ShopViewModel> _shopViewModel;
        private Mock<InventoryViewModel> _inventoryViewModel;
        private Button _shopButton;
        private Button _inventoryButton;

        [SetUp]
        public void SetUp()
        {
            _mainMenuViewModel = new Mock<MainMenuViewModel>();
            _shopViewModel = new Mock<ShopViewModel>();
            _inventoryViewModel = new Mock<InventoryViewModel>();
            Container.Bind<MainMenuView>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .WithArguments(_mainMenuViewModel.Object, _shopViewModel.Object, _inventoryViewModel.Object);

            Container.Inject(this);

            _shopButton = new GameObject().AddComponent<Button>();
            _inventoryButton = new GameObject().AddComponent<Button>();
            ReflectionHelper.SetInstanceField(_mainMenuView, "_shopButton", _shopButton);
            ReflectionHelper.SetInstanceField(_mainMenuView, "_inventoryButton", _inventoryButton);

            ReflectionHelper.InvokeInstanceMethod(_mainMenuView, "Awake");
            ReflectionHelper.InvokeInstanceMethod(_mainMenuView, "Start");
        }

        [Test]
        public void WhenClickOnShopButton_ExecuteOnGoToButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _shopViewModel.Object.OnGoToButtonPressed.Subscribe(observer.Object);
            _shopButton.onClick.Invoke();

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
            _mainMenuView.gameObject.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _mainMenuView.gameObject.activeSelf);
            _mainMenuViewModel.Object.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _mainMenuView.gameObject.activeSelf);
        }
    }
}