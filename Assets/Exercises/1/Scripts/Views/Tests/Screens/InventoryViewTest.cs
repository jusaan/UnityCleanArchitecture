using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.Views.Screens;
using UnityExercises.Views.Tests.Utilities;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class InventoryViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        private GameObject _inventory;
        private InventoryView _inventoryView;
        private Button _backButton;

        [SetUp]
        public void SetUp()
        {
            Container.Bind<InventoryViewModel>().AsSingle();
            Container.Inject(this);

            _inventory = new GameObject();
            _inventoryView = _inventory.AddComponent<InventoryView>();
            _backButton = new GameObject().AddComponent<Button>();

            ReflectionHelper.SetInstanceField(_inventoryView, "_inventoryViewModel", _inventoryViewModel);

            ReflectionHelper.SetInstanceField(_inventoryView, "_backButton", _backButton);

            ReflectionHelper.InvokeInstanceMethod(_inventoryView, "Awake");
        }

        [Test]
        public void WhenClickOnBackButton_ExecuteOnBackButtonPressedCommand()
        {
            var observer = new Mock<IObserver<Unit>>();
            _inventoryViewModel.OnBackButtonPressed.Subscribe(observer.Object);
            _backButton.onClick.Invoke();

            observer.Verify(x => x.OnNext(Unit.Default), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void WhenUpdateVisibilityOnTheViewModel_ShowOrHideTheGameObject(bool expectedValue)
        {
            _inventory.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _inventory.activeSelf);
            _inventoryViewModel.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _inventory.activeSelf);
        }
    }
}