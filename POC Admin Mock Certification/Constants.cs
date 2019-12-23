using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Certification.Demo
{
    public static class Constants
    {
        public static readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");

        public static class Asset
        {
            public const string DefinitionName = "M.Asset";

            public static class MemberGroups
            {
                public const string Content = "Content";
            }

            public static class Properties
            {
                public const string ApprovalDate = "ApprovalDate";
                public const string Title = "Title";
                public const string Description = "Description";
                public const string FileName = "FileName";
            }

            public static class Relations
            {
                public const string AssetTypeToAsset = "AssetTypeToAsset";
                public const string AssetMediaToAsset = "AssetMediaToAsset";
                public const string ContentRepositoryToAsset = "ContentRepositoryToAsset";
                public const string FinalLifeCycleStatusToAsset = "FinalLifeCycleStatusToAsset";
                internal static string AssetToPublicLink = "AssetToPublicLink";
            }
        }

        public static class ContentRepository
        {
            public const string DefinitionName = "M.Content.Repository";
        }

        public static class FinalLifeCycleStatus
        {
            public const string DefinitionName = "M.Final.LifeCycle.Status";

            public static class Properties
            {
                public const string StatusValue = "StatusValue";
            }
        }

        public static class AssetType
        {
            public const string DefinitionName = "M.AssetType";

            public static class Properties
            {
                public const string Label = "Label";
            }

            public static class Relations
            {
                public const string AssetTypeToAsset = "AssetTypeToAsset";
            }
        }

        public static class AssetMedia
        {
            public const string DefinitionName = "M.AssetMedia";

            public static class Properties
            {
                public const string ClassificationName = "ClassificationName";
            }

            public static class Relations
            {
                public const string AssetMediaToAsset = "AssetMediaToAsset";
            }
        }

        public static class UserGroup
        {
            public const string DefinitionName = "UserGroup";

            public const string Everyone = "Everyone";
            public const string Superusers = "Superusers";
            public const string BuiltinReaders = "M.Builtin.Readers";
            public const string BuiltinCreators = "M.Builtin.Creators";

            public static class Properties
            {
                public const string GroupName = "GroupName";
            }

            public static class Relations
            {
                public const string UserGroupToUser = "UserGroupToUser";
            }
        }

        public static class User
        {
            public const string DefinitionName = "User";

            public static class Properties
            {
                public const string Username = "Username";
                public const string EmailConfirmationPending = "EmailConfirmationPending";
            }

            public static class Relations
            {
                public const string UserToUserProfile = "UserToUserProfile";
                public const string UserGroupToUser = "UserGroupToUser";
            }
        }

        public static class UserProfile
        {
            public const string DefinitionName = "M.UserProfile";

            public static class Properties
            {
                public const string Email = "Email";
                public const string Username = "Username";
            }
        }

        public static class Job
        {
            public const string DefinitionName = "M.Job";

            public static class Properties
            {
                public const string Condition = "Job.Condition";
                public const string State = "Job.State";
                public const string Type = "Job.Type";
            }

            public static class Conditions
            {
                public const string Failed = "Failed";
                public const string Pending = "Pending";
                public const string Success = "Success";
            }

            public static class States
            {
                public const string Failed = "Failed";
                public const string Pending = "Pending";
                public const string Completed = "Completed";
            }

            public static class Types
            {
                public const string Processing = "Processing";
            }
        }

        public static class DataSources
        {
        }

        public static class Permissions
        {
        }

        public static class Properties
        {
            public const string Identifier = "Identifier";
            public const string CreatedOn = "created_on";
            public const string ModifiedOn = "modified_on";
        }

        public static class Renditions
        {
            public const string Preview = "preview";
            public const string Thumbnail = "thumbnail";
        }

        public static class LifeCycleStatus
        {
            public const string Created = "M.Final.LifeCycle.Status.Created";
            public const string Approved = "M.Final.LifeCycle.Status.Approved";
            public const string Rejected = "M.Final.LifeCycle.Status.Rejected";
            public const string Archived = "M.Final.LifeCycle.Status.Archived";
        }

        public static class ContentRepositories
        {
            public const string Standard = "M.Content.Repository.Standard";
        }

        public static class Pages
        {
            public const string DefinitionName = "Portal.Page";
            public static class Assets
            {
                public const string Identifier = "Portal.Page.Assets";
                public const string PageName = "Assets";
                public const string MassEditTable = "Portal.PageComponent.AssetMassEditTable.MassEditTable";
                public static class SearchComponent
                {
                    public const string Identifier = "Portal.PageComponent.Assets.search";
                    public static class Settings
                    {
                        public const string PropertyName = "settings";
                        public const string Facets = "facets";
                        public const string QueryQuilder = "querybuilder";
                    }
                    
                }
                public static class Output
                {
                    public const string ListViewBuilder = "Builders.Containers.List";
                    public const string TableViewBuilder = "Builders.Containers.Table";
                    public static class Fields
                    {
                        public const string FileSize = "FileSize";
                        public const string Title = "Title";
                        public const string Description = "Description";
                    }
                }
            }

            public static class Relations
            {
                public const string PageToPageComponent = "PageToPageComponent";
            }

            public static class Properties
            {
                public const string PageNameProperty = "Page.Name";
            }

            public static class Components
            {
                public const string Identifier = "Portal.PageComponent";
                public const string Settings = "PageComponent.Settings";
            }


        }

        public static class AppSettings
        {
            public const string MOriginAddress = "https://mocki3.stylelabs.io";
            public const string MClientId = "certification_process_automation_demo";
            public const string MClientSecret = "Ysvjf2nWbFaptsw4xMGXkvW4";
            public const string MUsername = "administrator";
            public const string MPassword = "H_7W7sC";
            public const string TempDirectory = "TempDirectory";
            public const string VisionKey = "VisionKey";
        }

        public static class Blob
        {
            public const string OnePixelImage = "https://stylelabs.com/wp-content/uploads/2017/09/logo-marketingcontenthub-600.png";

        }
    }
}
