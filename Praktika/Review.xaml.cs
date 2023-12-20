using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для Review.xaml
    /// </summary>
    public partial class Review : Window
    {
        public Review()
        {
            InitializeComponent();
        }

        private void review_Click(object sender, RoutedEventArgs e)
        {
            name_textBox.Text = null;
            review_richTextBox_run.Text = null;

            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
