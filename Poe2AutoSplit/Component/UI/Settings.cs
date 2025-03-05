using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.UI;
using Poe2AutoSplit.Component.AutoSplitter.Event;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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

        public Action OnLogPathChanged { get; set; }

        private readonly LiveSplitState _state;

        private const string DefaultLogPath = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile 2\logs\Client.txt";
        private const string EnabledKey = "Enabled";
        private const string LogPathKey = "LogPath";
        private const string SplitEventsKey = "SplitEvent";
        private const string EventKey = "Event";

        private bool _selectAllInProcess;

        public Settings(LiveSplitState state)
        {
            InitializeComponent();

            _state = state;
            SetDefaultSettings();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var settingsParent = document.CreateElement("Settings");

            SettingsHelper.CreateSetting(document, settingsParent, EnabledKey, IsEnabled);
            SettingsHelper.CreateSetting(document, settingsParent, LogPathKey, LogPath);

            var splitEventsElement = SettingsHelper.ToElement(document, SplitEventsKey, (string)null);
            foreach (var splitEvent in SplitEvent.AllEvents)
            {
                if (splitEvent.IsEnabled)
                {
                    SettingsHelper.CreateSetting(document, splitEventsElement, EventKey, splitEvent.ToString());
                }
            }

            settingsParent.AppendChild(splitEventsElement);

            return settingsParent;
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

                if (element[SplitEventsKey] != null)
                {
                    var splitEventsElement = element[SplitEventsKey];
                    foreach (XmlNode child in splitEventsElement.GetElementsByTagName(EventKey))
                    {
                        var name = child.InnerText;
                        if (SplitEvent.TryGetByName(name, out var splitEvent))
                        {
                            splitEvent.IsEnabled = true;
                        }
                    }
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

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            enabledCheckbox.Checked = IsEnabled;
            logPathTextbox.Text = LogPath;

            foreach (var splitEvent in SplitEvent.AllEvents)
            {
                checkedSplitEventList.Items.Add(splitEvent, splitEvent.IsEnabled);
            }

            UpdateSelectAllCheckboxState();
        }

        private void enabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            IsEnabled = enabledCheckbox.Checked;
        }

        private void logPathTextbox_TextChanged(object sender, EventArgs e)
        {
            LogPath = logPathTextbox.Text;
        }

        private void browseLogFileButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                logPathTextbox.Text = openFileDialog.FileName;
            }
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

            _state.Run.Clear();

            foreach (var splitEvent in SplitEvent.AllEvents)
            {
                if (splitEvent.IsEnabled)
                {
                    _state.Run.AddSegment(splitEvent.Name);
                }
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

        private void checkedSplitEventList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedSplitEventList.Items[e.Index] is SplitEvent selectedSplitEvent)
            {
                selectedSplitEvent.IsEnabled = e.NewValue == CheckState.Checked;
            }

            if (_selectAllInProcess)
                return;

            var change = e.NewValue == CheckState.Checked ? 1 : -1;
            UpdateSelectAllCheckboxState(change);
        }

        private void UpdateSelectAllCheckboxState(int change = 0)
        {
            if (checkedSplitEventList.CheckedItems.Count + change == checkedSplitEventList.Items.Count)
            {
                selectAllCheckbox.CheckState = CheckState.Checked;
            }
            else if (checkedSplitEventList.CheckedItems.Count + change == 0)
            {
                selectAllCheckbox.CheckState = CheckState.Unchecked;
            }
            else
            {
                selectAllCheckbox.CheckState = CheckState.Indeterminate;
            }
        }

        private void selectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _selectAllInProcess = true;

            if (selectAllCheckbox.CheckState != CheckState.Indeterminate)
            {
                for (var i = 0; i < checkedSplitEventList.Items.Count; i++)
                {
                    checkedSplitEventList.SetItemChecked(i, selectAllCheckbox.CheckState == CheckState.Checked);
                }
            }

            _selectAllInProcess = false;
        }

        private void selectAllCheckbox_Click(object sender, EventArgs e)
        {
            if (selectAllCheckbox.CheckState == CheckState.Indeterminate)
            {
                selectAllCheckbox.CheckState = CheckState.Unchecked;
            }
        }
    }
}
