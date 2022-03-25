using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.Shop;
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
            _shopViewModel.OnBackButtonPressed.BindTo(_backButton);
            _inventoryViewModel.OnGoToButtonPressed.BindTo(_inventoryButton);
            _shopViewModel.IsVisible.Subscribe(gameObject.SetActive);
        }
    }
}