using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace buttonPluginTest.Client
{
    internal class buttonPluginTestWorkSpaceToolbarPluginInstance : WorkSpaceToolbarPluginInstance
    {
        private Item _window;

        public buttonPluginTestWorkSpaceToolbarPluginInstance()
        {
        }

        public override void Init(Item window)
        {
            _window = window;

            Title = "buttonPluginTest";
        }

        public override void Activate()
        {
            // Here you should put whatever action that should be executed when the toolbar button is pressed
        }

        public override void Close()
        {
        }

    }

    internal class buttonPluginTestWorkSpaceToolbarPlugin : WorkSpaceToolbarPlugin
    {
        public buttonPluginTestWorkSpaceToolbarPlugin()
        {
        }

        public override Guid Id
        {
            get { return buttonPluginTestDefinition.buttonPluginTestWorkSpaceToolbarPluginId; }
        }

        public override string Name
        {
            get { return "buttonPluginTest"; }
        }

        public override void Init()
        {
            // TODO: remove below check when buttonPluginTestDefinition.buttonPluginTestWorkSpaceToolbarPluginId has been replaced with proper GUID
            if (Id == new Guid("22222222-2222-2222-2222-222222222222"))
            {
                System.Windows.MessageBox.Show("Default GUID has not been replaced for buttonPluginTestWorkSpaceToolbarPluginId!");
            }

            WorkSpaceToolbarPlaceDefinition.WorkSpaceIds = new List<Guid>() { ClientControl.LiveBuildInWorkSpaceId, ClientControl.PlaybackBuildInWorkSpaceId, buttonPluginTestDefinition.buttonPluginTestWorkSpacePluginId };
            WorkSpaceToolbarPlaceDefinition.WorkSpaceStates = new List<WorkSpaceState>() { WorkSpaceState.Normal };
        }

        public override void Close()
        {
        }

        public override WorkSpaceToolbarPluginInstance GenerateWorkSpaceToolbarPluginInstance()
        {
            return new buttonPluginTestWorkSpaceToolbarPluginInstance();
        }
    }
}
