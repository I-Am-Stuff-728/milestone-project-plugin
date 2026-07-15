using VideoOS.Platform.Client;

namespace buttonPluginTest.Client
{
    public class buttonPluginTestWorkSpaceViewItemManager : ViewItemManager
    {
        public buttonPluginTestWorkSpaceViewItemManager() : base("buttonPluginTestWorkSpaceViewItemManager")
        {
        }

        public ViewItemWpfUserControl viewThing;
        public override ViewItemWpfUserControl GenerateViewItemWpfUserControl()
        {
            viewThing = new buttonPluginTestWorkSpaceViewItemWpfUserControl();
            return viewThing;
        }
    }
}
