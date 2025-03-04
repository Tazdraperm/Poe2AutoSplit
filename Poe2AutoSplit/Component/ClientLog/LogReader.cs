using System;
using System.IO;
using System.Threading;
using LiveSplit.Options;
using Poe2AutoSplit.Component.AutoSplitter.Event;

using Settings = Poe2AutoSplit.Component.UI.Settings;
using Splitter = Poe2AutoSplit.Component.AutoSplitter.Splitter;

namespace Poe2AutoSplit.Component.ClientLog
{
    public class LogReader
    {
        private readonly Settings _settings;
        private readonly Splitter _splitter;
        private int _threadCount;

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
            if (SplitEvent.TryGetByLine(line, out var splitEvent))
            {
                _splitter.ProcessEvent(splitEvent);
            }
        }

        public void Stop()
        {
            _threadCount++;
        }
    }
}
