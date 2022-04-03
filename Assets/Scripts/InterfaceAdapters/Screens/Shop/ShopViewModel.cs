using UniRx;

namespace UnityCleanArchitecture.InterfaceAdapters.Screens.Shop
{
    public class ShopViewModel
    {
        public ReactiveCommand OnGoToButtonPressed { get; } = new ReactiveCommand();
        public ReactiveCommand OnBackButtonPressed { get; } = new ReactiveCommand();
        public ReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();
    }
}