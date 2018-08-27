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

        [MVControlReference("AccountCountText")]
        private IStaticText AccountCountText = null;

        [MVControlReference("NewAccountNameEdit")]
        private ITextBox NewAccountNameEdit;
         
        [MVControlReference("NewAccountPasswordEdit")]
        private ITextBox NewAccountPasswordEdit;

        [MVControlReference("NewAccountAccessLevelChoice")]
        private ICombo NewAccountAccessLevelChoice;

        [MVControlEvent("NewAccountCreateBtn", "Click")]
        void NewAccountCreateBtn_Click(object sender, MVControlEventArgs e)
        {
            try 
            {
                string AcountName = NewAccountNameEdit.Text.Trim();
                string Password = NewAccountPasswordEdit.Text.Trim();

                if (!string.IsNullOrEmpty(AcountName) && !string.IsNullOrEmpty(Password))
                {
                    Util.ChatParser("@accountcreate " + AcountName + " " + Password + " " + NewAccountAccessLevelChoice.Text[NewAccountAccessLevelChoice.Selected].ToString());
                }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        [MVControlEvent("GetAccountListBtn", "Click")]
        void GetAccountListBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                AccountCountText.Text = "Getting Logged in Players.";
                Util.ChatParser("@AceAlistallaccounts");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        [MVControlReference("AccountListBox")]
        private IList AccountListBox = null;

        private void UpdateAccountListBox(string accountListString)
        {
            AccountListBox.Clear();
            // Build array, Each Set of data will be seperated by a '|'.
            string[] accountList = accountListString.Split('|');
            Array.Sort(accountList);
            string[] OneRow = null;
            // Loop through the array and put the data in the list box
            foreach (string account in accountList)
            {
                OneRow = account.Split(':');
                IListRow row = AccountListBox.Add();
                row[0][0] = OneRow[0];
                row[1][0] = AccessLevel[Int32.Parse(OneRow[1])];
            }
            AccountCountText.Text = ("Account Count: " + AccountListBox.RowCount.ToString() + " as of " + System.DateTime.Now);
        }

    }
}
