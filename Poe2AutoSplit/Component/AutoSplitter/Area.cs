using System.Collections.Generic;
using System.Linq;
using LiveSplit.Options;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class Area
    {
        public string Name { get; }
        public string Id { get; }

        private static readonly Dictionary<string, Area> AreaById = new Dictionary<string, Area>();
        private static readonly Dictionary<string, Area> AreaByName = new Dictionary<string, Area>();

        private static readonly Dictionary<Area, Area> RequiredArea = new Dictionary<Area, Area>();
        private static readonly Dictionary<Area, GameEvent> RequiredEvent = new Dictionary<Area, GameEvent>();

        static Area()
        {
            AddArea("G1_town", "Clearfell Encampment");
            AddArea("G1_2", "Clearfell");
            AddArea("G1_3", "Mud Burrow");
            AddArea("G1_4", "Grelwood");
            var redVale = AddArea("G1_5", "Red Vale");
            AddArea("G1_6", "Grim Tangle", redVale);
            AddArea("G1_7", "Cemetery of the Eternals");
            AddArea("G1_8", "Mausoleum of the Praetor");
            AddArea("G1_9", "Tomb of the Consort");
            AddArea("G1_11", "Hunting Grounds");
            AddArea("G1_12", "Freythorn");
            AddArea("G1_13_1", "Ogham Farmlands");
            AddArea("G1_13_2", "Ogham Village", requiredEvent: new GameEvent("King in the Mists"));
            AddArea("G1_14", "Manor Ramparts");
            AddArea("G1_15", "Ogham Manor");

            AddArea("G2_1", "Vastiri Outskirts");
            AddArea("G2_town", "Arduna Caravan");
            AddArea("G2_10_1", "Mawdun Quarry");
            AddArea("G2_10_2", "Mawdun Mine");
            AddArea("G2_2", "Traitor’s Passage");
            AddArea("G2_3", "Halani Gates");
            AddArea("G2_4_1", "Keth");
            AddArea("G2_4_2", "Lost City");
            AddArea("G2_4_3", "Buried Shrines");
            AddArea("G2_5_1", "Mastodon Badlands");
            AddArea("G2_5_2", "Bone Pits");
            AddArea("G2_6", "Valley of the Titans");
            AddArea("G2_7", "Titan Grotto");
            AddArea("G2_8", "Deshar");
            AddArea("G2_9_1", "Path of Mourning");
            AddArea("G2_9_2", "Spires of Deshar");
            AddArea("G2_12_1", "Dreadnought");
            AddArea("G2_12_2", "Dreadnought Vanguard");

            AddArea("G3_1", "Sandswept Marsh");
            AddArea("G3_town", "Ziggurat Encampment");
            AddArea("G3_3", "Jungle Ruins");
            AddArea("G3_4", "Venom Crypts");
            AddArea("G3_2_1", "Infested Barrens");
            AddArea("G3_7", "Azak Bog");
            AddArea("G3_5", "Chimeral Wetlands");
            AddArea("G3_6_1", "Jiquani's Machinarium");
            AddArea("G3_6_2", "Jiquani's Sanctum");
            AddArea("G3_2_2", "Matlan Waterways");
            AddArea("G3_8", "Drowned City");
            AddArea("G3_9", "Molten Vault");
            AddArea("G3_11", "Apex of Filth");
            AddArea("G3_12", "Temple of Kopec");
            AddArea("G3_14", "Utzaal");
            AddArea("G3_16", "Aggorat");
            AddArea("G3_17", "Black Chambers");

            GameEvent.TryParseFromName("King in the Mists", out var ev);
            Log.Error(ev.Name);
        }

        public Area(string name, string id)
        {
            Name = name;
            Id = id;
        }

        private static Area AddArea(string id, string name, Area requiredArea=null, GameEvent requiredEvent=null)
        {
            var area = new Area(name, id);
            AreaById[id] = area;
            AreaByName[name] = area;

            if (requiredArea != null)
            {
                RequiredArea[area] = requiredArea;
            }

            if (requiredEvent != null)
            {
                RequiredEvent[area] = requiredEvent;
            }

            return area;
        }

        public static bool TryParseFromId(string id, out Area area)
        {
            if (AreaById.TryGetValue(id, out var existingArea))
            {
                area = existingArea;
                return true;
            }

            area = null;
            return false;
        }

        public static bool TryParseFromName(string name, out Area area)
        {
            if (AreaByName.TryGetValue(name, out var existingArea))
            {
                area = existingArea;
                return true;
            }

            area = null;
            return false;
        }

        public static bool CanSplit(Area area, HashSet<Area> encounteredAreas, HashSet<GameEvent> encounteredEvents)
        {
            return (!RequiredArea.TryGetValue(area, out var requiredArea) || encounteredAreas.Contains(requiredArea)) &&
                   (!RequiredEvent.TryGetValue(area, out var requiredEvent) || encounteredEvents.Contains(requiredEvent));
        }

        public override bool Equals(object obj)
        {
            var area = obj as Area;
            return area != null && Id.Equals(area.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
