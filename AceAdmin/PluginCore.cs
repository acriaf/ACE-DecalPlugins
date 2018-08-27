using System;
using System.Timers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using Decal.Adapter;
using Decal.Adapter.Wrappers;
using MyClasses.MetaViewWrappers;
using Decal.Interop.Core;


namespace AceAdmin
{
    //Attaches events from core
    [WireUpBaseEvents]

    //View (UI) handling
    [MVView("AceAdmin.mainView.xml")]
    [MVWireUpControlEvents]

    [FriendlyName("ACE Admin")]
    public partial class PluginCore : PluginBase
    {
        protected override void Startup()
        {
            try
            {
                // This initializes our static Globals class with references to the key objects your plugin will use, Host and Core.
                // The OOP way would be to pass Host and Core to your objects, but this is easier.
                Globals.Init("AceAdmin", Host, Core);

                //Initialize the view.
                MVWireupHelper.WireupStart(this, Host);

                //  on startup init EvanHandler to listen for chat messages
                ChatBoxMessage += new EventHandler<ChatTextInterceptEventArgs>(TextCommand);

                CoreManager.Current.ItemSelected += Current_Selection;

                
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        protected override void Shutdown()
        {
            try
            {
                //Destroy the view.
                MVWireupHelper.WireupEnd(this);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        private void Current_Selection(object sender, ItemSelectedEventArgs e)
        {
           SelectedText.Text = Globals.Core.WorldFilter[e.ItemGuid].Name.Trim();
        }

        [BaseEvent("LoginComplete", "CharacterFilter")]
        private void CharacterFilter_LoginComplete(object sender, EventArgs e)
        {
            try
            {
                // Setting a timer to delay the fetching of values from the sever, this is because we are getting the loginComplete message but for a breaf moment we still are unable to post a message to chat.
                //init timer
                EventTimer = new Timer();
                // set timer to trigger at 800 miliseconds
                EventTimer.Interval = 800;
                // Hook up the event for the timer elapses. 
                EventTimer.Elapsed += OnTimedEvent;
                // Have the timer fire repeated events
                // setting this to true as the timer may be uses for other things 
                EventTimer.AutoReset = true;
                // Now turn on the timer
                EventTimer.Enabled = true;
                Globals.TimedEvantAction = "getServerValues";
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [BaseEvent("Logoff", "CharacterFilter")]
        private void CharacterFilter_Logoff(object sender, Decal.Adapter.Wrappers.LogoffEventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        // Initialize Stuff
        private static Timer EventTimer;

        public string[] AccessLevel = { "Player", "Advocate", "Sentinel", "Envoy", "Developer", "Admin" };

        [MVControlReference("SelectedText")]
        private IStaticText SelectedText = null;

        // **Button Clicks**
        // Main Page *Button Clicks*
        [MVControlEvent("ServerStatusBtn", "Click")]
        void ServerStatusBtn(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@serverstatus");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("ListPlayersBtn", "Click")]
        void ListPlayersBtn(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@listplayers");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        [MVControlReference("AttackableCheckBox")]
        private ICheckBox AttackableCheckBox = null;

        
        [MVControlEvent("AttackableCheckBox", "Change")]
        void AttackableCheckBox_Change(object sender, MVCheckBoxChangeEventArgs e)
        {
            try
            {
                if (AttackableCheckBox.Checked.Equals(true))
                {
                    Util.ChatParser("@attackable on");
                }
                else
                {
                    Util.ChatParser("@attackable off");
                }
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("BuffSelfBtn", "Click")]
        void BuffSelfBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                    Util.ChatParser("@buff");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("RunSelfBtn", "Click")]
        void RunSelfBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@run");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("HealSelfBtn", "Click")]
        void HealSelfBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@heal");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        //Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection].Name.Trim()

        [MVControlEvent("TestBtn", "Click")]
        void TestBtn(object sender, MVControlEventArgs e)
        {
            try
            {
                SelectedText.Text = Globals.Core.WorldFilter[Globals.Host.Actions.CurrentSelection].Name.Trim();
            }
            catch (Exception ex) { Util.LogError(ex); }
        }


        //IACHooksEvents_Event.ObjectSelected
        //    event IACHooksEvents_ObjectSelectedEventHandler ObjectSelected;

        //ACHooksClass.ObjectSelected

        [BaseEvent("ObjectSelected", "WorldFilter")]
        void WorldFilter_ObjectSelected(object sender, ItemSelectedEventArgs e)
        {
            try
            {
                // This can get very spammy so I filted it to just print on ident received
                Util.WriteToChat("WorldFilter_ChangeObject: " + e.ItemGuid);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

    }
}

