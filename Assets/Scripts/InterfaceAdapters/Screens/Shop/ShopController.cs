using UniRx;
using UnityCleanArchitecture.Entities.Screens.Shop;
using UnityCleanArchitecture.Utilities.Helpers;

namespace UnityCleanArchitecture.InterfaceAdapters.Screens.Shop
{
    public class ShopController : DisposableBase
    {
        private readonly IShop _shop;
        private readonly ShopViewModel _shopViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public ShopController(IShop shop, 
                            ShopViewModel shopViewModel,
                            ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _shop = shop;
            _shopViewModel = shopViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _shopViewModel.OnGoToButtonPressed.Subscribe(SetActualScreen).AddTo(_disposables);
            _shopViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_shop);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}