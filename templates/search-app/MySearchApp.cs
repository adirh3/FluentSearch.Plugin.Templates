using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Blast.API.Search;
using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;

namespace Template.Fluent.Plugin
{
    public class MySearchApp : ISearchApplication
    {
        private readonly List<ISearchOperation> _supportedOperations;
        private readonly MySearchAppSettings _mySettings;
        private readonly SearchApplicationInfo _applicationInfo;

        private readonly HashSet<string> _searchDatabase = new()
        {
            "Jack Pot",
            "Fluent Search",
            "This is a test"
        };

        private readonly List<SearchTag> _searchTags = new()
        {
            new SearchTag
            {
                Name = "mysearchapp"
            }
        };

        public MySearchApp()
        {
            _supportedOperations = new List<ISearchOperation>
            {
                new MyFirstSearchOperation(),
                new MySecondSearchOperation(),
                new MyFuncSearchOperation()
            };
            _applicationInfo = new SearchApplicationInfo("My search app",
                "My search app description", _supportedOperations)
            {
                MinimumSearchLength = 1,
                IsProcessSearchEnabled = false,
                IsProcessSearchOffline = false,
                // You can find icon glyphs here - https://docs.microsoft.com/en-us/windows/apps/design/style/segoe-fluent-icons-font
                ApplicationIconGlyph = "\ue728",
                SearchAllTime = ApplicationSearchTime.Fast,
                DefaultSearchTags = _searchTags
            };
            // Create a custom setting page, if not specified Fluent Search will create a default one
            _applicationInfo.SettingsPage = _mySettings = new MySearchAppSettings(_applicationInfo);
        }

        public ValueTask LoadSearchApplicationAsync()
        {
            // Load search app is called once when the app is loaded into Fluent Search
            return ValueTask.CompletedTask;
        }

        public SearchApplicationInfo GetApplicationInfo()
        {
            return _applicationInfo;
        }

        public ValueTask<ISearchResult> GetSearchResultForId(object searchObjectId)
        {
            if (searchObjectId is MySearchObjectId mySearchObject)
                return new ValueTask<ISearchResult>(new MySearchResult(mySearchObject.Id, string.Empty, 0,
                    _supportedOperations, _searchTags));
            return new ValueTask<ISearchResult>();
        }

        public async IAsyncEnumerable<ISearchResult> SearchAsync(SearchRequest searchRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            string searchedText = searchRequest.SearchedText;
            string searchedTag = searchRequest.SearchedTag;

            // We don't have this tag
            if (!string.IsNullOrEmpty(searchedTag) && searchedTag != "mysearchapp")
                yield break;

            foreach (string name in _searchDatabase)
            {
                // Use the Blast API to search inside the text
                double score = name.SearchTokens(searchedText);
                if (score > 0) // something was found
                {
                    // Return a new result
                    yield return new MySearchResult(name, searchedText, score, _supportedOperations, _searchTags);
                }
            }
        }

        public ValueTask<IHandleResult> HandleSearchResult(ISearchResult searchResult)
        {
            if (searchResult is not MySearchResult mySearchResult)
                throw new InvalidCastException(nameof(MySearchResult));

            switch (searchResult.SelectedOperation)
            {
                case MyFirstSearchOperation:
                    // Handle first operation
                    break;
                case MySecondSearchOperation:
                    // Handle second operation
                    break;
                // We don't have MyFuncSearchOperation because it will handle itself (this code will not be called)
            }

            return new ValueTask<IHandleResult>(new HandleResult(true, false));
        }
    }
}