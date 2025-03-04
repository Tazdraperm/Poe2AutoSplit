using Poe2AutoSplit.Component.AutoSplitter.Event;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class AreaEvent : SplitEvent
    {
        public string Id { get; }

        public AreaEvent(string name, string id, params SplitEvent[] requiredEvents) : base(name, requiredEvents)
        {
            Id = id;
        }
    }
}
