﻿using DotNetEnv;

namespace Desktop_Frontend
{
    public class Auth0Config
    {
        public string Domain;
        public string ClientId;
        public string CallbackUrl;
        public string ApiIdentifier;
        public bool ConfigValid;

        public Auth0Config()
        {
            LoadEnvVars();

            ConfigValid = ValidateEnvVars();

        }

        private void LoadEnvVars()
        {
            Env.TraversePath().Load();

            Domain = Env.GetString("AUTH0_DOMAIN");
            ClientId = Env.GetString("AUTH0_CLIENT_ID");
            CallbackUrl = Env.GetString("AUTH0_CALLBACK_URL");
            ApiIdentifier = Env.GetString("AUTH0_API_IDENTIFIER");
        }

        private bool ValidateEnvVars()
        {
            return !(string.IsNullOrEmpty(Domain) || string.IsNullOrEmpty(ClientId)
               || string.IsNullOrEmpty(CallbackUrl) || string.IsNullOrEmpty(ApiIdentifier));
       
        }
    }
}
