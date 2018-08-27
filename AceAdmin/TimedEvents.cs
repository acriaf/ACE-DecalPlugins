using System;
using System.Timers;

using Decal.Adapter;
using Decal.Adapter.Wrappers;
using MyClasses.MetaViewWrappers;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AceAdmin
{
    public partial class PluginCore : PluginBase
    {
        
        void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // setting this up for different timed event actions right now we only have one action.     
            switch (Globals.TimedEvantAction)
            {
                case "getServerValues":
                    Util.ChatParser("@attackablestatus");
                    Util.ChatParser("@AceAlistpoi");
                    break;
            }
            // turn off the timer, this might change if more timed events are needed elsewhere.
            EventTimer.Enabled = false;
            

        }
    }
}
