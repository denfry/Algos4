using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Algos4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBuildTree_Click(object sender, RoutedEventArgs e)
        {
            TreeView.Items.Clear();

            TreeViewItem root = new TreeViewItem { Header = "0" };

            TreeViewItem node1 = new TreeViewItem { Header = "1" };
            TreeViewItem node2 = new TreeViewItem { Header = "2" };
            root.Items.Add(node1);
            root.Items.Add(node2);

            node1.Items.Add(new TreeViewItem { Header = "4" });
            TreeViewItem node3 = new TreeViewItem { Header = "3" };
            node1.Items.Add(node3);
            TreeViewItem node7 = new TreeViewItem { Header = "7" };
            node3.Items.Add(node7);
            node7.Items.Add(new TreeViewItem { Header = "6" });

            node2.Items.Add(new TreeViewItem { Header = "8" });
            node2.Items.Add(new TreeViewItem { Header = "5" });
            node2.Items.Add(new TreeViewItem { Header = "9" });

            AttachCollapsedEvent(root);

            TreeView.Items.Add(root);
        }

        private void AttachCollapsedEvent(TreeViewItem item)
        {
            item.Collapsed += TreeViewItemCollapsed;
            foreach (var child in item.Items)
            {
                if (child is TreeViewItem childItem)
                    AttachCollapsedEvent(childItem);
            }
        }

        private void TreeViewItemCollapsed(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                txtMessages.Text += $"Свернуто: {item.Header}\n";
            }
        }

        private void btnShowSelected_Click(object sender, RoutedEventArgs e)
        {
            if (TreeView.SelectedItem is TreeViewItem selectedItem)
            {
                txtMessages.Text += $"Выбрано: {selectedItem.Header}\n";
            }
            else
            {
                txtMessages.Text += "Узел не выбран.\n";
            }
        }

        private void btnShowLeftSibling_Click(object sender, RoutedEventArgs e)
        {
            if (TreeView.SelectedItem is TreeViewItem selectedItem)
            {
                TreeViewItem parent = GetParent(selectedItem);
                if (parent != null)
                {
                    int index = parent.Items.IndexOf(selectedItem);
                    if (index > 0)
                    {
                        TreeViewItem leftSibling = parent.Items[index - 1] as TreeViewItem;
                        if (leftSibling != null)
                        {
                            txtMessages.Text += $"Левый брат: {leftSibling.Header}\n";
                        }
                        else
                        {
                            txtMessages.Text += "Левый брат отсутствует.\n";
                        }
                    }
                    else
                    {
                        txtMessages.Text += "Левый брат отсутствует.\n";
                    }
                }
                else
                {
                    txtMessages.Text += "Узел является корневым и не имеет братьев.\n";
                }
            }
            else
            {
                txtMessages.Text += "Узел не выбран.\n";
            }
        }

        private TreeViewItem GetParent(TreeViewItem child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is TreeViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }
    }
}