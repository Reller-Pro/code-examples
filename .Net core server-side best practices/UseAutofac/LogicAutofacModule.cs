/// <summary>
/// Autofac Module for Logic related components
/// </summary>
public class LogicAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register types and interfaces for various search services
        var lifeTimeScopeRegistrations =
            new List<IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>>
            {
                builder.RegisterType<SaleSearchService>().As<ISaleSearchService>(),
                builder.RegisterType<UserSearchService>().As<IUserSearchService>(),
                builder.RegisterType<LessorSearchService>().As<ILessorSearchService>(),
                builder.RegisterType<ContentSearchService>().As<IContentSearchService>(),
                builder.RegisterType<CustomerSearch>().As<ICustomerSearch>(),
                builder.RegisterType<PrivateAdsSearchService>().As<IPrivateAdsSearchService>(),
                builder.RegisterType<PrivateAdsSaleSearchService>().As<IPrivateAdsSaleSearchService>(),
                builder.RegisterType<PrivateAdsLessorSearchService>().As<IPrivateAdsLessorSearchService>()
            };

        // Set instances to be resolved with a lifetime scope
        lifeTimeScopeRegistrations.ForEach(x => x.InstancePerLifetimeScope());
    }
}