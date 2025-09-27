using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MenuItem
    {
        private string m_ItemName;
        private List<MenuItem> m_SubItems;
        public event Action<MenuItem> ItemHasBeenChosen;

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
            OnItemHasBeenChosen();
        }

        protected virtual void OnItemHasBeenChosen()
        {
            if (ItemHasBeenChosen != null)
            {
                ItemHasBeenChosen.Invoke(this);
            }
        }
    }
}
