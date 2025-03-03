using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using Poe2AutoSplit.Component.ClientLog;

using Settings = Poe2AutoSplit.Component.UI.Settings;
using Splitter = Poe2AutoSplit.Component.AutoSplitter.Splitter;

namespace Poe2AutoSplit.Component
{
    public class Poe2AutoSplitComponent : LogicComponent
    {
        public const string Name = "Path of Exile 2 AutoSplitter";
        public override string ComponentName => Name;

        private readonly Settings _settings;
        private readonly LogReader _reader;

        public Poe2AutoSplitComponent(LiveSplitState state)
        {
            var timer = new TimerModel
            {
                CurrentState = state
            };

            _settings = new Settings(state);

            var splitter = new Splitter(timer, _settings);
            _reader = new LogReader(_settings, splitter);
            _reader.Start();

            _settings.OnLogPathChanged = _reader.Start;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return _settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return _settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            _settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

        public override void Dispose()
        {
            _reader.Stop();
        }
    }
}
