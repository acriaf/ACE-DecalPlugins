using System;
using System.Timers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using Decal.Adapter;
using Decal.Adapter.Wrappers;
using MyClasses.MetaViewWrappers;


namespace AceAdmin
{
    public partial class PluginCore : PluginBase
    {

        [MVControlReference("PlayerCountText")]
        private IStaticText PlayerCountText = null;
         

        [MVControlEvent("GetLoggedOnPlayerListBtn", "Click")]
        void GetLoggedOnPlayerListBtn_Click(object sender, MVControlEventArgs e)
        { 
            try
            {
                PlayerCountText.Text = "Getting Logged in Players.";
                Util.ChatParser("@AceAlistLoggedOnPlayers");
                SelectedPlayerEdit.Text = "";
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("GetAllPlayerListBtn", "Click")]
        void GetAllPlayerListBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                PlayerCountText.Text = "Getting All Players.";
                Util.ChatParser("@AceAlistAllplayers");
                SelectedPlayerEdit.Text = "";
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        
        [MVControlReference("SelectedPlayerEdit")]
        private ITextBox SelectedPlayerEdit;

        [MVControlReference("PlayersListBox")]
        private IList PlayersListBox = null;

        private void UpdatePlayersListBox(string playerListString)
        {
            PlayersListBox.Clear(); 
            // Build array, Each Set of data will be seperated by a '|'.
            string[] playerList = playerListString.Split('|');
            Array.Sort(playerList);
            string[] OneRow = null;
            // Loop through the array and put the data in the list box
            foreach (string player in playerList)
            {
                OneRow = player.Split(':');
                IListRow row = PlayersListBox.Add();
                row[0][0] = OneRow[0];
                if (OneRow[1] == "True")
                {
                    int RowToColor = PlayersListBox.RowCount - 1;
                    System.Drawing.Color ColorOfRow = System.Drawing.Color.DarkGreen;
                    PlayersListBox[RowToColor][0].Color = ColorOfRow;
                    PlayersListBox[RowToColor][1].Color = ColorOfRow;
                    PlayersListBox[RowToColor][2].Color = ColorOfRow;
                }

                // access Level can be sent as the number or the string, this will handle both cases
                string accessLevel;
                if (int.TryParse(OneRow[2], out int result))
                    accessLevel = AccessLevel[Int32.Parse(OneRow[2])];
                else
                    accessLevel = OneRow[2];


                row[1][0] = accessLevel;
                row[2][0] = OneRow[3];
            }
            PlayerCountText.Text = ("Player Count: " + PlayersListBox.RowCount.ToString() + " as of " + System.DateTime.Now);
        }


        [MVControlEvent("PlayersListBox", "Click")]
        void PlayersListBox_Click(object sender, int row, int col)
        {
            try
            {
                SelectedPlayerEdit.Text = PlayersListBox[row][0][0].ToString();
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        [MVControlEvent("TeleToPlayerBtn", "Click")]
        void TeleToPlayerBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@teleto " + SelectedPlayerEdit.Text);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("BuffPlayerBtn", "Click")]
        void BuffPlayerBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                    if (!String.IsNullOrEmpty(SelectedPlayerEdit.Text.Trim()))
                    Util.ChatParser("@buff " + SelectedPlayerEdit.Text);
                else
                    Util.WriteToChat("A Player must be Selected to Buff them.");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("BuffTargetBtn", "Click")]
        void BuffTargetBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                if (Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection] != null)
                    Util.ChatParser("@buff " + Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection].Name.Trim());
                else
                    Util.WriteToChat("A Player must be Targeted to Buff them.");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        [MVControlEvent("FellowBuffPlayerBtn", "Click")]
        void FellowBuffPlayerBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(SelectedPlayerEdit.Text.Trim()))
                    Util.ChatParser("@fellowbuff " + SelectedPlayerEdit.Text);
                else
                    Util.WriteToChat("A Player must be Selected to Buff them.");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("FellowBuffTargetBtn", "Click")]
        void FellowBuffTargetBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                if (Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection] != null)
                    Util.ChatParser("@fellowbuff " + Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection].Name.Trim());
                else
                    Util.WriteToChat("A Player must be Targeted to Buff them.");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


    }
}
