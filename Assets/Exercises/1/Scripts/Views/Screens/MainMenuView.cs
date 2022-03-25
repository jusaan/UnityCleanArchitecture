using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using Zenject;

namespace UnityExercises.Views.Screens
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
            _mainMenuViewModel.IsVisible.Subscribe(gameObject.SetActive);
        }
    }
}