using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.Utilities.Helpers;
using Zenject;

namespace UnityExercises.Views.Screens
{
    public class ShopView : ViewBase
    {
        [Inject] private readonly ShopViewModel _shopViewModel;
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _inventoryButton;

        private void Awake()
        {
            _shopViewModel.OnBackButtonPressed.BindTo(_backButton).AddTo(_disposables);
            _inventoryViewModel.OnGoToButtonPressed.BindTo(_inventoryButton).AddTo(_disposables);
            _shopViewModel.IsVisible.Subscribe(gameObject.SetActive).AddTo(_disposables);
        }
    }
}