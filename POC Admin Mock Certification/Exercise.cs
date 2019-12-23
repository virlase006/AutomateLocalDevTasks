using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stylelabs.M.Sdk.WebClient;

namespace Certification.Demo
{

    public delegate Task<int> HowToValidate(IWebMClient client);
    public delegate Task HowToPrepare(IWebMClient client);
    class Exercise
    {
        public string Prompt { get; set; }
        public HowToValidate HowToValidate { get; set; }
        public HowToPrepare HowToPrepare { get; set; }

        public int Score { get; set; }
        public async Task<int> ValidateAsync(IWebMClient client)
        {
            return await HowToValidate(client);
        }

        public async Task SetPrecondintions(IWebMClient client)
        {
            await HowToPrepare(client);
        }

        public Exercise()
        {
            HowToPrepare = null;
        }
    }
}
