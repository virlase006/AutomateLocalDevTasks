using Certification.Demo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Stylelabs.M.Base.Querying;
using Stylelabs.M.Base.Querying.Filters;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Models.Base;
using Stylelabs.M.Sdk.Models.Base.PropertyDefinitions;
using Stylelabs.M.Sdk.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock.Implementation
{
    public static class Admin
    {
        static bool success = false;

        public static object Stringstream { get; private set; }

        public static async Task<bool> ValidateAssetDeifinition(IWebMClient client, AssetValidationConfiguration police)
        {
            if (police is null)
            {
                var what = String.Format("{0} {1}", nameof(AssetValidationConfiguration), nameof(police));
                throw new ArgumentNullException(what);
            }
            return await ValidateAssetPropertiesAsync(police, client);
        }

        private static string CheckMemberAttributes(IMemberGroup group, Validation prop)
        {
            var p = (StringPropertyDefinition)group.MemberDefinitions.Single(x => x.Name == prop.PropertyName);
            var val = prop.Config;
            var mandatoryCheck = p.IsMandatory == val.Required;
            var conditionalCheck = p.IsConditional == val.Conditional;
            var editableCheck = p.AllowUpdates == val.Editable;
            var autocompleteCheck = p.IncludedInCompletion == val.Autocomplete;
            var fullTextSearchableCheck = p.IncludedInContent == val.FullTextSearchable;
            bool isValid = mandatoryCheck
                && conditionalCheck
                && editableCheck
                && autocompleteCheck
                && fullTextSearchableCheck;

            var failures = new List<string>();
            if (!mandatoryCheck) { failures.Add("Required"); }
            if (!conditionalCheck) { failures.Add("Conditional"); }
            if (!editableCheck) { failures.Add("Editable"); }
            if (!autocompleteCheck) { failures.Add("Include-in-autocomplete"); }
            if (!fullTextSearchableCheck) { failures.Add("FullText-searchable"); }

            var logger = new VLogger<StringPropertyDefinition>
            {
                Type = LogType.VALIDATION,
                About = "The attributes test",
                Passed = isValid,
                Reason = failures.Count() < 1 ? null : failures,
                ActualConfiguration = p, // JSON 
                ExpectedConfiguration = prop,
                Message = JsonConvert.SerializeObject(failures)
            };
            logger.Info();

            Console.WriteLine("[VALIDATION][{0}][{1}] The attributes test", isValid ? "PASS" : "FAIL", prop.PropertyName);

            return JsonConvert.SerializeObject(failures);
        }

        private static string CheckRelationAttributes(IMemberGroup group, Validation prop)
        {
            var p = (RelationDefinition)group.MemberDefinitions.Single(x => x.Name == prop.PropertyName);
            var val = prop.Config;
            var mandatoryCheck = (p.ChildIsMandatory || p.ParentIsMandatory) == val.Required;
            var conditionalCheck = p.IsConditional == val.Conditional;
            //var editableCheck = p.AllowUpdates == val.Editable; // 3.2.0

            var failures = new List<string>();
            if (!mandatoryCheck) { failures.Add("Required"); }
            if (!conditionalCheck) { failures.Add("Conditional"); }
            //if (!editableCheck) { failures.Add("Editable"); } // 3.2.0

            bool isValid = mandatoryCheck && conditionalCheck; // && editableCheck; // 3.2.0

            new VLogger<RelationDefinition>
            {
                Type = LogType.VALIDATION,
                About = "The attributes test",
                Passed = isValid,
                Reason = failures.Count() < 1 ? null : failures,
                ActualConfiguration = p, // JSON 
                ExpectedConfiguration = prop,
                Message = null
            }.Info();

            Console.WriteLine("[VALIDATION][{0}][{1}] The attributes test", isValid ? "PASS" : "FAIL", prop.PropertyName);
            return JsonConvert.SerializeObject(failures);
        }

        private static async Task<bool> ValidateAssetPropertiesAsync(AssetValidationConfiguration police, IWebMClient client)
        {
            foreach (Validation prop in police.PropertyValidations)
            {

                string potentialErrorMessage = String.Format("[EXCEPTION][VALIDATION][PROPERTY(\"{1}\")] property {0}.isRelation was wrongfully set to {2}. This is unacceptable.", prop.PropertyName, nameof(prop.IsRelation), prop.IsRelation);
                var definition = await client.EntityDefinitions.GetAsync(Constants.Asset.DefinitionName);
                var group = definition.MemberGroups.Single(x => x.Name == prop.MemberGroup);
                if (group == null)
                {
                    // Member Group not found
                    new SCLogger
                    {
                        Type = LogType.EXCEPTION,
                        About = "The attributes test",
                        Passed = false,
                        Reason = { String.Format("MemberGroup {0} not found.", prop.MemberGroup) },
                        Message = "This Membergroup might have been renamed or removed."
                    }.Info();
                    break;
                }

                if (!prop.IsRelation)
                {
                    try
                    {
                        CheckMemberAttributes(group, prop);
                        bool advancedSearchCheck = await ValidatePresenceInAdvancedSearchAsync(client, prop);
                    }
                    catch (InvalidCastException e)
                    {
                        Console.WriteLine(potentialErrorMessage);
                        throw;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(prop.AssociatedDefinition))
                    {
                        throw new ArgumentNullException("associated_definition", "The validation.json file: \"isRelation\" is set to true but \"definition\" was null or empty. This is unacceptable.");
                    }
                    try
                    {
                        CheckRelationAttributes(group, prop);
                        success = await ValidatePresenceInAdvancedSearchAsync(client, prop).ContinueWith(x =>
                        {
                            Console.WriteLine("[VALIDATION][{0}][{1}] Advanced Search", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                            return x.Result;
                        });

                    }
                    catch (InvalidCastException e)
                    {
                        Console.WriteLine(potentialErrorMessage);
                        throw;
                    }
                }

                var val = prop.Config;
                if (val.FacetEnabled)
                {
                    success = await ValidateFacetConfigurationAsync(client, val).ContinueWith(x =>
                    {
                        Console.WriteLine("[VALIDATION][{0}][{1}] Facet enabled", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                        return x.Result;
                    });
                }


                success = await ValidateIfPropertyIsDisplayedInSearchComponentAsync(client, prop, "list").ContinueWith(x =>
                {
                    Console.WriteLine("[VALIDATION][{0}][{1}] Show in grid-view", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                    return x.Result;
                });

                success = await ValidateIfPropertyIsDisplayedInSearchComponentAsync(client, prop, "table").ContinueWith(x =>
                {
                    Console.WriteLine("[VALIDATION][{0}][{1}] Show in table-view", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                    return x.Result;
                });
                success = await ValidateIfPropertyIsDisplayedInSearchComponentAsync(client, prop, "flyout").ContinueWith(x =>
                {
                    Console.WriteLine("[VALIDATION][{0}][{1}] Show in flyout-view", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                    return x.Result;
                });
                success = await ValidatePresenceInAssetMassEditTableAsync(client, prop).ContinueWith(x =>
                {
                    Console.WriteLine("[VALIDATION][{0}][{1}] Mass edit table", x.Result ? "PASS" : "FAIL", prop.PropertyName);
                    return x.Result;
                });
            }

            return success;
        }

        private static async Task<bool> ValidateFacetConfigurationAsync(IWebMClient client, MemberConfig config)
        {
            bool isValid = false;
            FacetConfig facetConfig = config.FacetConfig;
            var settings = await GetSearchComponentSettingsAsync(client);
            var facets = settings.SelectToken("facets").ToObject<List<FacetConfig>>();
            if (facetConfig == null && config.FacetEnabled)
            {
                throw new ArgumentNullException("FacetConfig", "The validation.json file: \"Facet_enabled\" is set to true but \"Facet_config\" was null or empty. This is unacceptable.");
            }

            if (facetConfig.Type.Equals("property") && facetConfig.Definition == null)
            {
                throw new ArgumentNullException("FacetConfig", "The validation.json file: Facet of type \"property\" encountered but \"definition\" was not set. This is unacceptable.");
            }

            var facet = facets.SingleOrDefault(f => f.Name == facetConfig.Name && f.Definition == f.Definition);

            if (facet == null && !config.FacetEnabled)
            {
                return false; // facet not configured (correctly)
            }
            isValid = facet.Name == facetConfig.Name
                    && facet.Position == facetConfig.Position;
            /*&& facet.Type == facetConfig.Type
            //&& facet.Definition == facetConfig.Definition
            //&& facet.HasLimit == facetConfig.HasLimit
            //&& facet.Sorting == facetConfig.Sorting
            //&& facet.Direction == facetConfig.Direction
            //&& facet.ApplyGrouping == facetConfig.ApplyGrouping
            */
            return isValid;
        }

        public static async Task<bool> ValidateFacetEnablePropertyAsync(IWebMClient client, Validation prop)
        {
            bool isValid = false;
            bool shouldBeEnabled = prop.Config.FacetEnabled;
            var settings = await GetSearchComponentSettingsAsync(client);
            var facets = settings.SelectToken(Constants.Pages.Assets.SearchComponent.Settings.Facets).Children();
            isValid = facets.SingleOrDefault(x => x.SelectToken("name").Value<string>() == prop.PropertyName) != null;
            new SCLogger
            {
                Type = LogType.VALIDATION,
                About = "Facet enabled",
                Passed = isValid,
                Reason = new List<string> { isValid ? "" : "Facet was not enabled." },
                Message = isValid ? "" : string.Format("Expected Configuration:           {0}\n", JsonConvert.SerializeObject(prop, Formatting.Indented))
                + (isValid ? "" : string.Format("Actual Facet list:             {0}", JsonConvert.SerializeObject(facets, Formatting.Indented)))
            }.Info();
            return isValid;
        }

        private static async Task<bool> ValidatePresenceInAdvancedSearchAsync(IWebMClient client, Validation prop)
        {
            const string whatIsBeingValidated = "Show in Advanced Search";
            var propertyName = prop.PropertyName;
            var definition = prop.AssociatedDefinition;
            var settings = await GetSearchComponentSettingsAsync(client);
            if (settings == null)
            {
                new SCLogger
                {
                    Type = LogType.EXCEPTION,
                    About = whatIsBeingValidated,
                    Passed = false,
                    Reason = new List<string> { "Property \"PageComponent.Settings\" not found." },
                    Message = "This Property might have been renamed."
                }.Info();
                return false;
            }
            var properties = settings.SelectToken(Constants.Pages.Assets.SearchComponent.Settings.QueryQuilder).Children(); // returns an array
            var propertyQueryFilter = prop.IsRelation ? prop.AssociatedDefinition : prop.PropertyName;
            var property = properties.SingleOrDefault(x => x.SelectToken("name").Value<string>() == propertyQueryFilter);
            bool isValid = (property == null && !prop.Config.ShowInAdvancedSearch) || (property != null && prop.Config.ShowInAdvancedSearch);
            if (property == null)
            {
                new SCLogger
                {
                    Type = LogType.VALIDATION,
                    About = whatIsBeingValidated,
                    Passed = isValid,
                    Reason = new List<string> { isValid ? "" : String.Format("Property '{0}' not found.", propertyName) },
                    ExpectedConfiguration = prop,
                    Message = String.Format("\nAdvanced searchable properties:\n {0}", JsonConvert.SerializeObject(properties, Formatting.Indented))
                }.Info();
                return isValid;
            }
            else
            {
                var associatedDefinition = prop.IsRelation ? property.SelectToken("type") : property.SelectToken("definition");
                var associatedDefinitionFound = associatedDefinition.Value<string>();
                if (prop.IsRelation && (associatedDefinition != null && associatedDefinitionFound != "definition"))
                {
                    new SCLogger
                    {
                        Type = LogType.VALIDATION,
                        About = whatIsBeingValidated,
                        Passed = false,
                        Reason = new List<string> { String.Format("Property \"{0}\" was found but was not a relation.", propertyName) },
                        Message = "This is unacceptable.",
                        ExpectedConfiguration = prop
                    }.Info();
                    return false;
                }
            }


            var logger = new SCLogger
            {
                Type = LogType.VALIDATION,
                About = whatIsBeingValidated,
                Passed = true,
                Reason = new List<string>(),
                ExpectedConfiguration = prop,
                Message = ""
            };
            logger.Info();
            return true;
        }

        private static async Task<bool> ValidatePresenceInAssetMassEditTableAsync(IWebMClient client, Validation prop)
        {
            var massEditTable = await client.Querying.SingleAsync(new Query
            {
                Filter = new IdentifierQueryFilter { Value = Constants.Pages.Assets.MassEditTable }
            });
            var columns = massEditTable.GetPropertyValue<JToken>(Constants.Pages.Components.Settings)
                .SelectToken("columns");
            var massEditTableProperty = columns.SingleOrDefault(x => x.SelectToken("name").Value<string>() == prop.PropertyName);
            var isValid = massEditTableProperty != null                       // property is present in the table
                && (massEditTableProperty.SelectToken("read_only") == null                   // property MIGHT BE editable in the table
                || massEditTableProperty.SelectToken("read_only").Value<bool>() == false);   // property is editable in the table ...

            isValid = (isValid == prop.Config.BulkEditInTable); // ... but is this what we were expecting ??
            var logger = new SCLogger
            {
                Type = LogType.VALIDATION,
                About = "Editable in MassEdit Table",
                Passed = isValid,
                Reason = new List<string> { isValid ? "" : String.Format(prop.Config.BulkEditInTable ? "Property {0} not found or is read-only." : "Property {0} was mass editable in table.", prop.PropertyName) },
                Message = isValid ? "" : string.Format("Expected Configuration:           {0}\n", JsonConvert.SerializeObject(prop, Formatting.Indented))
                + (isValid ? "" : string.Format("Mass edit table:             {0}", JsonConvert.SerializeObject(columns, Formatting.Indented))),
                ExpectedConfiguration = prop
            };
            logger.Info();
            return isValid;
        }

        private static async Task<bool> ValidateIfPropertyIsDisplayedInSearchComponentAsync(IWebMClient client, Validation prop, string viewName = "table")
        {
            bool isValid = false;
            var settings = await GetSearchComponentSettingsAsync(client);
            var views = settings.SelectToken("views").Children(); // returns an array
                                                                  // select the specified view element
            var token = JToken.FromObject(prop).SelectToken("value");
            string p = string.Format("show_in_{0}view", viewName);
            var nice = token.SelectToken(p);
            var shouldBeShown = nice.Value<bool>();

            if (settings != null)
            {
                var view = views.SingleOrDefault(x => x.SelectToken("name").Value<string>() == viewName);
                if (view == null || view.SelectToken("definitions") == null)
                {
                    return false;
                }
                var definitions = view.SelectToken("definitions").ToList().SingleOrDefault(x => x.Value<string>("definition") == Constants.Asset.DefinitionName);
                if (definitions == null)
                {
                    return false;
                }
                // Check & validate if one of its field is "Title"
                var fields = definitions.SelectToken("fields");
                var isDisplayed = fields.SingleOrDefault(x => x.SelectToken("name").ToObject<string>() == prop.PropertyName) != null;
                isValid = (isDisplayed && shouldBeShown) || (!isDisplayed && !shouldBeShown);

                SCLogger logger = new SCLogger
                {
                    Type = LogType.VALIDATION,
                    About = string.Format("Show in {0}view", viewName),
                    Passed = isValid,
                    ExpectedConfiguration = prop,
                    Reason = new List<string> { isValid ? "" : String.Format("Property {0} not found in {1}view.", prop.PropertyName, viewName) },
                    Message = isValid ? "" : string.Format("Expected Configuration:           {0}\n", JsonConvert.SerializeObject(prop, Formatting.Indented))
                             + (isValid ? "" : string.Format("Actual fields in {0}view:             {1}", viewName, JsonConvert.SerializeObject(fields, Formatting.Indented)))
                };

                logger.Info();
            }
            return isValid;
        }

        private static async Task<JToken> GetSearchComponentSettingsAsync(IWebMClient client)
        {
            IEntity searchComponent = await GetSearchComponent(client);
            if (searchComponent == null)
            {
                Console.WriteLine("[ERR][VAL] Failed to locate Search Component: identifier must have changed.");
                return null;
            }
            // Validate Output settings
            JToken settings = searchComponent.GetPropertyValue<JToken>(Constants.Pages.Components.Settings);
            return settings;
        }

        private static async Task<IEntity> GetSearchComponent(IWebMClient client)
        {
            var comp = await client.Querying.SingleAsync(new Query
            {
                Filter = new IdentifierQueryFilter
                {
                    Value = Constants.Pages.Assets.SearchComponent.Identifier
                }
            });
            return comp;
        }
    }
}