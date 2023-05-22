/// <summary>
/// Autofac Module for common/shared components
/// </summary>
public class CommonAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register types and interfaces for Google reCAPTCHA verification
        builder.RegisterType<GoogleCaptchaVerificator>().AsImplementedInterfaces();
    }
}