using System;
using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.Entities.Services.EventDispatcher;

namespace UnityExercises.InterfaceAdapters.Screens.Inventory
{
    public class InventoryPresenter : IDisposable
    {
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly InventoryViewModel _inventoryViewModel;

        public InventoryPresenter(IEventDispatcherService eventDispatcherService,
                            InventoryViewModel inventoryViewModel)
        {
            _eventDispatcherService = eventDispatcherService;
            _inventoryViewModel = inventoryViewModel;

            _eventDispatcherService.Subscribe<InventoryVisibility>(OnInventoryVisibilityChange);
        }

        public void Dispose()
        {
            _eventDispatcherService.Unsubscribe<InventoryVisibility>(OnInventoryVisibilityChange);
        }

        private void OnInventoryVisibilityChange(InventoryVisibility inventoryVisibility)
        {
            _inventoryViewModel.IsVisible.Value = inventoryVisibility.IsVisible;
        }       
    }
}