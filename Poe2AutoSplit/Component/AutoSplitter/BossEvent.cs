using System.Collections.Generic;
using Poe2AutoSplit.Component.AutoSplitter.Event;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class BossEvent : SplitEvent
    {
        public readonly List<string> VoiceLines;

        public BossEvent(string name, List<string> voiceLines, params SplitEvent[] requiredEvents) : base(name,
            requiredEvents)
        {
            VoiceLines = voiceLines;
        }
    }
}
