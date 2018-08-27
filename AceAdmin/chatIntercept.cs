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
        // called on all chat messages
        private void TextCommand(object sender, ChatTextInterceptEventArgs e)
        {
            try
            {
                // first check to see if we care, set up tText by calling "DoWeCare", return value will be True if it is something we do care about.
                string tText = DoWeCare(e.Text);

                // if tText is Nil do nothing
                if (!string.IsNullOrEmpty(tText))
                {
                    // Now that we know we care about this message lets Eat it so it will not go to the chat window.
                    //e.Eat = true;
                    
                    // get everything before the "=" and everything after the "="
                    string tTextBefore = Util.RemoveNonAlphaNum(Regex.Replace(tText, "=.+", ""));
                    string tTextAfter = Regex.Replace(tText, ".+=", "");

                    switch (tTextBefore)
                    {         
                        case "attackable":
                            AttackableCheckBox.Checked = Convert.ToBoolean(Util.RemoveNonAlphaNum(tTextAfter));
                            break;
                        case "PlayerList":
                            // Update the PlayerCount Text box that we are doing something as this might take a second or two if there are over ~600 players.
                            PlayerCountText.Text = "Updating Player List";
                            // call players.cs->UpdatePlayersListBox()
                            UpdatePlayersListBox(tTextAfter);
                            break;
                        case "PoiList":
                            // call poi.cs->UpdatePoiListBox()
                            UpdatePoiListBox(tTextAfter);
                            break;
                        case "AccountList":
                            // call account.cs->UpdateAccountListBox()
                            UpdateAccountListBox(tTextAfter);
                            break;
                    }
                }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        private string DoWeCare(string tText)
        {
            try
            {
                // If the message begins with #AceAdmin. then we care about it
                if (tText.StartsWith("#AceAdmin."))
                {
                    string tCommand = Regex.Replace(tText, "#AceAdmin.", "");
                    // remove carage return, linefeed from the string
                    tCommand = tCommand.TrimEnd('\r', '\n');
                    return tCommand;
                }
                // if we get to this point nothing above was true so retrun nil,  
                return "";
            }
            catch (Exception ex) { Util.LogError(ex); }
            return "";

        }


    }
}
