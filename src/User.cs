﻿using Auth0.OidcClient;
using System.Diagnostics;
namespace Desktop_Frontend
{
    internal class User : IUser
    {
        private Auth0Config config;
        private Auth0Client auth0Client;
        private bool loggedIn;
        private string username;
        private string myListsJSON;

        public User() : this(new Auth0Config()) { }

        public User(Auth0Config config)
        {
            this.config = config;
            username = "Auth0 User";
            myListsJSON = @"
            {
                ""lists"": [
                    {
                        ""id"": 0,
                        ""name"": ""Grocery List"",
                        ""ingredients"": [
                            {
                                ""name"": ""Apples"",
                                ""amount"": 5,
                                ""unit"": ""count""
                            },
                            {
                                ""name"": ""Milk"",
                                ""amount"": 1000,
                                ""unit"": ""ml""
                            }
                        ]
                    },
                    {
                        ""id"": 1,
                        ""name"": ""Pantry List"",
                        ""ingredients"": [
                            {
                                ""name"": ""Rice"",
                                ""amount"": 200,
                                ""unit"": ""g""
                            }
                        ]
                    }
                ]
            }";


            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = config.Domain,
                ClientId = config.ClientId
            };

            auth0Client = new Auth0Client(clientOptions);

            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;

        }

        // Async login method
        public async Task Login()
        {
            try
            {
                var loginResult = await auth0Client.LoginAsync();
     
                loggedIn = !loginResult.IsError;

            }
            catch (Exception e)
            {
                loggedIn = false;
            }
        }

        // Async logout method 
        public async Task Logout()
        {
            try
            {
                await auth0Client.LogoutAsync();
                loggedIn = false;
            }
            catch (Exception e)
            {
                loggedIn = true;
            }
        }

        private bool ConfigValid()
        {
            return config != null && config.ConfigValid;
        }

        public bool LoggedIn()
        {
            return loggedIn;
        }

        public string UserName()
        {
            return username;
        }

        public string GetLists()
        {
            return myListsJSON;
        }

    }
}
