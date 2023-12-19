using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApplicationScenarios;

namespace Praktika
{
    public partial class MainWindow : Window
    {
        string currentTable = "Users";
        bool isAuthorizedAsAdmin = false;
        public MainWindow()
        {
            InitializeComponent();
            isAuthorizedAsAdmin = false;

            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    ApplicationTestScenarios.GenerateRandomData.RunAsync();
                }
                FillDataGrid(context);
            }
            logoutButton.Visibility = Visibility.Hidden;
            loggedInUserTextBlock.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Visible;
        }

        private void FillDataGrid(PDbContextData context)
        {
            ApplicationScenarios.GetData getData = new ApplicationScenarios.GetData(context);
            var data = getData.GetAllData();
            Console.WriteLine(data);

            foreach (var table in data)
            {
                var tableName = table.Key;
                var tableValues = table.Value;
                var firstValue = tableValues[0];
                var columnNames = firstValue.GetType().GetProperties().Select(p => p.Name).ToList();
                TabItem tabItem = new TabItem();
                tabItem.Header = tableName;

                DataGrid dataGrid = new DataGrid();
                dataGrid.Name = "dataGrid_" + tableName;
                Console.WriteLine(dataGrid.Name);
                if (isAuthorizedAsAdmin)
                {
                    dataGrid.CellEditEnding += (sender, e) => DataGrid_CellEditEnding(sender, e, tableName);
                }
                dataGrid.HorizontalAlignment = HorizontalAlignment.Left;
                dataGrid.Margin = new Thickness(10, 10, 0, 0);
                dataGrid.VerticalAlignment = VerticalAlignment.Top;
                dataGrid.Height = 430;
                dataGrid.Width = 780;
                StackPanel buttonPanel = new StackPanel();
                buttonPanel.Orientation = Orientation.Horizontal;
                tabItem.Content = new StackPanel
                {
                    Children =
                        {
                            buttonPanel,
                            dataGrid
                        }
                };
                tabControl.Items.Add(tabItem);
                dataGrid.ItemsSource = tableValues;
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e, string tableName)
        {
            var dataGrid = (DataGrid)sender;
            var rowView = dataGrid.SelectedItem;
            var row = rowView.GetType().GetProperties().Select(p => p.GetValue(rowView, null)).ToList();
            var columnNames = rowView.GetType().GetProperties().Select(p => p.Name).ToList();
            var columnName = columnNames[e.Column.DisplayIndex];
            var newValue = ((TextBox)e.EditingElement).Text;
            if (row.Contains(null))
            {
                return;
            }
            var id = int.Parse(row[0].ToString());
            Console.WriteLine($"Row: {row}");
            Console.WriteLine($"Column: {columnName}");
            Console.WriteLine($"New value: {newValue}");
            Console.WriteLine($"Id: {id}");
            var result = ChangeAddValue(id, row, columnName, newValue);
            if (result)
            {
                MessageBox.Show("Row changed successfully!");
            }
            else
            {
                MessageBox.Show("Can't change this row!");
            }
        }

        private bool ChangeAddValue(int id, List<object> row, string columnName, string newValue)
        {
            using (var context = new PDbContextData())
            {
                try
                {
                    switch (currentTable)
                    {
                        case "Users":
                            var user = context.Users.Find(id);
                            if (user == null)
                            {
                                if (row[1] == null || row[2] == null)
                                {
                                    return false;
                                }
                                context.Add(new User
                                {
                                    Login = row[1].ToString(),
                                    Password = AppLogin.HashPassword(row[2].ToString())
                                });
                            }
                            else
                            {
                                switch (columnName)
                                {
                                    case "Login":
                                        user.Login = newValue;
                                        break;
                                    case "Password":
                                        user.Password = AppLogin.HashPassword(newValue);
                                        break;
                                }
                            }
                            context.SaveChanges();
                            break;
                        case "Products":
                            var product = context.Products.Find(id);
                            if (product == null)
                            {
                                if (row[1] == null)
                                {
                                    return false;
                                }
                                context.Add(new Product
                                {
                                    Name = row[1].ToString()
                                });
                            }
                            else
                            {
                                switch (columnName)
                                {
                                    case "Name":
                                        product.Name = newValue;
                                        break;
                                }
                            }
                            context.SaveChanges();
                            break;
                        case "Stashes":
                            var stash = context.Stashes.Find(id);
                            if (stash == null)
                            {
                                try
                                {
                                    if (row[1] == null || row[2] == null || row[3] == null)
                                    {
                                        return false;
                                    }
                                    context.Add(new Stash
                                    {
                                        Product = context.Products.Find(int.Parse(row[1].ToString())),
                                        TimeLastCheck = DateTime.Parse(row[2].ToString()),
                                        BuyPrice = (decimal)double.Parse(row[3].ToString())
                                    });
                                }
                                catch (System.Exception)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                switch (columnName)
                                {
                                    case "ProductId":
                                        stash.Product = context.Products.Find(int.Parse(newValue));
                                        break;
                                    case "TimeLastCheck":
                                        stash.TimeLastCheck = DateTime.Parse(newValue);
                                        break;
                                    case "BuyPrice":
                                        stash.BuyPrice = (decimal)double.Parse(newValue);
                                        break;
                                }
                            }
                            context.SaveChanges();
                            break;
                        case "TransactionHistories":
                            var transactionHistory = context.TransactionHistories.Find(id);
                            if (transactionHistory == null)
                            {
                                if (row[1] == null)
                                {
                                    return false;
                                }
                                context.Add(new TransactionHistory
                                {
                                    SpendMoney = double.Parse(row[1].ToString()),
                                    BalanceAfterTransaction = double.Parse(row[2].ToString()),
                                    TimeOfTransaction = DateTime.Parse(row[3].ToString())
                                });
                            }
                            else
                            {
                                switch (columnName)
                                {
                                    case "SpendMoney":
                                        transactionHistory.SpendMoney = double.Parse(newValue);
                                        break;
                                    case "BalanceAfterTransaction":
                                        transactionHistory.BalanceAfterTransaction = double.Parse(newValue);
                                        break;
                                    case "TimeOfTransaction":
                                        transactionHistory.TimeOfTransaction = DateTime.Parse(newValue);
                                        break;
                                }
                            }
                            context.SaveChanges();
                            break;
                    }
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = (TabItem)tabControl.SelectedItem;
            currentTable = selectedTab.Header.ToString();
        }

        public MainWindow(User user)
        {
            InitializeComponent();
            if (user.Login == "admin")
            {
                isAuthorizedAsAdmin = true;
                CommandBinding closeCommand = new CommandBinding(ApplicationCommands.Close, DeleteExecuted);
                this.CommandBindings.Add(closeCommand);
            }
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    ApplicationTestScenarios.GenerateRandomData.RunAsync();
                }
                FillDataGrid(context);
            }
            loginButton.Visibility = Visibility.Hidden;
            loggedInUserTextBlock.Visibility = Visibility.Visible;
            logoutButton.Visibility = Visibility.Visible;

            loggedInUserTextBlock.Text = $"Logged in as {user.Login}";
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private bool DeleteRow(int rowId)
        {
            using (var context = new PDbContextData())
                try
                {
                    {
                        switch (currentTable)
                        {
                            case "Users":
                                var user = context.Users.Find(rowId);
                                context.Users.Remove(user);
                                break;
                            case "Products":
                                var product = context.Products.Find(rowId);
                                context.Products.Remove(product);
                                break;
                            case "Stashes":
                                var stash = context.Stashes.Find(rowId);
                                context.Stashes.Remove(stash);
                                break;
                            case "TransactionHistories":
                                var transactionHistory = context.TransactionHistories.Find(rowId);
                                context.TransactionHistories.Remove(transactionHistory);
                                break;
                        }
                        context.SaveChanges();
                        return true;
                    }
                }
                catch (System.Exception)
                {
                    return false;
                }
        }

        private void DeleteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Console.WriteLine("Delete");
            var selectedTab = (TabItem)tabControl.SelectedItem;
            var dataGrid = (DataGrid)((StackPanel)selectedTab.Content).Children[1];
            var rowView = dataGrid.SelectedItem;
            var rowId = int.Parse(rowView.GetType().GetProperty("Id").GetValue(rowView, null).ToString());
            var result = DeleteRow(rowId);
            if (result)
            {
                MessageBox.Show("Row deleted successfully!");
            }
            else
            {
                MessageBox.Show("Can't delete this row!");
            }
        }

        public void TestAdd()
        {
            var lastUserId = 0;
            var lastProductId = 0;
            var lastStashId = 0;
            var lastTransactionHistoryId = 0;
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                lastUserId = context.Users.OrderBy(u => u.Id).Last().Id;
                lastProductId = context.Products.OrderBy(p => p.Id).Last().Id;
                lastStashId = context.Stashes.OrderBy(s => s.Id).Last().Id;
                lastTransactionHistoryId = context.TransactionHistories.OrderBy(t => t.Id).Last().Id;
            }
            //
            // Тест добавления, которое должно пройти (User)
            //
            currentTable = "Users";
            var result = ChangeAddValue(lastUserId + 1, new List<object> { lastUserId + 1, "test", "test" }, "Id", (lastUserId + 1).ToString());
            if (!result)
            {
                MessageBox.Show("Can't add this row!");
                return;
            }
            //
            // Тест добавления, которое должно пройти (Product)
            //
            currentTable = "Products";
            result = ChangeAddValue(lastProductId + 1, new List<object> { lastProductId + 1, "test" }, "Id", (lastProductId + 1).ToString());
            if (!result)
            {
                MessageBox.Show("Can't add this row!");
                return;
            }
            //
            // Тест добавления, которое должно пройти (Stash)
            //
            currentTable = "Stashes";
            result = ChangeAddValue(lastStashId + 1, new List<object> { lastStashId + 1, 1, "2021-05-05", 100000 }, "Id", (lastStashId + 1).ToString());
            if (!result)
            {
                MessageBox.Show("Can't add this row!");
                return;
            }
            //
            // Тест добавления, которое должно пройти (TransactionHistory)
            //
            currentTable = "TransactionHistories";
            result = ChangeAddValue(lastTransactionHistoryId + 1, new List<object> { lastTransactionHistoryId + 1, 100000, 100000, "2021-05-05" }, "Id", (lastTransactionHistoryId + 1).ToString());
            if (!result)
            {
                MessageBox.Show("Can't add this row!");
                return;
            }
            MessageBox.Show("Test adding passed!");
        }
        public void TestChange()
        {
            //
            // Тест изменения, которое должно пройти (User)
            //
            currentTable = "Users";
            var result = ChangeAddValue(1, new List<object> { 1, "test", "test" }, "Login", "test");
            if (!result)
            {
                MessageBox.Show("Can't change this row!");
                return;
            }
            //
            // Тест изменения, которое должно пройти (Product)
            //
            currentTable = "Products";
            result = ChangeAddValue(1, new List<object> { 1, "test" }, "Name", "test");
            if (!result)
            {
                MessageBox.Show("Can't change this row!");
                return;
            }
            //
            // Тест изменения, которое должно пройти (Stash)
            //
            currentTable = "Stashes";
            result = ChangeAddValue(1, new List<object> { 1, 1, "2021-05-05", 100000 }, "BuyPrice", "100000");
            if (!result)
            {
                MessageBox.Show("Can't change this row!");
                return;
            }
            //
            // Тест изменения, которое должно пройти (TransactionHistory)
            //
            currentTable = "TransactionHistories";
            result = ChangeAddValue(1, new List<object> { 1, 100000, 100000, "2021-05-05" }, "SpendMoney", "100000");
            if (!result)
            {
                MessageBox.Show("Can't change this row!");
                return;
            }
            MessageBox.Show("Test changing passed!");
        }
        public void TestDelete()
        {
            var lastUserId = 0;
            var lastProductId = 0;
            var lastStashId = 0;
            var lastTransactionHistoryId = 0;
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                lastUserId = context.Users.OrderBy(u => u.Id).Last().Id;
                lastProductId = context.Products.OrderBy(p => p.Id).Last().Id;
                lastStashId = context.Stashes.OrderBy(s => s.Id).Last().Id;
                lastTransactionHistoryId = context.TransactionHistories.OrderBy(t => t.Id).Last().Id;
            }
            //
            // Тест удаления, которое должно пройти (User)
            //
            currentTable = "Users";
            var result = DeleteRow(lastUserId);
            if (!result)
            {
                MessageBox.Show("Can't delete this row!");
                return;
            }
            //
            // Тест удаления, которое должно пройти (Product)
            //
            currentTable = "TransactionHistories";
            result = DeleteRow(lastTransactionHistoryId);
            if (!result)
            {
                MessageBox.Show("Can't delete this row!");
                return;
            }
            //
            // Тест удаления, которое должно пройти (Stash)
            //
            currentTable = "Products";
            result = DeleteRow(lastProductId);
            if (!result)
            {
                MessageBox.Show("Can't delete this row!");
                return;
            }
            //
            // Тест удаления, которое должно пройти (TransactionHistory)
            //
            currentTable = "Stashes";
            result = DeleteRow(lastStashId);
            if (!result)
            {
                MessageBox.Show("Can't delete this row!");
                return;
            }
            MessageBox.Show("Test deleting passed!");

        }

        public void falseTests()
        {
            var lastUserId = 0;
            var lastProductId = 0;
            var lastStashId = 0;
            var lastTransactionHistoryId = 0;
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                lastUserId = context.Users.OrderBy(u => u.Id).Last().Id;
                lastProductId = context.Products.OrderBy(p => p.Id).Last().Id;
                lastStashId = context.Stashes.OrderBy(s => s.Id).Last().Id;
                lastTransactionHistoryId = context.TransactionHistories.OrderBy(t => t.Id).Last().Id;
            }
            //
            // Тест добавления, которое не должно пройти (User)
            //
            currentTable = "Users";
            var result = ChangeAddValue(lastUserId + 1, new List<object> { lastUserId + 1, null, null }, "Id", (lastUserId + 1).ToString());
            if (result)
            {
                MessageBox.Show("False test adding failed!");
                return;
            }
            //
            // Тест добавления, которое не должно пройти (Product)
            //
            currentTable = "Products";
            result = ChangeAddValue(lastProductId + 1, new List<object> { lastProductId + 1, null }, "Id", (lastProductId + 1).ToString());
            if (result)
            {
                MessageBox.Show("False test adding failed!");
                return;
            }
            //
            // Тест добавления, которое не должно пройти (Stash)
            //
            currentTable = "Stashes";
            result = ChangeAddValue(lastStashId + 1, new List<object> { lastStashId + 1, 1, "2021-05-05", null }, "Id", (lastStashId + 1).ToString());
            if (result)
            {
                MessageBox.Show("False test adding failed!");
                return;
            }
            //
            // Тест добавления, которое не должно пройти (TransactionHistory)
            //
            currentTable = "TransactionHistories";
            result = ChangeAddValue(lastTransactionHistoryId + 1, new List<object> { lastTransactionHistoryId + 1, null, 100000, "2021-05-05" }, "Id", (lastTransactionHistoryId + 1).ToString());
            if (result)
            {
                MessageBox.Show("False test adding failed!");
                return;
            }
            MessageBox.Show("False test adding passed!");
        }

        private void smallButton_Click(object sender, RoutedEventArgs e)
        {
            TestAdd();
            TestChange();
            TestDelete();
            falseTests();
        }
    }
}