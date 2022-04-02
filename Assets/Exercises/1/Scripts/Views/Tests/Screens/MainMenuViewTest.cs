using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Utilities.Helpers;
using UnityExercises.Views.Screens;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class MainMenuViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuView _mainMenuView;

        private MainMenuViewModel _mainMenuViewModel;
        private ShopViewModel _shopViewModel;
        private InventoryViewModel _inventoryViewModel;
        private Button _shopButton;
        private Button _inventoryButton;

        [SetUp]
        public void SetUp()
        {
            _mainMenuViewModel = new MainMenuViewModel();
            _shopViewModel = new ShopViewModel();
            _inventoryViewModel = new InventoryViewModel();
            Container.Bind<MainMenuView>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .WithArguments(_mainMenuViewModel, _shopViewModel, _inventoryViewModel);

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
            _shopViewModel.OnGoToButtonPressed.Subscribe(observer.Object);
            _shopButton.onClick.Invoke();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }

        [Test]
        public void WhenClickOnInventoryButton_ExecuteOnGoToButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _inventoryViewModel.OnGoToButtonPressed.Subscribe(observer.Object);
            _inventoryButton.onClick.Invoke();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void WhenUpdateVisibilityOnTheViewModel_ShowOrHideTheGameObject(bool expectedValue)
        {
            _mainMenuView.gameObject.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _mainMenuView.gameObject.activeSelf);
            _mainMenuViewModel.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _mainMenuView.gameObject.activeSelf);
        }
    }
}