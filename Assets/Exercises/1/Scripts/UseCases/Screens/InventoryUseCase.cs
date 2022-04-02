using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.Utilities.Events;

namespace UnityExercises.UseCases.Screens
{
    public class InventoryUseCase : IInventory
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public InventoryUseCase(IEventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
        }

        public void SetActive(bool active)
        {
            _eventDispatcherService.Dispatch(new InventoryVisibility(active));
        }
    }
}