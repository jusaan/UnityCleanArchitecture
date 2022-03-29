using System;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.Entities.Services.EventDispatcher;

namespace UnityExercises.InterfaceAdapters.Screens.MainMenu
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public MainMenuPresenter(IEventDispatcherService eventDispatcherService,
                            MainMenuViewModel mainMenuViewModel)
        {
            _eventDispatcherService = eventDispatcherService;
            _mainMenuViewModel = mainMenuViewModel;

            _eventDispatcherService.Subscribe<MainMenuVisibility>(OnMainMenuVisibilityChange);
        }

        public void Dispose()
        {
            _eventDispatcherService.Unsubscribe<MainMenuVisibility>(OnMainMenuVisibilityChange);
        }

        private void OnMainMenuVisibilityChange(MainMenuVisibility mainMenuVisibility)
        {
            _mainMenuViewModel.IsVisible.Value = mainMenuVisibility.IsVisible;
        }
    }
}