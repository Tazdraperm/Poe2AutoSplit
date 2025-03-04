using LiveSplit.Model;
using LiveSplit.Options;
using Poe2AutoSplit.Component.AutoSplitter.Event;

using Settings = Poe2AutoSplit.Component.UI.Settings;

namespace Poe2AutoSplit.Component.AutoSplitter
{
    public class Splitter
    {
        private readonly ITimerModel _timer;
        private readonly Settings _settings;

        public Splitter(ITimerModel timer, Settings settings)
        {
            _timer = timer;
            _settings = settings;
            timer.CurrentState.OnStart += SplitEvent.ResetAll;
        }

        public void ProcessEvent(SplitEvent splitEvent)
        {
            Log.Info($"Split event encountered: {splitEvent.Name}");

            if (!_settings.IsEnabled)
                return;

            if (_timer.CurrentState.CurrentPhase != TimerPhase.Running)
                return;

            if (!splitEvent.CanSplit())
                return;

            Log.Info("Split!");
            splitEvent.Split();
            _timer.Split();
        }
    }
}
