/// <summary>
/// Begin implementation of the offer integration service
/// </summary>
public class OfferIntegrationService : IOfferIntegrationService
{
    private readonly Func<IntegrationType, IIntegration> _integrationFactory;

    // Inject the integration factory function to the constructor
    public OfferIntegrationService(Func<IntegrationType, IIntegration> integrationFactory)
    {
        _integrationFactory = integrationFactory;
    }

    //Define the method to refresh integration reports
    public async Task RefreshIntegrationReports(IntegrationType integrationType)
    {
        // Call the RefreshStateOffers method of the resolved integration service
        await _integrationFactory(integrationType).RefreshStateOffers();
    }
}
