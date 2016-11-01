using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using Glados.Core.Interfaces;
using Glados.Droid.Services;
using Glados.Droid.Database;
using Glados.Core.Database;

namespace Glados.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Glados.Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
        protected override void InitializeFirstChance()
        {
            Mvx.LazyConstructAndRegisterSingleton<IToast, ToastService>();
            Mvx.LazyConstructAndRegisterSingleton<ISqlite, SqliteDroid>();
            Mvx.LazyConstructAndRegisterSingleton<IAzureDatabase, AzureDatabase>();
            //Mvx.LazyConstructAndRegisterSingleton<IUserStoreDatabase, UserStoreDBAzure>();
            //Mvx.LazyConstructAndRegisterSingleton<IMessageStoreDatabase, MessageRequestStoreDBAzure>();
            //Mvx.LazyConstructAndRegisterSingleton<IUserFavouritesStoreDatabase, UserFavouritesStoreDBAzure>();
            //Mvx.LazyConstructAndRegisterSingleton<IMessageResponseStoreDatabase, MessageResponseStoreDBAzure>();
            Mvx.LazyConstructAndRegisterSingleton<IDatabase, TableStoreDBAzure>();
            //Mvx.LazyConstructAndRegisterSingleton<IUserLogin, UserLoggedInDB>();
            base.InitializeFirstChance();
        }
    }
}
