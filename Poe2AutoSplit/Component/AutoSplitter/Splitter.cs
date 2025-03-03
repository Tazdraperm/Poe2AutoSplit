using System;
using System.Collections.Generic;
using LiveSplit.Model;
using LiveSplit.Options;

using Settings = Poe2AutoSplit.Component.UI.Settings;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class Splitter
    {
        private readonly ITimerModel _timer;
        private readonly Settings _settings;

        private readonly HashSet<Area> _encounteredAreas = new HashSet<Area>();
        private readonly HashSet<GameEvent> _encounteredEvents = new HashSet<GameEvent>();

        public Splitter(ITimerModel timer, Settings settings)
        {
            _timer = timer;
            _settings = settings;
            timer.CurrentState.OnStart += Reset;
        }

        public void OnAreaChanged(Area area)
        {
            Log.Info($"Area changed to: {area.Name}");

            if (!_settings.IsEnabled)
                return;

            if (_timer.CurrentState.CurrentPhase != TimerPhase.Running)
                return;

            if (!_settings.SplitAreas.Contains(area))
                return;

            if (_encounteredAreas.Contains(area))
                return;

            if (!Area.CanSplit(area, _encounteredAreas, _encounteredEvents))
                return;

            Log.Info("Perform split");
            _timer.Split();
            _encounteredAreas.Add(area);
        }

        public void OnGameEvent(GameEvent gameEvent)
        {
            Log.Info($"Game event triggered: {gameEvent.Name}");

            if (!_settings.IsEnabled)
                return;

            if (_timer.CurrentState.CurrentPhase != TimerPhase.Running)
                return;

            if (!_settings.SplitEvents.Contains(gameEvent))
                return;

            if (_encounteredEvents.Contains(gameEvent))
                return;

            Log.Info("Perform split");
            _timer.Split();
            _encounteredEvents.Add(gameEvent);
        }

        private void Reset(object sender, EventArgs e)
        {
            _encounteredAreas.Clear();
            _encounteredEvents.Clear();
        }
    }
}
