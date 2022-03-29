using UniRx;
using UnityExercises.Entities.UseCases.Screens;
using UnityExercises.Entities.Utilities;
using UnityExercises.InterfaceAdapters.Screens.ScreenNavigator;

namespace UnityExercises.InterfaceAdapters.Screens.Inventory
{
    public class InventoryController : DisposableBase
    {
        private readonly IInventory _inventoryUseCase;
        private readonly InventoryViewModel _inventoryViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public InventoryController(IInventory inventoryUseCase,
                            InventoryViewModel inventoryViewModel,
                            ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _inventoryUseCase = inventoryUseCase;
            _inventoryViewModel = inventoryViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _inventoryViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _inventoryViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_inventoryUseCase);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}