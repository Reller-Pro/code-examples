/// <summary>
/// Delegate factories via Autofac module.
/// This module is used by Autofac to register the IIntegration interface and its implementations.
/// </summary>
public class RcCrmLogicAutofacModule : Module
{
    // Override the Load method to configure dependencies
    protected override void Load(ContainerBuilder builder)
    {
        // Register the IIntegration interface
        builder.Register<IIntegration>((c, p) =>
        {
            // Get the integration type parameter
            var type = p.TypedAs<IntegrationType>();

            // Decide which integration service to resolve based on the type
            switch (type)
            {
                case IntegrationType.Yandex:
                    // Resolve YandexIntegrationService with its dependencies
                    return new YandexIntegrationService(
                        c.Resolve<RcCrmDbContext>(),
                        c.Resolve<IMapper>(),
                        c.Resolve<ILogger<YandexIntegrationService>>(),
                        c.Resolve<IConfigurationRoot>(),
                        c.Resolve<IConnectionConfigProvider>(),
                        c.Resolve<IOfferIntegrationService>());

                case IntegrationType.Cian:
                    // Resolve CianIntegrationService with its dependencies
                    return new CianIntegrationService(
                        c.Resolve<RcCrmDbContext>(),
                        c.Resolve<IMapper>(),
                        c.Resolve<IConfigurationRoot>(),
                        c.Resolve<ILogger<CianIntegrationService>>(),
                        c.Resolve<IOfferIntegrationService>());

                case IntegrationType.Avito:
                    // Resolve AvitoIntegrationService with its dependencies
                    return new AvitoIntegrationService(
                        c.Resolve<RcCrmDbContext>(),
                        c.Resolve<IMapper>(),
                        c.Resolve<IConfigurationRoot>(),
                        c.Resolve<ILogger<AvitoIntegrationService>>(),
                        c.Resolve<IOfferIntegrationService>());

                case IntegrationType.Afy:
                    // Resolve AfyIntegrationService with its dependencies
                    return new AfyIntegrationService(c.Resolve<IMapper>());
                case IntegrationType.DomClick:
                    // Resolve DomClickIntegrationService with its dependencies
                    return new DomClickIntegrationService(c.Resolve<IMapper>());
                case IntegrationType.Variant:
                    // Resolve VariantIntegrationService with its dependencies
                    return new VariantIntegrationService(c.Resolve<IMapper>());
                case IntegrationType.Youla:
                    // Resolve YoulaIntegrationService with its dependencies
                    return new YoulaIntegrationService(c.Resolve<IMapper>());
                case IntegrationType.N1:
                    // Resolve N1IntegrationService with its dependencies
                    return new N1IntegrationService(c.Resolve<IMapper>());
                case IntegrationType.Sibdom:
                    // Resolve SibdomIntegrationService with its dependencies
                    return new SibdomIntegrationService(c.Resolve<IMapper>());

                default:
                    // If an unsupported type is provided, throw an error
                    throw new ArgumentException("Invalid connection type");
            }
        }).As<IIntegration>(); // Register the resolved service to the IIntegration interface
    }
}