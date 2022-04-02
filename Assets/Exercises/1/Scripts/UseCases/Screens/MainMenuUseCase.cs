using System;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.Utilities.Events;

namespace UnityExercises.UseCases.Screens
{
    public class MainMenuUseCase : IMainMenu
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public MainMenuUseCase(IEventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
        }

        public void SetActive(bool active)
        {
            _eventDispatcherService.Dispatch(new MainMenuVisibility(active));
        }
    }
}