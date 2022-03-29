using UniRx;
using UnityExercises.Entities.UseCases.Screens;
using UnityExercises.Entities.Utilities;
using UnityExercises.InterfaceAdapters.Screens.ScreenNavigator;

namespace UnityExercises.InterfaceAdapters.Screens.Shop
{
    public class ShopController : DisposableBase
    {
        private readonly IShop _shopUseCase;
        private readonly ShopViewModel _shopViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public ShopController(IShop shopUseCase, 
                            ShopViewModel shopViewModel,
                            ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _shopUseCase = shopUseCase;
            _shopViewModel = shopViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _shopViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _shopViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_shopUseCase);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}