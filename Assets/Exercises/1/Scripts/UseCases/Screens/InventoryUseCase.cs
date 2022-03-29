using UnityExercises.Entities.Screens.Inventory;
using UnityExercises.Entities.Services.EventDispatcher;
using UnityExercises.Entities.UseCases.Screens;

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