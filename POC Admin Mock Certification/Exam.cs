using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Stylelabs.M.Base.Querying;
using Stylelabs.M.Base.Querying.Filters;
using Stylelabs.M.Framework.Essentials.LoadOptions;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Contracts.Querying;
using Stylelabs.M.Sdk.Exceptions;
using Stylelabs.M.Sdk.Models.Base;
using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.Models.Base.PropertyDefinitions;
using POCO;
using MemberGroup = Stylelabs.M.Sdk.Models.Base.MemberGroup;
using Stylelabs.M.Sdk;
using Stylelabs.M.Sdk.Contracts.Policies;
using Stylelabs.M.Sdk.Models.Policies;

namespace Certification.Demo
{
    class Exam
    {
        IWebMClient client = null;
        public Exam(IWebMClient client) { this.client = client; }

        public static List<Exercise> Exercises = new List<Exercise>()
        {
            new Exercise
            {
                Prompt = "\nConfigure the content/assets page:\n*\tTableView only\n*\tProperties: Only fileSize\n\t...And upload 10 assets (folder 'upload' with 10 images provided) and directly publish them.",
                HowToValidate = ValidateQuestion1Async,
                HowToPrepare = PrepareQuestion1Async
            },
            new Exercise
            {
                Prompt = "\nImport the assets from the provided excel sheet 'import.xlsx'",
                HowToValidate = ValidateQuestion2Async
            },
            new Exercise
            {
                Prompt = "\nCreate a script that immediately sets the description of every asset created by the user 'Creator' to the current timestamp",
                HowToValidate = ValidateQuestion3Async,
                HowToPrepare = PrepareQuestion3Async
            },
            new Exercise
            {
                Prompt = "\nClient X created a taxonomy which is no longer needed and should be deleted. Delete the taxonomy 'M.TrainingTax'.",
                HowToValidate = ValidateQuestion4Async,
                HowToPrepare = PrepareQuestion4Async
            },
            new Exercise
            {
                Prompt = "\ncreate script that generates a public link for the 'preview' rendition \nfor every asset that gets uploaded (if and) as soon as it’s generated",
                HowToValidate = ValidateQuestion5Async
            }
        };

