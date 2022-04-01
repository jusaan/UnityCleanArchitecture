using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using Zenject;

namespace UnityExercises.Views.Screens
{
    public class InventoryView : ViewBase
    {
        [Inject] private readonly InventoryViewModel _inventoryViewModel;

        [SerializeField] private Button _backButton;

        private void Awake()
        {
            _inventoryViewModel.OnBackButtonPressed.BindTo(_backButton);
            _inventoryViewModel.IsVisible.Subscribe(gameObject.SetActive);
        }
    }
}