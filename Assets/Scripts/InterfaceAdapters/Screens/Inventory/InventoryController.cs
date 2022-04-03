using UniRx;
using UnityCleanArchitecture.Entities.Screens.Inventory;
using UnityCleanArchitecture.Utilities.Helpers;

namespace UnityCleanArchitecture.InterfaceAdapters.Screens.Inventory
{
    public class InventoryController : DisposableBase
    {
        private readonly IInventory _inventory;
        private readonly InventoryViewModel _inventoryViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public InventoryController(IInventory inventory,
                            InventoryViewModel inventoryViewModel,
                            ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _inventory = inventory;
            _inventoryViewModel = inventoryViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _inventoryViewModel.OnGoToButtonPressed.Subscribe(SetActualScreen).AddTo(_disposables);
            _inventoryViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_inventory);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}