using System;
using UnityCleanArchitecture.Entities.Screens.MainMenu;
using UnityCleanArchitecture.Utilities.Events;

namespace UnityCleanArchitecture.UseCases.Screens
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