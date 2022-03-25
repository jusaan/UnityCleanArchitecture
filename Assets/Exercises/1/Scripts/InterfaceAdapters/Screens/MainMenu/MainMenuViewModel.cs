using UniRx;

namespace UnityExercises.InterfaceAdapters.Screens.MainMenu
{
    public class MainMenuViewModel
    {
        public ReactiveCommand OnGoToButtonPressed { get; } = new ReactiveCommand();
        public ReactiveCommand OnBackButtonPressed { get; } = new ReactiveCommand();
        public ReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();
    }
}