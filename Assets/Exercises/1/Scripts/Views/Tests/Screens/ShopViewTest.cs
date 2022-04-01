using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Views.Screens;
using UnityExercises.Views.Tests.Utilities;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class ShopViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly ShopViewModel _shopViewModel;
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        private GameObject _shop;
        private ShopView _shopView;
        private Button _backButton;
        private Button _inventoryButton;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<ShopViewModel>().AsSingle();
            Container.Bind<InventoryViewModel>().AsSingle();
            Container.Inject(this);

            _shop = new GameObject();
            _shopView = _shop.AddComponent<ShopView>();
            _backButton = new GameObject().AddComponent<Button>();
            _inventoryButton = new GameObject().AddComponent<Button>();

            ReflectionHelper.SetInstanceField(_shopView, "_shopViewModel", _shopViewModel);
            ReflectionHelper.SetInstanceField(_shopView, "_inventoryViewModel", _inventoryViewModel);

            ReflectionHelper.SetInstanceField(_shopView, "_backButton", _backButton);
            ReflectionHelper.SetInstanceField(_shopView, "_inventoryButton", _inventoryButton);

            ReflectionHelper.InvokeInstanceMethod(_shopView, "Awake");
        }

        [Test]
        public void WhenClickOnBackButton_ExecuteOnBackButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _shopViewModel.OnBackButtonPressed.Subscribe(observer.Object);
            _backButton.onClick.Invoke();

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
            _shop.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _shop.activeSelf);
            _shopViewModel.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _shop.activeSelf);
        }
    }
}