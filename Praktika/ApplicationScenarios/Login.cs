using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace ApplicationScenarios
{
    public class UserApp
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    public class AppLogin
    {
        public async Task<User?> GetUser(string login, string password)
        {
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                var user = await context.Users.OfType<User>().SingleOrDefaultAsync(u => u.Login == login);
                if (user == null)
                {
                    MessageBox.Show("Wrong login or password!");
                    return null;
                }

                if (user != null && VerifyPassword(password, user.Password))
                {
                    MessageBox.Show("Logged in successfully!");
                    return user;
                }

                return null;
            }
        }

        public async Task<bool> CreateUser(User newUser, bool supressMessage)
        {
            newUser.Password = HashPassword(newUser.Password);
            using (var context = new PDbContextData())
            {
                // Check if the user with the same login already exists
                if (await context.Users.AnyAsync(u => u.Login == newUser.Login))
                {
                    MessageBox.Show("User with the same login already exists!");
                    return false;
                }

                // Add the new user to the database
                context.Users.Add(newUser);

                try
                {
                    // Save changes to the database
                    await context.SaveChangesAsync();
                    if (!supressMessage)
                        MessageBox.Show("User created successfully!");
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    // Handle exceptions (e.g., unique constraint violation)
                    MessageBox.Show($"Error creating user: {ex.Message}");
                    return false;
                }
            }
        }

        public static string HashPassword(string password)
        {
            // Convert the password string to a byte array
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Compute the hash value of the password
            byte[] hashBytes = SHA256.HashData(passwordBytes);

            // Convert the hash value to a hexadecimal string
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Implement your password verification logic here
            // This could involve comparing hashed passwords using a secure method
            // For simplicity, let's assume plain text comparison in this example
            string enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;
        }
    }
}
