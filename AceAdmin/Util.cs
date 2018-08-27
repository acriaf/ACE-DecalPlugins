using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AceAdmin
{
	public static class Util
	{
		public static void LogError(Exception ex)
		{
			try
			{
                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Decal\\" + Globals.PluginName + "\\errors.txt", true))
                {
					writer.WriteLine("============================================================================");
					writer.WriteLine(DateTime.Now.ToString());
					writer.WriteLine("Error: " + ex.Message);
					writer.WriteLine("Source: " + ex.Source);
					writer.WriteLine("Stack: " + ex.StackTrace);
					if (ex.InnerException != null)
					{
						writer.WriteLine("Inner: " + ex.InnerException.Message);
						writer.WriteLine("Inner Stack: " + ex.InnerException.StackTrace);
					}
					writer.WriteLine("============================================================================");
					writer.WriteLine("");
					writer.Close();
				}
			}
			catch
			{
			}
		}

        public static void LogMessage(string tText)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Decal\\" + Globals.PluginName + "\\errors.txt", true))
                {
                    writer.WriteLine(tText);
                    writer.Close();
                }
            }
            catch
            {
            }
        }



        public static void ChatParser(string message)
        {
            try
            {
                Globals.Host.Actions.InvokeChatParser(message);
            }
            catch (Exception ex) { LogError(ex); }
        }

        // WriteToChat will just send messages to the Chat window, not to the server.
        // to send messages to the server use ChatParser
        public static void WriteToChat(string message)
		{
            try
			{
				Globals.Host.Actions.AddChatText("<{" + Globals.PluginName + "}>: " + message, 4);
			}
			catch (Exception ex) { LogError(ex); }
		}


        public static string RemoveNonAlphaNum(string textIn)
        {
            return Regex.Replace(textIn, "[^a-zA-Z]", "");

        }



    }
}
