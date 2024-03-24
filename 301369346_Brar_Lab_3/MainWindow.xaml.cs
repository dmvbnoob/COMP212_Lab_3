using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _301369346_Brar_Lab_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<MenuItems> selectedItems = new ObservableCollection<MenuItems>();
        private const double taxRate = 0.13;

        public MainWindow()
        {
            InitializeComponent();
            PopulateMenuItems();
            dgCart.ItemsSource = selectedItems;
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://dmbrar.netlify.app/");
        }

        private void PopulateMenuItems()
        {
            cmbBev.ItemsSource = new List<MenuItems>
            {
                new MenuItems { Name = "Soda", Price = 1.95, Quantity = 1 },
                new MenuItems { Name = "Tea", Price = 1.50, Quantity = 1 },
                new MenuItems { Name = "Coffee", Price = 1.25, Quantity = 1 },
                new MenuItems { Name = "Mineral Water", Price = 2.95, Quantity = 1 },
                new MenuItems { Name = "Juice", Price = 2.50, Quantity = 1 },
                new MenuItems { Name = "Milk", Price = 1.50, Quantity = 1 }
            };
            cmbBev.DisplayMemberPath = "Name";

            cmbApp.ItemsSource = new List<MenuItems>
            {
                new MenuItems { Name = "Buffalo Wings", Price = 5.95, Quantity = 1 },
                new MenuItems { Name = "Buffalo Fingers", Price = 6.95, Quantity = 1 },
                new MenuItems { Name = "Potato Skins", Price = 8.95, Quantity = 1 },
                new MenuItems { Name = "Nachos", Price = 8.95, Quantity = 1 },
                new MenuItems { Name = "Mushroom Caps", Price = 10.95, Quantity = 1 },
                new MenuItems { Name = "Shrimp Cocktail", Price = 12.95, Quantity = 1 },
                new MenuItems { Name = "Chips and Salsa", Price = 6.95, Quantity = 1 }
            };
            cmbApp.DisplayMemberPath = "Name";

            cmbMain.ItemsSource = new List<MenuItems>
            {
                new MenuItems { Name = "Seafood Alfredo", Price = 15.95, Quantity = 1 },
                new MenuItems { Name = "Chicken Alfredo", Price = 13.95, Quantity = 1 },
                new MenuItems { Name = "Chicken Picatta", Price = 13.95, Quantity = 1 },
                new MenuItems { Name = "Turkey Club", Price = 11.95, Quantity = 1 },
                new MenuItems { Name = "Lobster Pie", Price = 19.95, Quantity = 1 },
                new MenuItems { Name = "Prime Rib", Price = 20.95, Quantity = 1 },
                new MenuItems { Name = "Shrimp Scampi", Price = 18.95, Quantity = 1 },
                new MenuItems { Name = "Turkey Dinner", Price = 13.95, Quantity = 1 },
                new MenuItems { Name = "Stuffed Chicken", Price = 14.95, Quantity = 1 }
            };
            cmbMain.DisplayMemberPath = "Name";

            cmbDes.ItemsSource = new List<MenuItems>
            {
                new MenuItems { Name = "Apple Pie", Price = 5.95, Quantity = 1 },
                new MenuItems { Name = "Sundae", Price = 3.95, Quantity = 1 },
                new MenuItems { Name = "Carrot Cake", Price = 5.95, Quantity = 1 },
                new MenuItems { Name = "Mud Pie", Price = 4.95, Quantity = 1 },
                new MenuItems { Name = "Apple Crisp", Price = 5.95, Quantity = 1 }
            };
            cmbDes.DisplayMemberPath = "Name";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMenuItem(cmbBev, chkBev.IsChecked ?? false);
            AddMenuItem(cmbApp, chkApp.IsChecked ?? false);
            AddMenuItem(cmbMain, chkMain.IsChecked ?? false);
            AddMenuItem(cmbDes, chkDes.IsChecked ?? false);
            RefreshDG();
        }

        private void AddMenuItem(ComboBox cmb, bool isChecked)
        {
            if (isChecked && cmb.SelectedItem is MenuItems selectedItem)
            {
                var existingItem = selectedItems.FirstOrDefault(item => item.Name == selectedItem.Name);
                if (existingItem != null)
                {
                    existingItem.Quantity += 1;
                }
                else
                {
                    var newItem = new MenuItems { Name = selectedItem.Name, Price = selectedItem.Price, Quantity = 1 };
                    selectedItems.Add(newItem);
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            selectedItems.Clear();
            dgCart.ItemsSource = null;
            dgCart.ItemsSource = selectedItems;

            txtSub.Text = "$0.00";
            txtTax.Text = "$0.00";
            txtTotal.Text = "$0.00";

            cmbBev.SelectedIndex = -1;
            cmbApp.SelectedIndex = -1;
            cmbMain.SelectedIndex = -1;
            cmbDes.SelectedIndex = -1;

            chkBev.IsChecked = false;
            chkApp.IsChecked = false;
            chkMain.IsChecked = false;
            chkDes.IsChecked = false;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgCart.SelectedItem as MenuItems;
            if (selectedItem != null)
            {
                selectedItems.Remove(selectedItem);
            }
            UpdateComp();
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            UpdateComp();
        }

        private void RefreshDG()
        {
            dgCart.ItemsSource = null;
            dgCart.ItemsSource = selectedItems;
        }

        private void UpdateComp()
        {
            var subtotal = selectedItems.Sum(item => item.Price * item.Quantity);
            var tax = subtotal * taxRate;
            var total = subtotal + tax;

            txtSub.Text = subtotal.ToString("C2");
            txtTax.Text = tax.ToString("C2");
            txtTotal.Text = total.ToString("C2");
        }

    }
}
