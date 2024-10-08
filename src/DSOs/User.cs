﻿using Auth0.OidcClient;
using Desktop_Frontend.Auth0;
namespace Desktop_Frontend.DSOs
{
    internal class User : IUser
    {
        private Auth0Config config;
        private Auth0Client auth0Client;
        private bool loggedIn;
        private string username;
        private string myListsJSON;
        private string accessToken;

        public User() : this(new Auth0Config()) { }

        public User(Auth0Config config)
        {
            this.config = config;
            username = "Auth0 User";
            accessToken = "";
            myListsJSON = "";


            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = config.Domain,
                ClientId = config.ClientId,
            };

            auth0Client = new Auth0Client(clientOptions);

            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;

        }

        // Async login method
        public async Task Login()
        {
            try
            {
                var loginResult = await auth0Client.LoginAsync(new
                {
                    audience = config.Audience
                });



                accessToken = loginResult.AccessToken;

                if (!loginResult.IsError && !string.IsNullOrEmpty(accessToken))
                {
                    loggedIn = true;
                }

            }
            catch (Exception)
            {
                accessToken = "";
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
            catch (Exception)
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

        public string GetAccessToken()
        {
            return accessToken;
        }

        public string GetLists()
        {
            return myListsJSON;
        }

    }
}
