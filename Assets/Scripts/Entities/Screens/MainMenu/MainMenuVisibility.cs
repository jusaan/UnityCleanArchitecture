namespace UnityCleanArchitecture.Entities.Screens.MainMenu
{
    public class MainMenuVisibility
    {
        public bool IsVisible { get; }

        public MainMenuVisibility(bool isVisible)
        {
            IsVisible = isVisible;
        }
    }
}