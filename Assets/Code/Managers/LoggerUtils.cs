namespace Code.Managers
{
    public static class LoggerUtils
    {
        public static string ToLogPrefix(this CustomLogger.LogType logType)
        {
            switch (logType)
            {
                case CustomLogger.LogType.StateLog:
                    return "<color=#33ffac>STATE: </color>";
                
                case CustomLogger.LogType.Inventory:
                    return "<color=#9f33ff>INVENTORY: </color>";
                
                default:
                    return string.Empty;
            }
        }
    }
}