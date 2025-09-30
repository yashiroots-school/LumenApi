using LumenApi.Web.ContributorEndpoints;

namespace LumenApi.Web.Endpoints.ContributorEndpoints;

public class ContributorListResponse
{
  public List<ContributorRecord> Contributors { get; set; } = new();
}
