using UniRx;

namespace UnityCleanArchitecture.InterfaceAdapters.Screens.Inventory
{
    public class InventoryViewModel
    {
        public ReactiveCommand OnGoToButtonPressed { get; } = new ReactiveCommand();
        public ReactiveCommand OnBackButtonPressed { get; } = new ReactiveCommand();
        public ReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();
    }
}