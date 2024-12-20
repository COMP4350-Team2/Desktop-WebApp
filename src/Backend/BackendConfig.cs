﻿using DotNetEnv;
using System.IO;
using System.Runtime.CompilerServices;

namespace Desktop_Frontend.Backend
{
    /// <summary>
    /// This class is responsible for reading the backend environment variables
    /// </summary>
    public class BackendConfig
    {
        public string? BackendUrl;
        public string? Create_User_Endpoint;
        public string? All_Ing_Endpoint;
        public string? Get_My_Lists_Endpoint;
        public string? Add_Ing_Endpoint;
        public string? Get_Measurements_Endpoint;
        public string? Rem_Ing_Endpoint;
        public string? Set_Ing_Endpoint;
        public string? Del_List_Endpoint;
        public string? Create_List_Endpoint;
        public string? Rename_List_Endpoint;
        public string? Move_Ing_Endpoint;
        public string? Create_Custom_Ing_Endpoint;
        public string? Delete_Custom_Ing_Endpoint;
        public string? Get_Recipes_Endpoint;
        public string? Create_Recipe_Endpoint;
        public string? Delete_Recipe_Endpoint;
        public string? Add_Ing_Recipe_Endppoint;
        public string? Delete_Ing_Recipe_Endpoint;
        public string? Add_Step_Recipe_Endppoint;
        public string? Delete_Step_Recipe_Endpoint;
        public string? Refresh_Token_Endpoint;

        /// <summary>
        /// bool to check if the backend configuration is valid
        /// </summary>
        public bool ConfigValid;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackendConfig"/> class
        /// and loads environment variables from the .env file.
        /// </summary>
        public BackendConfig()
        {
            LoadEnvVars();
            ConfigValid = ValidateEnvVars();
        }

        /// <summary>
        /// Reads the .env file and sets the instance variables accordingly
        /// </summary>
        private void LoadEnvVars()
        {

            string? sourceDirectory = GetSourceFileDirectory();

            if(sourceDirectory != null)
            {
                string envFilePath = Path.Combine(sourceDirectory, "BACKEND.env");

                if (File.Exists(envFilePath))
                {
                    using (var stream = File.OpenRead(envFilePath))
                    {
                        Env.Load(stream); 
                    }
                }

                BackendUrl = Env.GetString("BACKEND_URL");
                Create_User_Endpoint = Env.GetString("CREATE_USER");
                All_Ing_Endpoint = Env.GetString("ALL_INGREDIENTS");
                Get_My_Lists_Endpoint = Env.GetString("MY_LISTS");
                Add_Ing_Endpoint = Env.GetString("ADD_INGREDIENT");
                Get_Measurements_Endpoint = Env.GetString("GET_MEASUREMENTS");
                Rem_Ing_Endpoint = Env.GetString("REMOVE_INGREDIENT");
                Set_Ing_Endpoint = Env.GetString("SET_INGREDIENT");
                Del_List_Endpoint = Env.GetString("DELETE_LIST");
                Create_List_Endpoint = Env.GetString("CREATE_LIST");
                Rename_List_Endpoint = Env.GetString("RENAME_LIST");
                Move_Ing_Endpoint = Env.GetString("MOVE_INGREDIENT");
                Create_Custom_Ing_Endpoint = Env.GetString("CREATE_CUSTOM_ING");
                Delete_Custom_Ing_Endpoint = Env.GetString("DELETE_CUSTOM_ING");
                Get_Recipes_Endpoint = Env.GetString("GET_RECIPES");
                Create_Recipe_Endpoint = Env.GetString("CREATE_RECIPE");
                Delete_Recipe_Endpoint = Env.GetString("DELETE_RECIPE");
                Add_Ing_Recipe_Endppoint = Env.GetString("ADD_ING_RECIPE");
                Delete_Ing_Recipe_Endpoint = Env.GetString("DELETE_ING_RECIPE");
                Add_Step_Recipe_Endppoint = Env.GetString("ADD_STEP_RECIPE");
                Delete_Step_Recipe_Endpoint = Env.GetString("DELETE_STEP_RECIPE");
                Refresh_Token_Endpoint = Env.GetString("REFRESH-TOKEN");
            }

        }

        /// <summary>
        /// Checks if .env file is read correctly and variables assigned.
        /// </summary>
        /// <returns>Returns true if .env file read properly, false otherwise.</returns>
        private bool ValidateEnvVars()
        {
            return !(string.IsNullOrEmpty(BackendUrl) || string.IsNullOrEmpty(Create_User_Endpoint)
               || string.IsNullOrEmpty(All_Ing_Endpoint) || string.IsNullOrEmpty(Get_My_Lists_Endpoint)
               || string.IsNullOrEmpty(Add_Ing_Endpoint) || string.IsNullOrEmpty(Get_Measurements_Endpoint)
               || string.IsNullOrEmpty(Rem_Ing_Endpoint) || string.IsNullOrEmpty(Set_Ing_Endpoint)
               || string.IsNullOrEmpty(Del_List_Endpoint) || string.IsNullOrEmpty(Create_List_Endpoint) 
               || string.IsNullOrEmpty(Rename_List_Endpoint) || string.IsNullOrEmpty(Move_Ing_Endpoint)
               || string.IsNullOrEmpty(Create_Custom_Ing_Endpoint) || string.IsNullOrEmpty(Delete_Custom_Ing_Endpoint)
               || string.IsNullOrEmpty(Get_Recipes_Endpoint) || string.IsNullOrEmpty(Create_Recipe_Endpoint)
               || string.IsNullOrEmpty(Delete_Recipe_Endpoint) || string.IsNullOrEmpty(Add_Ing_Recipe_Endppoint)
               || string.IsNullOrEmpty(Delete_Ing_Recipe_Endpoint) || string.IsNullOrEmpty(Add_Step_Recipe_Endppoint)
               || string.IsNullOrEmpty(Delete_Step_Recipe_Endpoint) || string.IsNullOrEmpty(Refresh_Token_Endpoint));
        }

        /// <summary>
        /// Gets the directory of the source file where this method is called from.
        /// </summary>
        /// <param name="sourceFilePath">
        /// The full path of the source file calling this method. 
        /// This parameter is automatically provided by the compiler using the <see cref="CallerFilePathAttribute"/> and should not be explicitly passed.
        /// </param>
        /// <returns>The directory of the source file.</returns>
        private string? GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
        }
    }
}
