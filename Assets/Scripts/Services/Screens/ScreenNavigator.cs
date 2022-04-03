using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityCleanArchitecture.InterfaceAdapters.Screens;
using UnityCleanArchitecture.Utilities.Interactables;
using Zenject;

namespace UnityCleanArchitecture.Services.Screens
{
    public class ScreenNavigator : MonoBehaviour
    {
        [Inject] private readonly ScreenNavigatorViewModel _screenNavigatorViewModel;
        private readonly Stack<IActivable> _screensHistory = new Stack<IActivable>();

        private IActivable _actualScreen;

        private void Awake()
        {
            _screenNavigatorViewModel.BackToPreviousScreen.Subscribe(BackToPreviousScreen).AddTo(this);
            _screenNavigatorViewModel.SetActualScreen.Subscribe(SetActualScreen).AddTo(this);
        }

        private void SetActualScreen(IActivable screen)
        {
            if (_actualScreen != null)
            {
                _actualScreen.SetActive(false);
                _screensHistory.Push(_actualScreen);
            }

            _actualScreen = screen;
            _actualScreen.SetActive(true);
        }

        private void BackToPreviousScreen(Unit _)
        {
            if (!_screensHistory.Any())
            {
                return;
            }

            _actualScreen.SetActive(false);
            _actualScreen = _screensHistory.Pop();
            _actualScreen.SetActive(true);
        }
    }
}