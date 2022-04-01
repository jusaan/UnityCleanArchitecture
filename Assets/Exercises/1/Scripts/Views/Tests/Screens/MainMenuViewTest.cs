using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Views.Screens;
using UnityExercises.Views.Tests.Utilities;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class MainMenuViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly MainMenuViewModel _mainMenuViewModel;
        [Inject] private readonly ShopViewModel _shopViewModel;
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        private GameObject _mainMenu;
        private MainMenuView _mainMenuView;
        private Button _shopButton;
        private Button _inventoryButton;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<MainMenuViewModel>().AsSingle();
            Container.Bind<ShopViewModel>().AsSingle();
            Container.Bind<InventoryViewModel>().AsSingle();
            Container.Inject(this);

            _mainMenu = new GameObject();
            _mainMenuView = _mainMenu.AddComponent<MainMenuView>();
            _shopButton = new GameObject().AddComponent<Button>();
            _inventoryButton = new GameObject().AddComponent<Button>();

            ReflectionHelper.SetInstanceField(_mainMenuView, "_mainMenuViewModel", _mainMenuViewModel);
            ReflectionHelper.SetInstanceField(_mainMenuView, "_shopViewModel", _shopViewModel);
            ReflectionHelper.SetInstanceField(_mainMenuView, "_inventoryViewModel", _inventoryViewModel);

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
            _mainMenu.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _mainMenu.activeSelf);
            _mainMenuViewModel.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _mainMenu.activeSelf);
        }
    }
}