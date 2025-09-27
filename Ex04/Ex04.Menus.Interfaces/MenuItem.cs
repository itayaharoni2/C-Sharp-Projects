using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly List<ISelectedListener> r_SelectedListeners = new List<ISelectedListener>();
        private string m_ItemName;
        private List<MenuItem> m_SubItems;

        public MenuItem(string i_Name)
        {
            m_ItemName = i_Name;
        }

        public string Name
        {
            get { return m_ItemName; }
            set { m_ItemName = value; }
        }

        public int SubMenuItemsCount
        {
            get
            {
                int numberOfItems;

                if (m_SubItems == null)
                {
                    numberOfItems = 0;
                }
                else
                {
                    numberOfItems = m_SubItems.Count;
                }

                return numberOfItems;
            }
        }

        public void AddSelectedListener(ISelectedListener i_ListenerToAdd)
        {
            r_SelectedListeners.Add(i_ListenerToAdd);
        }

        public void AddSubMenuItem(MenuItem i_MenuItemToAdd)
        {
            if (m_SubItems == null)
            {
                m_SubItems = new List<MenuItem>();
            }

            m_SubItems.Add(i_MenuItemToAdd);
        }

        public MenuItem GetSubMenuItemByIndex(int i_IndexOfSubMenuItem)
        {
            return m_SubItems[i_IndexOfSubMenuItem];
        }

        internal void SelectedByUser()
        {
            onItemHasBeenChosen();
        }

        private void onItemHasBeenChosen()
        {
            foreach (ISelectedListener listener in r_SelectedListeners)
            {
                listener.NotifySelected(this);
            }
        }
    }
}
