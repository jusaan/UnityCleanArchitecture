using UniRx;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.Utilities.Helpers;

namespace UnityExercises.InterfaceAdapters.Screens.MainMenu
{
    public class MainMenuController : DisposableBase
    {
        private readonly IMainMenu _mainMenuUseCase;
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public MainMenuController(IMainMenu mainMenuUseCase,
                                MainMenuViewModel mainMenuViewModel,
                                ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _mainMenuUseCase = mainMenuUseCase;
            _mainMenuViewModel = mainMenuViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _mainMenuViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _mainMenuViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_mainMenuUseCase);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}