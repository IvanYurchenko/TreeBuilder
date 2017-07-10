using Ninject.Modules;

namespace TreeBuilder
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITreeBuilder>().To<TreeBuilder>().InSingletonScope();
            Bind<INodeHelper>().To<NodeHelper>().InSingletonScope();
        }
    }
}
