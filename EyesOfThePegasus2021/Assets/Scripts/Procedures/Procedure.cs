using System.Collections;
using System.Collections.Generic;

// TODO: Fill out this class
public class Procedure : ITimeline
{
    public ITimelineItem CurrentItem()
    {
        throw new System.NotImplementedException();
    }

    public LinkedList<ITimelineItem> Timeline()
    {
        throw new System.NotImplementedException();
    }

    public ITimelineItem RemoveTimelineItem(ITimelineItem timelineItem)
    {
        throw new System.NotImplementedException();
    }

    public ITimelineItem AddTimelineItem(ITimelineItem timelineItemToAdd, ITimelineItem previousTimelineItem)
    {
        throw new System.NotImplementedException();
    }

    public int CurrentTimelineCost(CostType costType)
    {
        throw new System.NotImplementedException();
    }
}
