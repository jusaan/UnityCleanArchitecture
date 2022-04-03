using UnityCleanArchitecture.InterfaceAdapters.Screens;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Inventory;
using UnityCleanArchitecture.InterfaceAdapters.Screens.MainMenu;
using UnityCleanArchitecture.InterfaceAdapters.Screens.Shop;
using UnityCleanArchitecture.UseCases.Screens;
using UnityCleanArchitecture.Utilities.Events;
using Zenject;

namespace UnityCleanArchitecture.Configuration.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventDispatcherService>()
                .To<EventDispatcherService>()
                .AsSingle();

            Container.Bind<ScreenNavigatorViewModel>()
                .AsSingle();

            InjectMainMenu();
            InjectShop();
            InjectInventory();
        }

        private void InjectMainMenu()
        {
            Container.Bind<MainMenuViewModel>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuUseCase>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuController>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<MainMenuPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void InjectShop()
        {
            Container.Bind<ShopViewModel>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ShopUseCase>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ShopController>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ShopPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void InjectInventory()
        {
            Container.Bind<InventoryViewModel>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryUseCase>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<InventoryPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
