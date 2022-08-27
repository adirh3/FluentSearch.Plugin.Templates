using System.Threading.Tasks;
using Avalonia.Input;
using Blast.Core;
using Blast.Core.Interfaces;
using Blast.Core.Results;

namespace Template.Fluent.Plugin;

public class MyFirstSearchOperation : SearchOperationBase
{
    protected internal MyFirstSearchOperation() : base("First operation",
        "First operation description", "\uE74E")
    {
        HideMainWindow = false;
        // Control + S for save by default
        KeyGesture = new KeyGesture(Key.S, KeyModifiers.Control);
    }
}

public class MySecondSearchOperation : SearchOperationBase
{
    protected internal MySecondSearchOperation() : base("Second operation",
        "Second operation description", "\uE74D")
    {
        HideMainWindow = true;
        // Defaults to Delete gesture
        KeyGesture = new KeyGesture(Key.Delete);
    }
}

public class MyFuncSearchOperation : SearchOperationBase
{
    protected internal MyFuncSearchOperation() : base("My function operation",
        "Function operation description", "\uE74D")
    {
        HideMainWindow = true;
        // Defaults to Delete gesture
        KeyGesture = new KeyGesture(Key.D2, KeyModifiers.Control);
    }

    public override async ValueTask<IHandleResult> HandleSearchResult(ISearchResult searchResult)
    {
        // Here we can handle the trigger of the search operation, alternative to HandleSearchResult in the search app  
        await Task.Delay(200);
        return new HandleResult(true, false);
    }
}