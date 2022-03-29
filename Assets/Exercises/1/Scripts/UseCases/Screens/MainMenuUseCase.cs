using System;
using UnityExercises.Entities.Screens.MainMenu;
using UnityExercises.Entities.Services.EventDispatcher;
using UnityExercises.Entities.UseCases.Screens;

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