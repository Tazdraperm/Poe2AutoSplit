using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using Poe2AutoSplit.Component;

[assembly: ComponentFactory(typeof(Poe2AutoSplitComponentFactory))]

namespace Poe2AutoSplit.Component
{
    public class Poe2AutoSplitComponentFactory : IComponentFactory
    {
        public string ComponentName => Poe2AutoSplitComponent.Name;
        public string Description => "Auto Splitter for Path of Exile 2";
        public string UpdateName => ComponentName;
        public ComponentCategory Category => ComponentCategory.Other;

        public Version Version => Version.Parse("0.2.0");
        public string UpdateURL => "";
        public string XMLURL => UpdateURL + "";

        public IComponent Create(LiveSplitState state)
        {
            return new Poe2AutoSplitComponent(state);
        }
    }
}
