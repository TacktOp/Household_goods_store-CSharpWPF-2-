using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            CaptchaText.Visibility = Visibility.Hidden;
            CaptchaTextBox.Visibility = Visibility.Hidden;
        }
        private string CaptchaValue = null;

        private async void LoginButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            // If CaptchaValue is not null then check for captcha
            if (CaptchaValue != null && CaptchaValue != CaptchaTextBox.Text)
            {
                MessageBox.Show("Wrong captcha!");
                CreateCaptcha();
                // Make login button unpressable for 10 seconds
                LoginButton.IsEnabled = false;
                await Task.Delay(10000);
                LoginButton.IsEnabled = true;
                return;
            }
            string Login = LoginTextBox.Text;
            string Password = PasswordBox.Password;

            var appLogin = new ApplicationScenarios.AppLogin();
            var user = await appLogin.GetUser(Login, Password);
            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                CreateCaptcha();
                Console.WriteLine(CaptchaValue);
                CaptchaText.Visibility = Visibility.Visible;
                CaptchaTextBox.Visibility = Visibility.Visible;
            }
        }

        private void CreateCaptcha()
        {
            string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            string[] ar = allowchar.Split(a);
            string pwd = string.Empty;
            Random r = new();

            for (int i = 0; i < 6; i++)
            {
                string temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;
            }

            CaptchaText.Text = pwd;

            CaptchaValue = CaptchaText.Text;

        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string newLogin = LoginTextBox.Text;
            string newPassword = PasswordBox.Password;
            using (var context = new PDbContextData())
            {
                // Create newUser by class User from context
                var newUser = new User
                {
                    Login = newLogin,
                    Password = newPassword
                };
                // Use await with CreateUser
                var appLogin = new ApplicationScenarios.AppLogin();
                await appLogin.CreateUser(newUser, false);
            }
        }
    }

}
