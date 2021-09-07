using System.Collections.Generic;

public interface ITimeline
{
    ITimelineItem CurrentItem();
    LinkedList<ITimelineItem> Timeline();
    ITimelineItem RemoveTimelineItem(ITimelineItem timelineItem);
    ITimelineItem AddTimelineItem(ITimelineItem timelineItemToAdd,
        ITimelineItem previousTimelineItem);

    int CurrentTimelineCost(CostType costType);
    // TODO: Methods to generate new ITimelines based on objective
}
