using System;
using UnityExercises.Entities.Services.EventDispatcher;
using UnityExercises.Entities.UseCases.Screens;

namespace UnityExercises.UseCases.Screens
{
    public class MainMenuUseCase : IMainMenuUseCase
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public MainMenuUseCase(IEventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
        }

        public void SetAsActualScreen()
        {
            throw new NotImplementedException();
        }
    }
}