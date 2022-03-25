using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using Zenject;

namespace UnityExercises.Views.Screens
{
    public class InventoryView : ViewBase
    {
        [Inject] private readonly InventoryViewModel InventoryViewModel;

        [SerializeField] private Button _backButton;

        private void Awake()
        {
            InventoryViewModel.OnBackButtonPressed.BindTo(_backButton);
            InventoryViewModel.IsVisible.Subscribe(gameObject.SetActive);
        }
    }
}