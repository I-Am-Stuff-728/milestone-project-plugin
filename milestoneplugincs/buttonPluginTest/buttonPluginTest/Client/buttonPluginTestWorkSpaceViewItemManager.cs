using VideoOS.Platform.Client;

namespace buttonPluginTest.Client
{
    public class buttonPluginTestWorkSpaceViewItemManager : ViewItemManager
    {
        public buttonPluginTestWorkSpaceViewItemManager() : base("buttonPluginTestWorkSpaceViewItemManager")
        {
        }

        public override ViewItemWpfUserControl GenerateViewItemWpfUserControl()
        {
            return new buttonPluginTestWorkSpaceViewItemWpfUserControl();
        }
    }
}
