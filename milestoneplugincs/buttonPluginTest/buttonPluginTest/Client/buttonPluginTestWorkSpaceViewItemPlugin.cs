using System;
using VideoOS.Platform.Client;
using VideoOS.Platform.UI.Controls;

namespace buttonPluginTest.Client
{
    public class buttonPluginTestWorkSpaceViewItemPlugin : ViewItemPlugin
    {
        public buttonPluginTestWorkSpaceViewItemPlugin()
        {
        }

        public override Guid Id
        {
            get { return buttonPluginTestDefinition.buttonPluginTestWorkSpaceViewItemPluginId; }
        }

        public override VideoOSIconSourceBase IconSource { get => buttonPluginTestDefinition.PluginIcon; protected set => base.IconSource = value; }

        public override string Name
        {
            get { return "WorkSpace Plugin View Item"; }
        }

        public override bool HideSetupItem
        {
            get
            {
                return false;
            }
        }
        public ViewItemManager itsMe;

        public override ViewItemManager GenerateViewItemManager()
        {   
            itsMe = new buttonPluginTestWorkSpaceViewItemManager();
       
            return itsMe;
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }


    }
}
