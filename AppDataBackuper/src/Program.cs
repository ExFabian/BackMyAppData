using static AppDataBackuper.Form1;

namespace AppDataBackuper.src
{
    // Make our own CheckedListBox implementation that ignores checking when double-clicking an item and allows checking to be made by clicking the checkbox. Why, Microslop, is this not a toggleable option?
    class FixedCheckedListBox : CheckedListBox
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONDBLCLK = 0x0203;

            // If we detect a (double) left click
            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
            {
                // LParam, in this case, is an Int32 that contains the X and Y coordinates of the mouse: First 16 bits are the Y value and the other 16 are the X value.
                // Here, we take only the latter half of the bits for the x value and the former half for the y value with some bitwise operations
                short x = (short)(m.LParam.ToInt32() & 0xFFFF);
                short y = (short)(m.LParam.ToInt32() >> 16);

                int index = IndexFromPoint(new Point(x, y));

                if (index != NoMatches)
                {
                    Rectangle itemRect = GetItemRectangle(index);

                    // Select the item, and toggle check state if the checkbox was clicked
                    if(GetSelected(index) == false)
                        SetSelected(index, true);
                    if (x < itemRect.Left + 13)
                        SetItemChecked(index, !GetItemChecked(index));
                }

                // Don't pass the inputs further ahead
                return;
            }

            base.WndProc(ref m);
        }
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}