using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Poe2AutoSplit.Component.AutoSplitter.Event
{
    public class SplitEvent
    {
        public static IEnumerable<SplitEvent> AllEvents => Events;

        private static readonly HashSet<SplitEvent> Events = new HashSet<SplitEvent>();
        private static readonly Dictionary<string, SplitEvent> EventByName = new Dictionary<string, SplitEvent>();
        private static readonly Dictionary<string, AreaEvent> AreaEventById = new Dictionary<string, AreaEvent>();
        private static readonly Dictionary<BossEvent, List<string>> VoiceLinesByBossEvent =
            new Dictionary<BossEvent, List<string>>();

        // This probably should not be here
        // But some class have to extract SplitEvent from log line
        // So let it be here
        private const string TimestampTemplate = @"^[^ ]+ [^ ]+ \d+";
        private static readonly Regex GeneratingAreaRegex = new Regex(TimestampTemplate + ".*Generating level (\\d+) area \"(.*)\"");

        static SplitEvent()
        {
            AddAreaEvent("G1_town", "Clearfell Encampment");
            AddAreaEvent("G1_2", "Clearfell");
            AddAreaEvent("G1_3", "Mud Burrow");
            AddAreaEvent("G1_4", "Grelwood");
            var redVale = AddAreaEvent("G1_5", "Red Vale");
            AddAreaEvent("G1_6", "Grim Tangle", redVale);
            AddAreaEvent("G1_7", "Cemetery of the Eternals");
            AddAreaEvent("G1_9", "Tomb of the Consort");
            AddAreaEvent("G1_8", "Mausoleum of the Praetor");
            AddBossEvent("Lachlann", "Lachlann of Endless Lament: Together... at last...");
            AddAreaEvent("G1_11", "Hunting Grounds");
            AddAreaEvent("G1_12", "Freythorn");
            AddAreaEvent("G1_13_1", "Ogham Farmlands");
            var kingInTheMists = AddBossEvent("King in the Mists",
                new List<string> 
                {
                    "The King in the Mists: Why do you hate us for wanting to exist?",
                    "The King in the Mists: So long as you know me, I will always exist...",
                    "The King in the Mists: This is not the end..."
                });
            AddAreaEvent("G1_13_2", "Ogham Village", kingInTheMists);
            AddAreaEvent("G1_14", "Manor Ramparts");
            AddAreaEvent("G1_15", "Ogham Manor");
            AddBossEvent("Count Geonor", "The Hooded One: Allow me to clear your mind, if only for a moment.");

            AddAreaEvent("G2_1", "Vastiri Outskirts");
            AddAreaEvent("G2_town", "Arduna Caravan");
            AddAreaEvent("G2_10_1", "Mawdun Quarry");
            AddAreaEvent("G2_10_2", "Mawdun Mine");
            AddAreaEvent("G2_2", "Traitor’s Passage");
            AddAreaEvent("G2_3", "Halani Gates");
            AddAreaEvent("G2_4_1", "Keth");
            AddAreaEvent("G2_4_2", "Lost City");
            AddAreaEvent("G2_4_3", "Buried Shrines");
            AddAreaEvent("G2_5_1", "Mastodon Badlands");
            AddAreaEvent("G2_5_2", "Bone Pits");
            AddAreaEvent("G2_6", "Valley of the Titans");
            AddAreaEvent("G2_7", "Titan Grotto");
            AddAreaEvent("G2_8", "Deshar");
            AddAreaEvent("G2_9_1", "Path of Mourning");
            AddAreaEvent("G2_9_2", "Spires of Deshar");
            AddAreaEvent("G2_12_1", "Dreadnought");
            AddAreaEvent("G2_12_2", "Dreadnought Vanguard");
            AddBossEvent("Jamanra", "Jamanra, the Abomination: You have accomplished... Nothing. Oriana will prevail... And the Faridun will rule the Vastiri.");

            AddAreaEvent("G3_1", "Sandswept Marsh");
            AddAreaEvent("G3_town", "Ziggurat Encampment");
            AddAreaEvent("G3_3", "Jungle Ruins");
            AddAreaEvent("G3_4", "Venom Crypts");
            AddAreaEvent("G3_2_1", "Infested Barrens");
            AddAreaEvent("G3_7", "Azak Bog");
            AddAreaEvent("G3_5", "Chimeral Wetlands");
            AddAreaEvent("G3_6_1", "Jiquani's Machinarium");
            AddAreaEvent("G3_6_2", "Jiquani's Sanctum");
            AddAreaEvent("G3_2_2", "Matlan Waterways");
            AddAreaEvent("G3_8", "Drowned City");
            AddAreaEvent("G3_9", "Molten Vault");
            AddAreaEvent("G3_11", "Apex of Filth");
            AddAreaEvent("G3_12", "Temple of Kopec");
            AddAreaEvent("G3_14", "Utzaal");
            AddAreaEvent("G3_16", "Aggorat");
            AddAreaEvent("G3_17", "Black Chambers");
            AddBossEvent("Doryani", "Doryani: Gah! No! Do not fail me... Not now!");

            AddAreaEvent("G4_town", "Kingsmarch");
            AddAreaEvent("G4_1_1", "Isle of Kin");
            AddAreaEvent("G4_1_2", "Volcanic Warrens");
            AddAreaEvent("G4_2_1", "Kedge Bay");
            AddAreaEvent("G4_2_2", "Journey's End");
            AddBossEvent("Captain Hartlin", "Captain Hartlin: Do not... believe his lies...");
            AddAreaEvent("G4_3_1", "Whakapanu Island");
            AddAreaEvent("G4_3_2", "Singing Caverns");
            AddBossEvent("Diamora", "Diamora, the Song of Death: We could have been... so much more.");
            AddAreaEvent("G4_4_1", "Eye of Hinekora");
            AddAreaEvent("G4_4_2", "Halls of the Dead");
            AddBossEvent("Yama the White", "Yama The White: Ahhh! Okay, okay. Yama sees you. You are worthy.");
            AddAreaEvent("G4_4_3", "Trial of the Ancestors");
            AddAreaEvent("G4_5_1", "Abandoned Prison");
            AddAreaEvent("G4_5_2", "Solitary Confinement");
            AddBossEvent("The Prisoner", "The Prisoner: You cannot wound23 me... I cannot die...");
            AddAreaEvent("G4_7", "Shrike Island");
            AddBossEvent("Arastas", "Missionary Lorandis: What!? No! Stay back! Saviour... help me!");
            AddBossEvent("Torvian", "Cnaeus, of the Order: The Saviour...");
            AddAreaEvent("G4_10", "The Excavation");
            AddBossEvent("Benedictus", "Benedictus, First Herald of Utopia: My lady... you must take your leave! I will bury these fools... I will bury it all! For the Saviourrr! ");
            AddAreaEvent("G4_11_1b", "Ngakanu");
            AddAreaEvent("G4_11_2", "Heart of the Tribe");
            AddBossEvent("Tavakai", "Tavakai: What... what have I done?");
        }

        private static AreaEvent AddAreaEvent(string id, string name, params SplitEvent[] requiredEvents)
        {
            var areaEvent = new AreaEvent(name, id, requiredEvents);
            Events.Add(areaEvent);
            EventByName[name] = areaEvent;
            AreaEventById[id] = areaEvent;
            return areaEvent;
        }

        private static BossEvent AddBossEvent(string name, string voiceLine, params SplitEvent[] requiredEvents)
        {
            return AddBossEvent(name, new List<string> { voiceLine }, requiredEvents);
        }

        private static BossEvent AddBossEvent(string name, List<string> voiceLines, params SplitEvent[] requiredEvents)
        {
            var bossEvent = new BossEvent(name, voiceLines, requiredEvents);
            Events.Add(bossEvent);
            EventByName[name] = bossEvent;
            VoiceLinesByBossEvent[bossEvent] = voiceLines;
            return bossEvent;
        }

        public static bool TryGetByName(string name, out SplitEvent splitEvent)
        {
            if (EventByName.TryGetValue(name, out splitEvent))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetByLine(string line, out SplitEvent splitEvent)
        {
            var match = GeneratingAreaRegex.Match(line);
            if (match.Success)
            {
                var groups = match.Groups;
                var areaId = groups[2].Value;
                if (AreaEventById.TryGetValue(areaId, out var areaEvent))
                {
                    splitEvent = areaEvent;
                    return true;
                }
            }
            else foreach (var pair in VoiceLinesByBossEvent)
            {
                var bossEvent = pair.Key;
                var voiceLines = pair.Value;
                foreach (var voiceLine in voiceLines)
                {
                    if (line.Contains(voiceLine))
                    {
                        splitEvent = bossEvent;
                        return true;
                    }
                }
            }

            splitEvent = null;
            return false;
        }

        public static void ResetAll(object sender, EventArgs e)
        {
            foreach (var ev in Events)
            {
                ev.Reset();
            }
        }

        public string Name { get; }
        public bool IsEnabled { get; set; }

        private readonly List<SplitEvent> _requiredEvents = new List<SplitEvent>();
        private bool _didSplit;

        public SplitEvent(string name, params SplitEvent[] requiredEvents)
        {
            Name = name;
            _requiredEvents.AddRange(requiredEvents);
        }

        public bool CanSplit()
        {
            if (_didSplit)
                return false;

            foreach (var requiredEvent in _requiredEvents)
            {
                if (requiredEvent.IsEnabled && !requiredEvent._didSplit)
                    return false;
            }

            return true;
        }

        public void Split()
        {
            _didSplit = true;
        }

        public void Reset()
        {
            _didSplit = false;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var ev = obj as SplitEvent;
            return ev != null && Name.Equals(ev.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
