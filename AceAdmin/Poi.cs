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

        [MVControlReference("PoiListBox")]
        private IList PoiListBox = null;

        private void UpdatePoiListBox(string poiListString)
        {
            PoiListBox.Clear();
            // Build array, Each Set of data will be seperated by a '|'.
            string[] poiList = poiListString.Split('|');
            Array.Sort(poiList);
            // Loop through the array and put the data in the list box
            foreach (string poi in poiList)
            {
                IListRow row = PoiListBox.Add();
                row[0][1] = 100693024;
                row[1][0] = poi;
            }
        }


        [MVControlEvent("PoiListBox", "Click")]
        void PoiListBox_Click(object sender, int row, int col)
        {
            try
            {
                String Dest = PoiListBox[row][1][0].ToString();
                Util.ChatParser("@telepoi " + Dest);
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

        [MVControlEvent("PoiRefreshBtn","Click")]
        void PoiRefreshBtn_Click(object sender, MVControlEventArgs e)
        {
            try
            {
                Util.ChatParser("@AceAlistPoi");
            }
            catch (Exception ex) { Util.LogError(ex); }
        }

    }
}
