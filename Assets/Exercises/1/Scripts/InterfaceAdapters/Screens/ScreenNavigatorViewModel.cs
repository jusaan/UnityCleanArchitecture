using UniRx;
using UnityExercises.Utilities.Interactables;

namespace UnityExercises.InterfaceAdapters.Screens
{
    public class ScreenNavigatorViewModel
    {
        public ReactiveCommand BackToPreviousScreen { get; } = new ReactiveCommand();
        public ReactiveCommand<IActivable> SetActualScreen { get; } = new ReactiveCommand<IActivable>();
    }
}