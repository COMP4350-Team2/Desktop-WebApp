﻿using Desktop_Frontend.DSOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows;

namespace Desktop_Frontend.Backend
{
    /// <summary>
    /// Concrete implenentation of the Backend
    /// </summary>  
    public class Backend : IBackend
    {
        private BackendConfig config;

        private HttpClient HttpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Backend"/> class with default configuration.
        /// </summary>
        public Backend() : this(new BackendConfig()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Backend"/> class with a specified configuration.
        /// </summary>
        /// <param name="config"><see cref="BackendConfig"/> for the concrete implementation.</param>
        public Backend(BackendConfig config)
        {
            this.config = config;

            HttpClient = new HttpClient();
        }

        /// <summary>
        /// Retrieves all available ingredients from the backend API.
        /// </summary>
        /// <param name="user">The user of type <see cref="IUser"/> who is making requests.</param>
        /// <returns>A list of <see cref="Ingredient"/> objects based on results obtained from the backend API.
        /// Shows a message box indicating failure. Empty list returned on failure.
        /// </returns>
        public async Task<List<Ingredient>> GetAllIngredients(IUser user)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            //Create request
            string url = config.BackendUrl + config.All_Ing_Endpoint;
            string accessToken = user.GetAccessToken();
            var request = new HttpRequestMessage(HttpMethod.Get, url); ;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                HttpResponseMessage response = await HttpClient.SendAsync(request);
                ValidateAllIngResponse(response);
                FillAllIngList(response, ingredients);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to fetch all ingredients");
            }


            return ingredients;
        }

        /// <summary>
        /// Creates a new user in the backend API. To be used on sign in. If user exists, no new user is created.
        /// </summary>
        /// <param name="user">The user of type <see cref="IUser"/> to create in the backend.</param>
        /// <returns>A boolean value indicating whether the user creation was successful. The user may already
        /// exist in the backend API but this is not considered a failure.
        /// </returns>
        public async Task<bool> CreateUser(IUser user)
        {
            bool success = false;

            //Create request
            string url = config.BackendUrl + config.Create_User_Endpoint;
            string accessToken = user.GetAccessToken();
            var request = new HttpRequestMessage(HttpMethod.Post, url);;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //Send request and get response
            try
            {
                HttpResponseMessage response = await HttpClient.SendAsync(request);

                success = await ValidateUserCreation(response);

            }
            catch (Exception)
            {
               success = false;
            }


            return success;
        }

        /// <summary>
        /// Validates the response from the backend API after a user creation attempt.
        /// </summary>
        /// <param name="response">The HTTP response to validate.</param>
        /// <returns>A boolean value indicating whether the response was valid.</returns>
        private async Task<bool> ValidateUserCreation(HttpResponseMessage response)
        {
            bool valid = false;

            if(response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Contains("Item created successfully") ||
                    responseBody.Contains("Item already exists."))
                {
                    valid = true;
                }
            }

            return valid;
        }

        /// <summary>
        /// Validates the response from the backend API when fetching all ingredients. Throws
        /// exception on failure.
        /// </summary>
        /// <param name="response">The HTTP response to validate.</param>
        private static void ValidateAllIngResponse(HttpResponseMessage response)
        {
           if (!response.IsSuccessStatusCode)
           {
                throw new Exception();
           }
        }

        /// <summary>
        /// Fills the list of <see cref="Ingredient"/> objects with data from the backend API response.
        /// </summary>
        /// <param name="response">The HTTP response containing the ingredient data.</param>
        /// <param name="allIng">The list to populate with <see cref="Ingredient"/> objects.</param>
        private async void FillAllIngList(HttpResponseMessage response, List<Ingredient> allIng)
        {
            // Read the response content as a string
            string body = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            var jsonBody = JsonDocument.Parse(body);

            // Check if the root element contains "result"
            if (jsonBody.RootElement.TryGetProperty("result", out JsonElement resultArray))
            {
                // Enumerate through each ingredient in the "result" array
                foreach (JsonElement item in resultArray.EnumerateArray())
                {
                    // Extract the name and type from the JSON object
                    string? name = item.GetProperty("name").GetString();
                    string? ingType = item.GetProperty("type").GetString(); ;
                    
                    if(name != null && ingType != null)
                    {
                        // Create an Ingredient object and add it to the list
                        allIng.Add(new Ingredient(name, ingType));
                    }

                }
            }
        }

    }
}
