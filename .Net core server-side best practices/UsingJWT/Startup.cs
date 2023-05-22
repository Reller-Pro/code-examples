/// <summary>
/// This class sets up dependency injection using Autofac Modules, reducing configuration complexity and bundling related components.
/// </summary>
public class Startup
{
    public IConfigurationRoot Configuration { get; }
    public IContainer AppContainer { get; private set; }

    // Constructor takes in an IHostingEnvironment parameter to get the current hosting environment for the app
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder();
        Configuration = builder.Build();
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        return ConfigureAutofac(services);
    }

    // Private method that configures the Autofac container
    private IServiceProvider ConfigureAutofac(IServiceCollection services)
    {
        var builder = new ContainerBuilder();

        // Populate the builder with services from the IServiceCollection
        builder.Populate(services);

        // Register Autofac modules for the app
        builder.RegisterModule<CommonAutofacModule>();
        builder.RegisterModule<LogicAutofacModule>();
        builder.RegisterModule<FileStorageAutofacModule>();

        // Build the Autofac container
        AppContainer = builder.Build();

        // Return an AutofacServiceProvider, which wraps the Autofac container and implements the IServiceProvider interface
        return new AutofacServiceProvider(AppContainer);
    }
}