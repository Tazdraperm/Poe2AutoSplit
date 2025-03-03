using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using LiveSplit.Options;
using Poe2AutoSplit.Component.AutoSplitter;

using Settings = Poe2AutoSplit.Component.UI.Settings;
using Splitter = Poe2AutoSplit.Component.AutoSplitter.Splitter;

namespace Poe2AutoSplit.Component.ClientLog
{
    public class LogReader
    {
        private readonly Settings _settings;
        private readonly Splitter _splitter;
        private int _threadCount;

        private const string TimestampTemplate = @"^[^ ]+ [^ ]+ \d+";
        private static readonly Regex GeneratingAreaRegex = new Regex(TimestampTemplate + ".*Generating level (\\d+) area \"(.*)\"");

        public LogReader(Settings settings, Splitter splitter)
        {   
            _settings = settings;
            _splitter = splitter;
        }

        public void Start()
        {
            var thisThreadCount = ++_threadCount;
            var name = _settings.LogPath;

            if (!File.Exists(name))
                return;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                try
                {
                    using (var fs = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        fs.Seek(0, SeekOrigin.End);
                        using (var sr = new StreamReader(fs))
                        {
                            while (thisThreadCount == _threadCount)
                            {
                                while (!sr.EndOfStream)
                                {
                                    ProcessLine(sr.ReadLine());
                                }

                                while (sr.EndOfStream)
                                {
                                    Thread.Sleep(100);
                                }

                                ProcessLine(sr.ReadLine());
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }).Start();
        }

        public void ProcessLine(string line)
        {
            var match = GeneratingAreaRegex.Match(line);
            if (match.Success)
            {
                var groups = match.Groups;
                if (Area.TryParseFromId(groups[2].Value, out var area))
                {
                    _splitter.OnAreaChanged(area);
                }
            }
            else if (GameEvent.TryParseFromLine(line, out var gameEvent))
            {
                _splitter.OnGameEvent(gameEvent);
            }
        }

        public void Stop()
        {
            _threadCount++;
        }
    }
}
