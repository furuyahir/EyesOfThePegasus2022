using System.Collections.Generic;

public interface ITimelineItem
{
    string ID { get; }
    string Title { get; }
    string Subtitle { get; }
    string Description { get; }
    string Location { get; }
    CompletionState CompletionState { get; }
    Priority Priority { get; }
    LinkedList<string> Ownership { get; }
    HashSet<ITimelineItem> Dependencies { get; }
    int Cost(CostType costType);
    void UpdateCost(CostType costType, int value);
    void SetCompletion(CompletionState state);
    void SetPriority(Priority priority);
    void AddOwnership(string newOwner);

}
