using System.Text;
using System.Threading.Tasks;
using RdpRealTimePricing.Model.Data;
using Endpoint = LSEG.Data.Delivery.Request.EndpointRequest;

namespace RdpPriceReportApp.ViewModel
{
    public class RdpCompanyInfo
    {
        private string baseEndpoint = "https://api.refinitiv.com/user-framework/mobile/overview-service/v1/";

        public async Task<CompanyName> GetCompanyNameAsync(LSEG.Data.Core.ISession session, string ricname)
        {
            var companyName = new CompanyName();
            var endpoint = new StringBuilder();
            endpoint.Append(baseEndpoint);
            endpoint.Append($"corp/company-name/{ricname}");
            var response = await Endpoint.Definition(endpoint.ToString()).GetDataAsync().ConfigureAwait(true);
            if (response.IsSuccess)
            {
                companyName = response.Data?.Raw?["data"]["companyName"].ToObject<CompanyName>();
            }

            return companyName;
        }

        public async Task<CompanyBusinessSummary> GetCompanyBusinessSummaryAsync(
            LSEG.Data.Core.ISession session, string ricname)
        {
            var companyBusinessSummary = new CompanyBusinessSummary();
            var endpoint = new StringBuilder();
            endpoint.Append(baseEndpoint);
            endpoint.Append($"corp/business-summary/{ricname}");
            var response = await Endpoint.Definition(endpoint.ToString()).GetDataAsync().ConfigureAwait(true);
            if (response.IsSuccess)
            {
                companyBusinessSummary =
                    response.Data?.Raw?["data"]["businessSummary"].ToObject<CompanyBusinessSummary>();
            }

            return companyBusinessSummary;
        }
    }
}
