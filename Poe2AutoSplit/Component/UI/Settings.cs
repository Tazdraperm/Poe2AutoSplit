using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.UI;
using Poe2AutoSplit.Component.AutoSplitter;

namespace Poe2AutoSplit.Component.UI
{
    public partial class Settings: UserControl
    {
        public bool IsEnabled { get; set; }

        public string LogPath
        {
            get => _logPath;
            set
            {
                _logPath = value;
                OnLogPathChanged?.Invoke();
            }
        }
        private string _logPath;

        public string ConfigPath
        {
            get => _configPath;
            set
            {
                _configPath = value;
                LoadConfig();
            }
        }
        private string _configPath;

        private readonly LiveSplitState _state;

        public List<Area> SplitAreas = new List<Area>();
        public List<GameEvent> SplitEvents = new List<GameEvent>();

        public Action OnLogPathChanged { get; set; }

        private const string DefaultLogPath = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile 2\logs\Client.txt";

        private const string EnabledKey = "Enabled";
        private const string LogPathKey = "LogPath";
        private const string ConfigPathKey = "ConfigPath";

        private readonly List<string> _splitNames = new List<string>();

        public Settings(LiveSplitState state)
        {
            InitializeComponent();

            _state = state;
            SetDefaultSettings();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var settingsNode = document.CreateElement("Settings");

            SettingsHelper.CreateSetting(document, settingsNode, EnabledKey, IsEnabled);
            SettingsHelper.CreateSetting(document, settingsNode, LogPathKey, LogPath);
            SettingsHelper.CreateSetting(document, settingsNode, ConfigPathKey, ConfigPath);

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            SetDefaultSettings();

            try
            {
                var element = (XmlElement)settings;
                if (element[EnabledKey] != null)
                {
                    IsEnabled = bool.Parse(element[EnabledKey].InnerText);
                }

                if (element[LogPathKey] != null)
                {
                    LogPath = element[LogPathKey].InnerText;
                }

                if (element[ConfigPathKey] != null)
                {
                    ConfigPath = element[ConfigPathKey].InnerText;
                }

            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void SetDefaultSettings()
        {
            IsEnabled = true;
            LogPath = DefaultLogPath;
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigPath))
                return;

            SplitAreas.Clear();
            SplitEvents.Clear();
            _splitNames.Clear();

            var lines = File.ReadAllLines(ConfigPath);
            foreach (var line in lines)
            {
                if (Area.TryParseFromName(line, out var area))
                {
                    SplitAreas.Add(area);
                    _splitNames.Add(area.Name);
                    Log.Info($"Add area: {area.Name}");
                }
                else if (GameEvent.TryParseFromName(line, out var gameEvent))
                {
                    SplitEvents.Add(gameEvent);
                    _splitNames.Add(gameEvent.Name);
                    Log.Info($"Add event: {gameEvent.Name}");
                }
            }
        }

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            enabledCheckbox.Checked = IsEnabled;
            logPathTextbox.Text = LogPath;
            configPathTextbox.Text = ConfigPath;

            LoadConfig();
        }

        private void enabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            IsEnabled = enabledCheckbox.Checked;
        }

        private void logPathTextbox_TextChanged(object sender, EventArgs e)
        {
            LogPath = logPathTextbox.Text;
        }

        private void configPathTextbox_TextChanged(object sender, EventArgs e)
        {
            ConfigPath = configPathTextbox.Text;
            LoadConfig();
        }

        private void browseLogFileButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                logPathTextbox.Text = openFileDialog.FileName;
            }
        }

        private void browseConfigFileButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                configPathTextbox.Text = openFileDialog.FileName;
            }
        }

        private void reloadConfigButton_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void generateSplitsButton_Click(object sender, EventArgs e)
        {
            if (_state.CurrentPhase != TimerPhase.NotRunning)
            {
                MessageBox.Show("Splits cannot be changed while the timer is running or has not been reset.",
                    "Generate Splits", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Your current split segments will be overwritten.\nAre you sure you want to proceed?",
                    "Confirm Generate Splits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            LoadConfig();

            _state.Run.Clear();
            foreach (var name in _splitNames)
            {
                _state.Run.AddSegment(name);
            }

            if (_state.Run.Count == 0)
            {
                _state.Run.AddSegment("");
            }

            _state.Run.HasChanged = true;
            _state.Form.Invalidate();

            MessageBox.Show("Splits generated successfully.",
                "Generate Splits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
