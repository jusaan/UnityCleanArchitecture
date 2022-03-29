using System;
using UnityExercises.Entities.Screens.Shop;
using UnityExercises.Entities.Services.EventDispatcher;

namespace UnityExercises.InterfaceAdapters.Screens.Shop
{
    public class ShopPresenter : IDisposable
    {
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly ShopViewModel _shopViewModel;

        public ShopPresenter(IEventDispatcherService eventDispatcherService,
                            ShopViewModel shopViewModel)
        {
            _eventDispatcherService = eventDispatcherService;
            _shopViewModel = shopViewModel;

            _eventDispatcherService.Subscribe<ShopVisibility>(OnShopVisibilityChange);
        }

        public void Dispose()
        {
            _eventDispatcherService.Unsubscribe<ShopVisibility>(OnShopVisibilityChange);
        }

        private void OnShopVisibilityChange(ShopVisibility shopVisibility)
        {
            _shopViewModel.IsVisible.Value = shopVisibility.IsVisible;
        }
    }
}