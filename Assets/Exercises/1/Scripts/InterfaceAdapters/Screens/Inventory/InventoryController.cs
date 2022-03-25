using UniRx;
using UnityExercises.Entities.UseCases.Screens;
using UnityExercises.Entities.Utilities;

namespace UnityExercises.InterfaceAdapters.Screens.Inventory
{
    public class InventoryController : DisposableBase
    {
        private readonly IInventoryUseCase _inventoryUseCase;
        private readonly InventoryViewModel _inventoryViewModel;

        public InventoryController(IInventoryUseCase inventoryUseCase,
                            InventoryViewModel inventoryViewModel)
        {
            _inventoryUseCase = inventoryUseCase;
            _inventoryViewModel = inventoryViewModel;

            _inventoryViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _inventoryViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _inventoryUseCase.SetAsActualScreen();
        }

        private void BackToPreviousScreen(Unit _)
        {
            _inventoryUseCase.BackToPreviousScreen();
        }
    }
}