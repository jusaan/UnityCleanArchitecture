using UnityExercises.Entities.Screens.Shop;
using UnityExercises.Utilities.Events;

namespace UnityExercises.UseCases.Screens
{
    public class ShopUseCase : IShop
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public ShopUseCase(IEventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
        }

        public void SetActive(bool active)
        {
            _eventDispatcherService.Dispatch(new ShopVisibility(active));
        }
    }
}