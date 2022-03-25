using UnityExercises.Entities.UseCases.Screens;
using UnityExercises.Entities.Utilities;
using UniRx;

namespace UnityExercises.InterfaceAdapters.Screens.MainMenu
{
    public class MainMenuController : DisposableBase
    {
        private readonly IMainMenuUseCase _mainMenuUseCase;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public MainMenuController(IMainMenuUseCase mainMenuUseCase,
                                MainMenuViewModel mainMenuViewModel)
        {
            _mainMenuUseCase = mainMenuUseCase;
            _mainMenuViewModel = mainMenuViewModel;

            _mainMenuViewModel.OnGoToButtonPressed.Subscribe(SetAsActualScreen).AddTo(_disposables);
        }

        private void SetAsActualScreen(Unit _)
        {
            _mainMenuUseCase.SetAsActualScreen();
        }
    }
}