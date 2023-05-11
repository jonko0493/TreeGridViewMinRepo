using Eto.Drawing;
using Eto.Forms;
using SerialLoops.Controls;
using System.Collections.Generic;

namespace TreeGridViewMinRepo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Title = "My Eto Form";
            MinimumSize = new Size(200, 200);

            List<Section> firstList = new()
            {
                new() { Text = "Hello 1" },
                new() { Text = "Hello 2" },
                new() { Text = "Hello 3" },
            };
            List<Section> secondList = new()
            {
                new() { Text = "World 1" },
                new() { Text = "World 2" },
                new() { Text = "World 3" },
            };

            List<Section> topSections = new()
            {
                new("Hellos", firstList, null),
                new("Worlds", secondList, null)
            };

            SectionListTreeGridView treeGridView = new(topSections, new(300, 300), true);

            Content = new StackLayout
            {
                Padding = 10,
                Items =
                {
                    treeGridView.Control
                }
            };

            // create a few commands that can be used for the menu and toolbar
            var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            var renameCommand = new Command { MenuText = "Rename Item", ToolBarText = "Rename Item", Shortcut = Keys.F2 };
            renameCommand.Executed += (sender, e) =>
            {
                treeGridView.SelectedItem.Text = "New Text";
                treeGridView.Control.Invalidate();
            };

            // create menu
            Menu = new MenuBar
            {
                Items =
                {
					// File submenu
					new SubMenuItem { Text = "&File", Items = { clickMe, renameCommand } },
					// new SubMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new SubMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
                ApplicationItems =
                {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            // create toolbar			
            ToolBar = new ToolBar { Items = { clickMe } };
        }
    }
}
