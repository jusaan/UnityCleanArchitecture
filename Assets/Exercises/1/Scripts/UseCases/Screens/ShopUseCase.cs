using UnityExercises.Entities.Services.EventDispatcher;
using UnityExercises.Entities.UseCases.Screens;

namespace UnityExercises.UseCases.Screens
{
    public class ShopUseCase : IShopUseCase
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public ShopUseCase(IEventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
        }

        public void BackToPreviousScreen()
        {
            throw new System.NotImplementedException();
        }

        public void SetAsActualScreen()
        {
            throw new System.NotImplementedException();
        }
    }
}