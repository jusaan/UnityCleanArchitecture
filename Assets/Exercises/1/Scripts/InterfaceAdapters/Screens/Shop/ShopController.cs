using UniRx;
using UnityExercises.Entities.UseCases.Screens;
using UnityExercises.Entities.Utilities;

namespace UnityExercises.InterfaceAdapters.Screens.Shop
{
    public class ShopController : DisposableBase
    {
        private readonly IShopUseCase _shopUseCase;
        private readonly ShopViewModel _shopViewModel;

        public ShopController(IShopUseCase shopUseCase, 
                            ShopViewModel shopViewModel)
        {
            _shopUseCase = shopUseCase;
            _shopViewModel = shopViewModel;

            _shopViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _shopViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _shopUseCase.SetAsActualScreen();
        }

        private void BackToPreviousScreen(Unit _)
        {
            _shopUseCase.BackToPreviousScreen();
        }
    }
}