using MvvmCross.Platform.IoC;
using System.Threading.Tasks;

namespace Glados.Core
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			RegisterAppStart<ViewModels.LoginViewModel>();
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
    }
}
