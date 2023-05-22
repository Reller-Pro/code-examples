/// <summary>
/// Define the interface for the offer integration service
/// </summary>
public interface IOfferIntegrationService
{
    Task RefreshIntegrationReports(IntegrationType integrationType);
}
