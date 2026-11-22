using Ninject.Modules;
using FitnessApp.Logic.Interfaces;
using FitnessApp.Logic.Models;
using FitnessApp.DataAccessLayer.EF;

namespace FitnessApp.WinForms
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            // Привязываем интерфейс к конкретной реализации Entity Framework
            Bind<IRepository<Training>>()
                .To<EntityRepository<Training>>()
                .InSingletonScope();
        }
    }
}
