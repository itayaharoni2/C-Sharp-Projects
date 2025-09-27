using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class VersionItemListener : ISelectedListener
    {
        public void NotifySelected(MenuItem i_SelectedMenuItem)
        {
            TestOperations.PrintVersion();
        }
    }
}
