using UniRx;
using UnityCleanArchitecture.Utilities.Interactables;

namespace UnityCleanArchitecture.InterfaceAdapters.Screens
{
    public class ScreenNavigatorViewModel
    {
        public ReactiveCommand BackToPreviousScreen { get; } = new ReactiveCommand();
        public ReactiveCommand<IActivable> SetActualScreen { get; } = new ReactiveCommand<IActivable>();
    }
}