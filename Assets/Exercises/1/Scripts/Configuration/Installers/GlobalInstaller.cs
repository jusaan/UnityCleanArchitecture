using UnityExercises.Entities.Services.EventDispatcher;
using UnityExercises.InterfaceAdapters.Screens.Inventory;
using UnityExercises.InterfaceAdapters.Screens.MainMenu;
using UnityExercises.InterfaceAdapters.Screens.Shop;
using UnityExercises.UseCases.Screens;
using Zenject;

namespace UnityExercises.Configuration.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventDispatcherService>()
                .To<EventDispatcherService>()
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