        private static async Task PrepareQuestion1Async(IWebMClient client)
        {
            const string customTax = "M.CustomTax";
            // Check if the taxonomy exists
            long? definitionId = await client.EntityDefinitions.GetIdAsync(customTax);
            if (definitionId == null)
            {
                // Create the taxonomy definition
                IEntityDefinition taxonomy = new EntityDefinition
                {
                    Name = customTax,
                    IsTaxonomyItemDefinition = true,
                    IsSystemOwned = false,
                    DisplayTemplate = "{Name}",
                    MemberGroups =
                    {
                        new MemberGroup
                        {
                            Name = Constants.Asset.MemberGroups.Content,
                            MemberDefinitions =
                            {
                                new StringPropertyDefinition
                                {
                                    Name = "Name",
                                    IncludedInContent = true,
                                    IsMandatory = true,
                                }
                            }
                        }
                    }
                };
                definitionId = await client.EntityDefinitions.SaveAsync(taxonomy);
            }

            // Get the M.Asset definition
            var assetDefinition = await client.EntityDefinitions.GetAsync(Constants.Asset.DefinitionName);
            // Check if the relation is already defined
            var contentGroup = assetDefinition.MemberGroups.Where(g => g.Name == Constants.Asset.MemberGroups.Content).Single();
            const string relationName = "CustomTaxToAssetRelation";
            var relation = contentGroup.MemberDefinitions.Where(x => x.DefinitionType == MemberDefinitionType.Relation && x.Name == relationName);
            if (relation.Count() < 1)
            {
                // DTR: Define The Relation
                contentGroup.MemberDefinitions.Add(new RelationDefinition
                {
                    Name = relationName,
                    ParentIsMandatory = true,
                    ChildIsMandatory = false,
                    Labels =
                    {
                            { new CultureInfo("en-US"), "CustomProp" }
                    },
                    Cardinality = RelationCardinality.ManyToMany,
                    Role = RelationRole.Child,
                    AssociatedEntityDefinitionName = customTax,
                    IsTaxonomyRelation = true,
                    AssociatedLabels =
                    {
                            { new CultureInfo("en-US"), "CustomProp" }
                    },
                    InheritsSecurity = true,
                    NestedProperties = { "Label" },
                    IsNested = true
                });
                // Save all the changes
                try
                {
                    await client.EntityDefinitions.SaveAsync(assetDefinition);
                }
                catch (ValidationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static async Task PrepareQuestion4Async(IWebMClient client)
        {
            const string customTax = "M.TrainingTax";
            // Check if the taxonomy definition exists
            long? definitionId = await client.EntityDefinitions.GetIdAsync(customTax);
            if (definitionId == null)
            {
                // Create the taxonomy definition
                IEntityDefinition taxonomy = new EntityDefinition
                {
                    Name = customTax,
                    IsTaxonomyItemDefinition = true,
                    IsSystemOwned = false,
                    DisplayTemplate = "{Name}",
                    MemberGroups =
                    {
                        new MemberGroup
                        {
                            Name = Constants.Asset.MemberGroups.Content,
                            MemberDefinitions =
                            {
                                new StringPropertyDefinition
                                {
                                    Name = "Name",
                                    IncludedInContent = true,
                                    IsMandatory = true,
                                }
                            }
                        }
                    }
                };
                definitionId = await client.EntityDefinitions.SaveAsync(taxonomy);
            }

            // Get taxonomy entities
            var res = await client.Entities.GetByDefinitionAsync(customTax);
            var taxValues = res.Items;
            // If there are none, create some
            if (taxValues.Count() == 0)
            {
                // Create 5 entities of this definition
                for (int i = 0; i < 5; i++)
                {
                    IEntity entity = await client.EntityFactory.CreateAsync(customTax, CultureLoadOption.Default);
                    entity.SetPropertyValue("Name", string.Format("Value{0}", i));
                    long? id = await client.Entities.SaveAsync(entity);
                }
                res = await client.Entities.GetByDefinitionAsync(customTax);
            }

            // Check if a specific M.TrainingTax entity exists
            Query query = new Query
            {
                Filter = new CompositeQueryFilter
                {
                    Children =
                    {
                        new DefinitionQueryFilter
                        {
                            Name = customTax
                        },
                        new PropertyQueryFilter
                        {
                            Property = "Name",
                            Operator = ComparisonOperator.Equals,
                            Value = "Value0"
                        }
                    }
                }
            };

            long? taxValueId = await client.Querying.SingleIdAsync(query);

            // IF it doesn't exist, create it ...
            if (taxValueId == null)
            {
                IEntity entity = await client.EntityFactory.CreateAsync(customTax, CultureLoadOption.Default);
                entity.SetPropertyValue("Name", string.Format("Value0"));
                taxValueId = await client.Entities.SaveAsync(entity);
            }

            // Get the "M.Builtin.Readers" user group ID
            query = new Query
            {
                Filter = new CompositeQueryFilter
                {
                    Children =
                    {
                        new PropertyQueryFilter
                        {
                            Property = Constants.UserGroup.Properties.GroupName,
                            Operator = ComparisonOperator.Equals,
                            Value = Constants.UserGroup.BuiltinReaders
                        },
                        new DefinitionQueryFilter
                        {
                            Name = Constants.UserGroup.DefinitionName
                        }
                    }
                }
            };

            long? usergroupId = await client.Querying.SingleIdAsync(query);

            // Get the group policy for "M.Builtin.Readers"
            IUserGroupPolicy policy = await client.Policies.GetUserGroupPolicyAsync(usergroupId.Value);

            // Get the rule on M.Asset & M.File
            var assetRule = policy.Rules
                .Where(x => x.DefinitionNames.Contains(Constants.Asset.DefinitionName)
                    && x.DefinitionNames.Contains("M.File"))
                .SingleOrDefault();

            if (assetRule.Conditions.Where(X => X.EntityIds.Contains(taxValueId.Value)).SingleOrDefault() == null)
            {
                // ... and add it to the Policy Rule
                assetRule.Conditions.Add(new Condition
                {
                    EntityIds =
                {
                    taxValueId.Value
                }
                });
                // Save changes
                await client.Policies.UpdateAsync(policy);
            }
        }

        private static async Task PrepareQuestion3Async(IWebMClient client)
        {
            await Exam.CreateUser(client, "Creator", new List<string> { Constants.UserGroup.BuiltinCreators });
        }

        private static async Task<int> ValidateQuestion1Async(IWebMClient client)
        {
            bool result = false;
            int score = 0;
            // Prepare query to get Search component
            // Get Search component
            IEntity searchComponent = await client.Querying.SingleAsync(new Query
            {
                Filter = new IdentifierQueryFilter
                {
                    Value = Constants.Pages.Assets.SearchComponent.Identifier
                }
            });

            if (searchComponent == null)
            {
                Console.WriteLine("[ERR][VAL] Failed to locate Search Component: identifier must have changed.");
            }
            else
            {
                // Validate Output settings
                var stringSettings = searchComponent.GetPropertyValue(Constants.Pages.Components.Settings);
                var settings = JToken.Parse(stringSettings.ToString());
                var views = settings.SelectToken("views").ToObject<List<ViewsProperty>>();
                result = views.Where(x => x.Builder != null).Count() == 1
                    && views.SingleOrDefault(x => x.Builder == Constants.Pages.Assets.Output.TableViewBuilder
                    && x.Definitions.Count == 1
                    && x.Definitions[0].Fields.Count == 1
                    && x.Definitions[0].Fields[0].Name == Constants.Pages.Assets.Output.Fields.FileSize) != null;
                score += result ? 5 : 0;
            }
            // IF the user was able to set the SearchComponent to show TableView only ...
            if (result)
            {
                // Validate number of assets created today
                var res = await client.Querying.QueryAsync(new Query
                {
                    Filter = new CompositeQueryFilter
                    {
                        Children =
                        {
                            new DefinitionQueryFilter
                            {
                                Name = Constants.Asset.DefinitionName
                            },
                            new CreatedOnQueryFilter
                            {
                                Operator = ComparisonOperator.Gte,
                                Value = DateTime.Today
                            },
                            new CreatedByQueryFilter
                            {
                                Username = "amu" // Should be dynamic depending on which user is executing the test
                            }
                        }
                    }
                });
                result = res.Items.ToArray()
                    .Where(x => !string.IsNullOrEmpty(x.GetPropertyValue<string>("FileName")))
                    .Count() > 0;
                score += result ? 5 : 0;
            }

            Console.WriteLine("Question 1: {0}/10", score);
            return score;
        }


        private static async Task<int> ValidateQuestion2Async(IWebMClient client)
        {
            Query query = new Query
            {
                Filter = new CompositeQueryFilter
                {
                    Children =
                    {
                        new DefinitionQueryFilter
                        {
                            Name = Constants.Asset.DefinitionName
                        },
                        new PropertyQueryFilter
                        {
                            Property = Constants.Asset.Properties.Title,
                            Operator = ComparisonOperator.Contains,
                            Value = "import"
                        }
                    }
                }
            };

            var result = await client.Querying.QueryAsync(query);
            int res = result.Items.Count() >= 1 ? 5 : 0;
            Console.WriteLine("Question 2: {0}/5", res);
            return res;
        }

        public static async Task<int> ValidateQuestion3Async(IWebMClient client)
        {
            int score = 0;
            // Try uploading an asset as Admin --> Should NOT trigger the script
            await ImpersonatedValidationQuestion3Async(client, shouldTriggerScript: false)
                .ContinueWith(x => score += x.Result).ConfigureAwait(false);
                
            // Try it with the Creator --> Should trigger the script
            IMClient impersonatedUser = await client.ImpersonateAsync("Creator");
            await ImpersonatedValidationQuestion3Async(impersonatedUser as IWebMClient, shouldTriggerScript: true)
                .ContinueWith(x => score += x.Result).ConfigureAwait(false);

            if(score > 0)
            {
                Console.WriteLine("Question 3: {0}/10", score);
            } else
            {
                Console.WriteLine("Question 3: Description not set to current timestamp (0/10)");
            };
            return score;
        }

        private static async Task<int> ValidateQuestion4Async(IWebMClient client)
        {
            const string customTax = "M.TrainingTax";
            // Check if the taxonomy definition exists
            long? definitionId = await client.EntityDefinitions.GetIdAsync(customTax);

            Console.WriteLine("Question 4: {0}/5", definitionId == null? 5: 0);
            // IF not, you get a cookie
            return definitionId == null? 5: 0;
        }

        private static async Task<int> ValidateQuestion5Async(IWebMClient client)
        {
            IEntity asset = await CreateAsset(client);
            IQueryResult res = null;
            //Console.WriteLine("Waiting for public link(s), this might take a while ... ");
            for (int i = 0; i < 5; i++)
            {
                var timer = DateTime.Now.AddSeconds(10);
                while (DateTime.Now <= timer)
                {
                    continue;
                }
                Query query = new Query
                {
                    Filter = new CompositeQueryFilter
                    {
                        Children =
                        {
                            new DefinitionQueryFilter
                            {
                                Name = "M.PublicLink"
                            },
                            new RelationQueryFilter
                            {
                                Relation = Constants.Asset.Relations.AssetToPublicLink,
                                ParentId = asset.Id
                            },
                            new PropertyQueryFilter
                            {
                                Property = "Resource",
                                Operator = ComparisonOperator.Equals,
                                Value = "preview"
                            } 
                        }
                    }
                };
                res = await client.Querying.QueryAsync(query).ConfigureAwait(false);
                if (res.Items.Count() > 0)
                {
                    break;
                }
            }
            int score = res.Items.Count() > 0 ? 10 : 0;
            Console.WriteLine("Question 5: {0}/10", score);
            return score;
        }

        private static async Task<int> ImpersonatedValidationQuestion3Async(IMClient client, bool shouldTriggerScript = true)
        {
            int score = 0;
            IEntity asset = await CreateAsset(client as IWebMClient);
                bool success = false;
                ICultureSensitiveProperty descriptionProp = asset.GetProperty<ICultureSensitiveProperty>("Description");
                string description = descriptionProp.GetValue<string>(Constants.DefaultCulture);
                if (description != null)
                {
                    var today = DateTime.Parse(description.Substring(26, 10));
                    success = description.ToLower().StartsWith("This asset was created on ".ToLower())
                    && DateTime.Parse(description.Substring(26, 10)) == DateTime.Today;
                }
                score += (success && shouldTriggerScript) || (!success && !shouldTriggerScript) ? 5 : 0;
                return score;
        }

        private static async Task<IEntity> CreateAsset(IWebMClient client, string title = "CustomAsset")
        {
            var assetDefinition = await client.EntityDefinitions.GetAsync(Constants.Asset.DefinitionName);
            var asset = await client.EntityFactory.CreateAsync(Constants.Asset.DefinitionName, CultureLoadOption.Default);
            asset.SetPropertyValue("Title", title);
            var assetId = await client.Entities.SaveAsync(asset);
            asset = await client.Entities.GetAsync(assetId);
            long jobId = await Jobs.CreateFetchJob(assetId, new Uri(Constants.Blob.OnePixelImage), client as IWebMClient);
            if (jobId > 0)
            {
                // create timer
                var timer = DateTime.Now;
                // configure timer
                var limit = timer.AddSeconds(10);
                while (await Jobs.IsJobCompleted(jobId, client as IWebMClient) && timer <= limit)
                {
                    // Wait for job to finish, max 10 seconds
                    if (timer == limit)
                    {
                        Console.WriteLine("[FetchJob] TimeOut Exceeded. Moving on...");
                    }
                    continue;
                }
            }
            return asset;
        }

        public static async Task CreateUser(IWebMClient client, string username, ICollection<string> userGroups)
        {
            // 'Everyone' is a mandatory usergroup
            if (!userGroups.Contains(Constants.UserGroup.Everyone))
            {
                userGroups.Add(Constants.UserGroup.Everyone);
            }

            // Check if user exists already
            IEntity user = await client.Querying.SingleAsync(new Query
            {
                Filter = new CompositeQueryFilter
                {
                    Children =
                    {
                        new DefinitionQueryFilter
                        {
                            Name = Constants.User.DefinitionName
                        },
                        new PropertyQueryFilter
                        {
                            Property = Constants.User.Properties.Username,
                            Operator = ComparisonOperator.Equals,
                            Value = username
                        }
                    }
                }
            });

            if(user != null)
            {
                return;
            }

            Console.WriteLine("Creating user '{0}' ...", username);
            user = await client.EntityFactory.CreateAsync(Constants.User.DefinitionName, CultureLoadOption.Default);
            IRelation usergroupToUserRelation = await user.GetRelationAsync(Constants.User.Relations.UserGroupToUser);

            // Add user to the specified user groups
            userGroups.ToList().ForEach(async groupName =>
            {
                long? usergroupId = await client.Querying.SingleIdAsync(new Query
                {
                    Filter = new CompositeQueryFilter
                    {
                        Children =
                        {
                            new DefinitionQueryFilter
                            {
                                Name = Constants.UserGroup.DefinitionName
                            },
                            new PropertyQueryFilter
                            {
                                Property = Constants.UserGroup.Properties.GroupName,
                                Operator = ComparisonOperator.Equals,
                                Value = groupName
                            }
                        }
                    }
                });

                if(usergroupId != null)
                {
                    usergroupToUserRelation.GetIds().Add(usergroupId.Value);
                }
            });
            var timer = DateTime.Now.AddSeconds(1);
            while ( DateTime.Now <= timer) { continue; }
            user.SetPropertyValue(Constants.User.Properties.Username, username);
            await client.Entities.SaveAsync(user);
        }

        
    }
}
