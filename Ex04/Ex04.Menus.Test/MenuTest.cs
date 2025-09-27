namespace Ex04.Menus.Test
{
    public class MenuTest
    {
        public static void RunTest()
        {
            Events.MainMenu eventsMenu = createDelegateMenu();
            Interfaces.MainMenu interfaceMenu = createInterfaceMenu();

            eventsMenu.Show();
            interfaceMenu.Show();
        }

        private static Events.MainMenu createDelegateMenu()
        {
            Events.MainMenu eventsMainMenu = new Events.MainMenu();
            Events.MenuItem versionAndCapitalsMenuItem = new Events.MenuItem("Version and Capitals");
            Events.MenuItem dateAndTimeMenuItem = new Events.MenuItem("Show Date/Time");
            Events.MenuItem timeMenuItem = new Events.MenuItem("Show Time");
            Events.MenuItem dateMenuItem = new Events.MenuItem("Show Date");
            Events.MenuItem versionMenuItem = new Events.MenuItem("Show Version");
            Events.MenuItem countCapitalsMenuItem = new Events.MenuItem("Count Capitals");

            eventsMainMenu.MainMenuItem.AddSubMenuItem(versionAndCapitalsMenuItem);
            eventsMainMenu.MainMenuItem.AddSubMenuItem(dateAndTimeMenuItem);
            dateAndTimeMenuItem.AddSubMenuItem(timeMenuItem);
            dateAndTimeMenuItem.AddSubMenuItem(dateMenuItem);
            versionAndCapitalsMenuItem.AddSubMenuItem(versionMenuItem);
            versionAndCapitalsMenuItem.AddSubMenuItem(countCapitalsMenuItem);
            dateMenuItem.ItemHasBeenChosen += printDate;
            timeMenuItem.ItemHasBeenChosen += printTime;
            versionMenuItem.ItemHasBeenChosen += printVersion;
            countCapitalsMenuItem.ItemHasBeenChosen += countCapitals;

            return eventsMainMenu;
        }

        private static Interfaces.MainMenu createInterfaceMenu()
        {
            Interfaces.MainMenu interfaceMainMenu = new Interfaces.MainMenu();
            Interfaces.MenuItem versionAndCapitalsMenuItem = new Interfaces.MenuItem("Version and Capitals");
            Interfaces.MenuItem dateAndTimeMenuItem = new Interfaces.MenuItem("Show Date/Time");
            Interfaces.MenuItem timeMenuItem = new Interfaces.MenuItem("Show Time");
            Interfaces.MenuItem dateMenuItem = new Interfaces.MenuItem("Show Date");
            Interfaces.MenuItem versionMenuItem = new Interfaces.MenuItem("Show Version");
            Interfaces.MenuItem countCapitalsMenuItem = new Interfaces.MenuItem("Count Capitals");

            interfaceMainMenu.MainMenuItem.AddSubMenuItem(versionAndCapitalsMenuItem);
            interfaceMainMenu.MainMenuItem.AddSubMenuItem(dateAndTimeMenuItem);
            dateAndTimeMenuItem.AddSubMenuItem(timeMenuItem);
            dateAndTimeMenuItem.AddSubMenuItem(dateMenuItem);
            versionAndCapitalsMenuItem.AddSubMenuItem(versionMenuItem);
            versionAndCapitalsMenuItem.AddSubMenuItem(countCapitalsMenuItem);
            dateMenuItem.AddSelectedListener(new ShowDateItemListener());
            timeMenuItem.AddSelectedListener(new ShowTimeItemListener());
            versionMenuItem.AddSelectedListener(new VersionItemListener());
            countCapitalsMenuItem.AddSelectedListener(new CountCapitalsItemListener());

            return interfaceMainMenu;
        }

        private static void printDate(Events.MenuItem i_Invoker)
        {
            TestOperations.PrintCurrentDate();
        }

        private static void printTime(Events.MenuItem i_Invoker)
        {
            TestOperations.PrintCurrentTime();
        }

        private static void printVersion(Events.MenuItem i_Invoker)
        {
            TestOperations.PrintVersion();
        }

        private static void countCapitals(Events.MenuItem i_Invoker)
        {
            TestOperations.CountCapitals();
        }
    }
}
