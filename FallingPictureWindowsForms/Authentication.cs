using System;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Provides authentication functionality for the application.
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Checks the user credentials against a predefined set.
        /// </summary>
        /// <param name="i_Username">The username to check.</param>
        /// <param name="i_Password">The password to check.</param>
        /// <returns>True if credentials are valid, false otherwise.</returns>
        public static bool checkUserCredentials(string i_Username, string i_Password)
        {
            // TODO: Implement proper authentication with a secure database
            const bool v_ValidCredentials = true;

            if (i_Username == "kali" && i_Password == "kali")
            {
                return v_ValidCredentials;
            }

            return !v_ValidCredentials;
        }
    }
}