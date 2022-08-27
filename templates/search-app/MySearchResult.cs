using System.Collections.Generic;
using Blast.Core.Interfaces;
using Blast.Core.Results;

namespace Template.Fluent.Plugin
{
    public class MySearchObjectId
    {
        public string Id { get; set; }
    }

    public sealed class MySearchResult : SearchResultBase
    {
        public MySearchResult(string name, string searchedText, double score,
            IList<ISearchOperation> searchOperations, ICollection<SearchTag> searchTags) : base(
            name, searchedText, "Result type", score, searchOperations, searchTags)
        {
            // The search object Id is used to reload this result if the user pinned or cached it
            SearchObjectId = new MySearchObjectId {Id = name};
            UseIconGlyph = true;
            IconGlyph = "";
            ShouldCacheResult = true;
            DisableMachineLearning = false;
        }

        protected override void OnSelectedSearchResultChanged()
        {
        }
    }
}