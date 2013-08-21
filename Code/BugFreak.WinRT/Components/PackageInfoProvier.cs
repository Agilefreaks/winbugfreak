namespace BugFreak.WinRT.Components
{
    using System.Collections.Generic;
    using global::BugFreak.Components;
    using Windows.ApplicationModel;

    internal class PackageInfoProvier : IErrorDataProvider
    {
        public List<KeyValuePair<string, string>> GetData()
        {
            var result = new List<KeyValuePair<string, string>>();

            var package = Package.Current.Id;
            result.Add(new KeyValuePair<string, string>("Name", package.FamilyName));
            result.Add(new KeyValuePair<string, string>("FullName", package.FullName));
            result.Add(new KeyValuePair<string, string>("Version", string.Format("{0}.{1}.{2}.{3}", package.Version.Major, package.Version.Minor, package.Version.Build, package.Version.Revision)));
            result.Add(new KeyValuePair<string, string>("Architecture", package.Architecture.ToString()));
            
            return result;
        }
    }
}
