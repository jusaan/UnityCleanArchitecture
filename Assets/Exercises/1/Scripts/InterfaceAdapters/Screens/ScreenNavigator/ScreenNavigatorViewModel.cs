using UniRx;
using UnityExercises.Entities.Utilities;

namespace UnityExercises.InterfaceAdapters.Screens.ScreenNavigator
{
    public class ScreenNavigatorViewModel
    {
        public ReactiveCommand BackToPreviousScreen { get; } = new ReactiveCommand();
        public ReactiveCommand<IActivable> SetActualScreen { get; } = new ReactiveCommand<IActivable>();
    }
}