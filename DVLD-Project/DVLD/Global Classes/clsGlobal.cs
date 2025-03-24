using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using Microsoft.Win32;

namespace DVLD.Global_Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                string FilePath = CurrentDirectory + "\\Data.txt";
                if (Username == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }
                string dataToSave = Username + "#//#" + Password;
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
        public static bool GetStoredCredential(ref string Username,ref string Password)
        {
            
            try
            {    
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string filePath = currentDirectory + "\\data.txt";

                if (File.Exists(filePath))
                {    
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }


        }

        public static bool GetStoredCredentialFromRegistry(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            string UsernameKey = "Username";
            string PasswordKey = "Password";
            try
            {
                Username = Registry.GetValue(keyPath, UsernameKey, null) as string;
                Password = Registry.GetValue(keyPath, PasswordKey, null) as string;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool RememberUsernameAndPasswordUsingRegistry(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            string UsernameKey = "Username";
            string PasswordKey = "Password";
            try
            {
                Registry.SetValue(keyPath, UsernameKey, Username, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordKey, Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        static string ComputeHash(string input)
        {
            //SHA is Secured Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
