/// <summary>
/// Autofac Module for File Storage related components
/// </summary>
public class FileStorageAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register types and interfaces for file storage and image handling
        builder.RegisterType<FileStorageConfiguration>().As<IFileStorageConfiguration>();
        builder.RegisterType<AzureFileStorage>().As<IFileStorage>();
        builder.RegisterType<ImageHandlerService>().As<IImageHandlerService>();
        builder.RegisterType<ImageResizer>().AsSelf();
    }
}
