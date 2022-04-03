using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Inventory;
using UnityCleanArchitecture.InterfaceAdapters.Screens.MainMenu;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Shop;
using UnityCleanArchitecture.Utilities.Helpers;
using Zenject;

namespace UnityCleanArchitecture.Views.Screens
{
    public class MainMenuView : ViewBase
    {
        [Inject] private readonly MainMenuViewModel _mainMenuViewModel;
        [Inject] private readonly ShopViewModel _shopViewModel;
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _inventoryButton;

        private void Awake()
        {
            _shopViewModel.OnGoToButtonPressed.BindTo(_shopButton);
            _inventoryViewModel.OnGoToButtonPressed.BindTo(_inventoryButton);
            _mainMenuViewModel.IsVisible.Value = true;
            _mainMenuViewModel.IsVisible.Subscribe(gameObject.SetActive);
        }

        private void Start()
        {
            _mainMenuViewModel.OnGoToButtonPressed.Execute();
        }
    }
}