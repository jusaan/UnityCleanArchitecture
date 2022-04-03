using UnityCleanArchitecture.Entities.Screens.Inventory;
using UnityCleanArchitecture.Utilities.Events;

namespace UnityCleanArchitecture.UseCases.Screens
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