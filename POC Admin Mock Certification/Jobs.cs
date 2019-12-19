using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using Stylelabs.M.Framework.Utilities;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Models.Jobs;
using Stylelabs.M.Sdk.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certification.Demo
{
    class Jobs
    {
        public static async Task<long> CreateFetchJob(long entityId, Uri resourceUrl, IWebMClient client)
        {
            // Validation
            Guard.GreaterThan("entityId", entityId, 0);
            Guard.NotNull("resourceUrl", resourceUrl);

            // Create the fetch job request
            var fjr = new WebFetchJobRequest("File", entityId);
            fjr.Urls.Add(resourceUrl);

            // Create the fetch job
            long fetchJobId = await client.Jobs.CreateFetchJobAsync(fjr).ConfigureAwait(false);
            return fetchJobId;
        }

        public static async Task<bool> IsJobCompleted(long id, IWebMClient client)
        {
            // Query for the job by id
            var job = await client.Entities.GetAsync(id, EntityLoadConfiguration.Full);

            // Get the state of the job
            var stateProperty = job.GetProperty<ICultureInsensitiveProperty>(Constants.Job.Properties.State);

            // Check if the job was completed
            return (stateProperty.GetValue<string>() == Constants.Job.States.Completed);
        }
    }
}
