namespace WebSsh.ResourceManager.Constants
{
    /// <summary>
    /// Константы для ssh терминала
    /// </summary>
    public sealed class SshTerminalConstants
    {
        /// <summary>
        /// При проверке подключения сессии терминала произошла ошибка
        /// </summary>
        public const string CheckTerminalError = "An error occurred while checking the connection of the terminal session";

        /// <summary>
        /// При подключении к устройству произошла ошибка
        /// </summary>
        public const string ConnectToSourceError = "An error occurred while connecting to the device";

        /// <summary>
        /// При отключении от устройства произошла ошибка
        /// </summary>
        public const string DisconnectSourceError = "An error occurred while disconnecting from the device";

        /// <summary>
        /// При выполнении команды произошла ошибка
        /// </summary>
        public const string ExecuteCommandError = "An error occurred while executing the command";

        /// <summary>
        /// При получении списка SSH сессий пользователя произошла ошибка
        /// </summary>
        public const string GettingUserSessionsError = "An error occurred while getting the list of SSH sessions of the user";
    }
}
