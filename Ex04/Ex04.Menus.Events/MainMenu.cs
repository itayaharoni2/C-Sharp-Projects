using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly MenuItem r_MainMenuItem;
        private readonly Stack<MenuItem> r_MenuNavigationStack;

        public MainMenu()
        {
            r_MainMenuItem = new MenuItem("Delegates Main Menu");
            r_MenuNavigationStack = new Stack<MenuItem>();
        }

        public MenuItem MainMenuItem
        {
            get { return r_MainMenuItem; }
        }

        public void Show()
        {
            r_MenuNavigationStack.Push(r_MainMenuItem);
            while (r_MenuNavigationStack.Any())
            {
                MenuItem currentItem = r_MenuNavigationStack.Peek();
                int userChoice;

                Console.Clear();
                userChoice = getUserChoice();
                if (userChoice == 0)
                {
                    r_MenuNavigationStack.Pop();
                }
                else
                {
                    MenuItem selectedItem = currentItem.GetSubMenuItemByIndex(userChoice - 1);
                    bool itemHasSubMenu = selectedItem.SubMenuItemsCount != 0;

                    selectedItem.SelectedByUser();
                    if (itemHasSubMenu)
                    {
                        r_MenuNavigationStack.Push(selectedItem);
                    }
                }
            }
        }

        private int getUserChoice()
        {
            MenuItem currentMenuComponent = r_MenuNavigationStack.Peek();
            string userChoice = null;
            bool isValidChoice = false;

            printChoiceOptions(currentMenuComponent, r_MenuNavigationStack.Count);
            while (!isValidChoice)
            {
                userChoice = Console.ReadLine();
                isValidChoice = isStringANumberInRange(userChoice, 0, currentMenuComponent.SubMenuItemsCount);
                if (!isValidChoice)
                {
                    Console.Clear();
                    printChoiceOptions(currentMenuComponent, r_MenuNavigationStack.Count);
                    Console.WriteLine("Please Select A Valid Option");
                }
            }

            int userChoiceInt = int.Parse(userChoice);

            return userChoiceInt;
        }

        private bool isStringANumberInRange(string i_String, int i_MinValue, int i_MaxValue)
        {
            int numericValueOfString;
            bool isNumeric = int.TryParse(i_String, out numericValueOfString);

            return isNumeric && (numericValueOfString >= i_MinValue && numericValueOfString <= i_MaxValue);
        }

        private void printChoiceOptions(MenuItem i_MenuItem, int i_MenuItemNavigationDepth)
        {
            string zeroOptionText;
            bool isRootMenu = i_MenuItemNavigationDepth == 1;
            StringBuilder optionsMenuBuilder = new StringBuilder();

            optionsMenuBuilder.AppendLine(string.Format(@"**{0}**
--------------------------", i_MenuItem.Name));
            for (int i = 0; i < i_MenuItem.SubMenuItemsCount; i++)
            {
                optionsMenuBuilder.AppendLine(string.Format("{0,3} -> {1}", i + 1, i_MenuItem.GetSubMenuItemByIndex(i).Name));
            }

            if (isRootMenu)
            {
                zeroOptionText = "Exit";
            }
            else
            {
                zeroOptionText = "Back";
            }

            optionsMenuBuilder.Append(string.Format(@"  0 -> {0}
--------------------------
Enter your request: (1 to {1} or press '0' to {2}):", zeroOptionText, i_MenuItem.SubMenuItemsCount, zeroOptionText));
            Console.WriteLine(optionsMenuBuilder);
        }
    }
}
