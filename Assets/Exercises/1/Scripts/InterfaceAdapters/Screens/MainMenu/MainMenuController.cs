using UniRx;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.Utilities.Helpers;

namespace UnityExercises.InterfaceAdapters.Screens.MainMenu
{
    public class MainMenuController : DisposableBase
    {
        private readonly IMainMenu _mainMenu;
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;

        public MainMenuController(IMainMenu mainMenu,
                                MainMenuViewModel mainMenuViewModel,
                                ScreenNavigatorViewModel screenNavigatorViewModel)
        {
            _mainMenu = mainMenu;
            _mainMenuViewModel = mainMenuViewModel;
            _screenNavigatorViewModel = screenNavigatorViewModel;

            _mainMenuViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
            _mainMenuViewModel.OnBackButtonPressed.Subscribe(BackToPreviousScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _screenNavigatorViewModel.SetActualScreen.Execute(_mainMenu);
        }

        private void BackToPreviousScreen(Unit _)
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Execute();
        }
    }
}