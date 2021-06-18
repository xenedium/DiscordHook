using System;
using System.Runtime.InteropServices;

namespace DiscordHook
{
    public static class Win32Api
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        
    }
}