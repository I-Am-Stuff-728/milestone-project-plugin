using System;
using System.Windows;
using System.Windows.Media.Imaging;
using VideoOS.Platform.Client;
using VideoOS.Platform.UI.Controls;

namespace buttonPluginTest.Client
{
    public class buttonPluginTestClientAction : ClientAction
    {
        public override Guid Id
        {
            get => buttonPluginTestDefinition.buttonPluginTestClientActionId;
        }

        public override string Name
        {
            get => "button that does nothing"; //Note that the action name should be localized (unless it contains a name of an Item or similar).
        }

        public override VideoOSIconSourceBase Icon
        {
            get => buttonPluginTestDefinition.PluginIcon;
        }

        public override void Init()
        {
            // TODO: remove below check when buttonPluginTestDefinition.buttonPluginTestClientActionId has been replaced with proper GUID
            if (Id == new Guid("55555555-5555-5555-5555-555555555550"))
            {
                System.Windows.MessageBox.Show("Default GUID has not been replaced for buttonPluginTestClientActionId!");
            }
        }

        public override void Close()
        {
        }

        public override void Activated()
        {
            MessageBox.Show("button that does nothing did nothing");
        }
    }
}