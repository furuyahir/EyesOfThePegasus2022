using System.Collections.Generic;

public class InstructionItem : ITimelineItem
{
    public int CostValue;
    public string ID { get; private set;}
    public string Title { get; private set;}
    public string Subtitle { get; private set;}
    public string Description { get; private set;}
    public string Location { get; }
    public CompletionState CompletionState { get; private set;}
    public Priority Priority { get; private set;}
    public LinkedList<string> Ownership { get; private set;}
    public HashSet<ITimelineItem> Dependencies { get; }

    public InstructionItem(string id, string title, string subtitle, string description, string 
    location , int costValue, Priority priority, string owner, ITimelineItem[] dependencies,
        CompletionState completionState = CompletionState.INCOMPLETE)
    {
        Dependencies = new HashSet<ITimelineItem>(dependencies);
    }
    
    public int Cost(CostType costType)
    {
        return CostValue;
    }

    public void UpdateCost(CostType costType, int value)
    {
        CostValue = value;
    }

    public void SetCompletion(CompletionState state)
    {
        CompletionState = state;
    }

    public void SetPriority(Priority priority)
    {
        Priority = priority;
    }

    public void AddOwnership(string newOwner)
    {
        Ownership.AddFirst(newOwner);
    }

}
