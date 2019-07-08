﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTerminalControl
{
    internal static class NativeMethods
    {
        public delegate void ScrollCallback(int viewTop, int viewHeight, int bufferSize);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern uint CreateTerminal(IntPtr parent, out IntPtr hwnd, out IntPtr terminal);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void SendTerminalOutput(IntPtr terminal, string lpdata);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern uint TriggerResize(IntPtr terminal, double width, double height, out int columns, out int rows);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void DpiChanged(IntPtr terminal, int newDpi);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterScrollCallback(IntPtr terminal, [MarshalAs(UnmanagedType.FunctionPtr)]ScrollCallback callback);

        [DllImport("PublicTerminalCore.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void UserScroll(IntPtr terminal, int viewTop);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetKeyState(int keyCode);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }
    }
}