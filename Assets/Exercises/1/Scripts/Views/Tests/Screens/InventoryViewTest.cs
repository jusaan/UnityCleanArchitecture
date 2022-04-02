using Moq;
using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.Utilities.Helpers;
using UnityExercises.Views.Screens;
using Zenject;

namespace UnityExercises.Views.Tests.Screens
{
    [TestFixture]
    public class InventoryViewTest : ZenjectUnitTestFixture
    {
        [Inject] private readonly InventoryView _inventoryView;

        private InventoryViewModel _inventoryViewModel;
        private Button _backButton;

        [SetUp]
        public void SetUp()
        {
            _inventoryViewModel = new InventoryViewModel();
            Container.Bind<InventoryView>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .WithArguments(_inventoryViewModel);

            Container.Inject(this);

            _backButton = new GameObject().AddComponent<Button>();
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
            _inventoryView.gameObject.SetActive(!expectedValue);
            Assert.AreEqual(!expectedValue, _inventoryView.gameObject.activeSelf);
            _inventoryViewModel.IsVisible.SetValueAndForceNotify(expectedValue);

            Assert.AreEqual(expectedValue, _inventoryView.gameObject.activeSelf);
        }
    }
}