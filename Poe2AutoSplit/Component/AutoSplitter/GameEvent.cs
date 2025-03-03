using System.Collections.Generic;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class GameEvent
    {
        public string Name { get; }

        private static readonly Dictionary<string, List<string>> LineByEventName = new Dictionary<string, List<string>>
        {
            ["Lachlann"] = new List<string>()
            {
                "Lachlann of Endless Lament: Together... at last..."

            },

            ["King in the Mists"] = new List<string>
            {
                "The King in the Mists: Why do you hate us for wanting to exist?",
                "The King in the Mists: So long as you know me, I will always exist...",
                "The King in the Mists: This is not the end..."
            },

            ["Doryani"] = new List<string>
            {
                "Doryani: Gah! No! Do not fail me... Not now!"
            }
        };

        public GameEvent(string name)
        {
            Name = name;
        }

        public static bool TryParseFromName(string name, out GameEvent gameEvent)
        {
            if (LineByEventName.ContainsKey(name))
            {
                gameEvent = new GameEvent(name);
                return true;
            }

            gameEvent = null;
            return false;
        }

        public static bool TryParseFromLine(string line, out GameEvent gameEvent)
        {
            foreach (var pair in LineByEventName)
            {
                var name = pair.Key;
                var lines = pair.Value;
                foreach (var eventLine in lines)
                {
                    if (line.Contains(eventLine))
                    {
                        gameEvent = new GameEvent(name);
                        return true;
                    }
                }
            }

            gameEvent = null;
            return false;
        }

        public override bool Equals(object obj)
        {
            var gameEvent = obj as GameEvent;
            return gameEvent != null && Name.Equals(gameEvent.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
