using Mock.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stylelabs.M.Base.Querying;
using Stylelabs.M.Base.Querying.Filters;
using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using Stylelabs.M.Framework.Essentials.LoadOptions;
using Stylelabs.M.Sdk;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Models.Base;
using Stylelabs.M.Sdk.Models.Base.PropertyDefinitions;
using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certification.Demo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // await RunPoC(await Connect()).ContinueWith(x => { Console.ReadKey(); } );
            var client = await Connect();
            // read JSON directly from a file
            var snitch = GetAssetValidationConfiguration(client, @"..\..\..\json\validation.json");
            await Admin.ValidateAssetDeifinition(client, police: snitch);
            Console.ReadKey();
        }

        public static AssetValidationConfiguration GetAssetValidationConfiguration(IWebMClient client, string file_location)
        {
            try
            {
                StreamReader file = File.OpenText(file_location);
                JsonTextReader reader = new JsonTextReader(file);
                AssetValidationConfiguration validation = JToken.ReadFrom(reader).ToObject<AssetValidationConfiguration>();
                return validation;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw e;
            }
        }

        private static async Task RunPoC(IWebMClient client)
        {
            Task w = await PrepareExam(client).ContinueWith(async x =>
            {
                await StartExam(client).ContinueWith(async y =>
                {
                    await ValidateExam(client).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }).ConfigureAwait(false);

        }

        private static async Task PrepareExam(IWebMClient client)
        {
            Console.WriteLine("Preparing environment ...");
            // Set up the preconditions
            Exam.Exercises.ForEach(async question =>
            {
                if (question.HowToPrepare != null)
                {
                    await question.SetPrecondintions(client);
                }
            });
            Console.WriteLine("Done. Moving on ...");
            Log("\nFinished.");
        }

        private static async Task StartExam(IWebMClient client)
        {
            Log("Press any key to start the test.");
            Console.ReadKey();
            // Present the question / exercise one by one
            Exam.Exercises.ForEach(question =>
            {
                Console.Clear();
                Log(question.Prompt);
                Console.Write("Write 'next' to proceed: ");
                string result = Console.ReadLine();
                while (!result.ToUpper().Equals("NEXT"))
                {
                    Console.Write("To prevent accidental skips, write 'next' to proceed: ");
                    result = Console.ReadLine();
                    if (result.ToUpper().Equals("NEXT"))
                    {
                        break;
                    }
                }
            });
        }

        private static async Task ValidateExam(IWebMClient client)
        {
            // Validate and calculate score
            int score = 0, index = 1;
            Console.Clear();
            Log("\nEnd of test. Validating . . .");
            Exam.Exercises.ForEach(async question =>
            {
                await question.ValidateAsync(client).ContinueWith(x =>
                {
                    score += x.Result;
                    if (index == Exam.Exercises.Count())
                    {
                        Console.WriteLine("\nYour score: {0}", score);

                        Log("\nPress any key to continue ...");
                        Console.ReadKey();
                    }
                    return x.Result;
                });
                index++;
            });
        }

        private static async Task<IWebMClient> Connect()
        {
            Uri endpoint = new Uri(Constants.AppSettings.MOriginAddress);

            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = Constants.AppSettings.MClientId,
                ClientSecret = Constants.AppSettings.MClientSecret,
                UserName = Constants.AppSettings.MUsername,
                Password = Constants.AppSettings.MPassword
            };

            Console.WriteLine("Connecting to Sitecore Content Hub...");
            IWebMClient client = MClientFactory.CreateMClient(endpoint, oauth);
            try
            {
                await client.TestConnectionAsync();
                Console.WriteLine("Connection established. Moving on...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return client;
        }

        public static void Log(string x) => Console.WriteLine(x);

        public static async Task ValidateQuestion3Async(IWebMClient client)
        {
            IMClient impersonatedUser = await client.ImpersonateAsync("Creator");
            var assetDefinition = await impersonatedUser.EntityDefinitions.GetAsync(Constants.Asset.DefinitionName);
            var asset = await impersonatedUser.EntityFactory.CreateAsync(Constants.Asset.DefinitionName, CultureLoadOption.Default);
            asset.SetPropertyValue("Title", "CustomAsset");
            var assetId = await impersonatedUser.Entities.SaveAsync(asset);
            asset = await impersonatedUser.Entities.GetAsync(assetId);
            await Jobs.CreateFetchJob(assetId, new Uri("https://picsum.photos/1/1"), impersonatedUser as IWebMClient).ContinueWith(async x =>
            {
                var fetchJobId = x.Result;
                await Jobs.IsJobCompleted(fetchJobId, impersonatedUser as IWebMClient).ContinueWith(y =>
                {
                    //var description = asset.GetPropertyValue<string>(Constants.Asset.Properties.Description);
                    ICultureSensitiveProperty descriptionProp = asset.GetProperty<ICultureSensitiveProperty>("Description");
                    string description = descriptionProp.GetValue<string>(Constants.DefaultCulture);
                    var today = DateTime.Parse(description.Substring(26, 10));
                    bool success = description.ToLower().StartsWith("This asset was created on ".ToLower())
                    && DateTime.Parse(description.Substring(26, 10)) == DateTime.Today;
                    return y.Result;
                });
                return x.Result;
            });
        }
    }

    
}
