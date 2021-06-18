using System;
using System.Runtime.InteropServices;

namespace DiscordHook
{
    public static class Win32Api
    {
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        
    }
}