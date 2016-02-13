// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#if !W8CORE && NET35Plus

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace SharpDX.Toolkit
{
    internal class ToolkitHwndHost : HwndHost
    {
        private readonly HandleRef childHandle;

        #region | Native |

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetWindowLong32(HandleRef hwnd, WindowLongType index);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetWindowLong64(HandleRef hwnd, WindowLongType index);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowLong32(HandleRef hwnd, WindowLongType index, IntPtr wndProc);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hwnd, WindowLongType index, IntPtr wndProc);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetParent(HandleRef hWnd, IntPtr hWndParent);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool ShowWindow(HandleRef hWnd, int mCmdShow);

        public static IntPtr GetWindowLong(HandleRef hWnd, WindowLongType index)
        {
            if (IntPtr.Size == 4) return GetWindowLong32(hWnd, index);
            return GetWindowLong64(hWnd, index);
        }
        public static IntPtr SetWindowLong(HandleRef hwnd, WindowLongType index, IntPtr wndProcPtr)
        {
            if (IntPtr.Size == 4) return SetWindowLong32(hwnd, index, wndProcPtr);
            return SetWindowLongPtr64(hwnd, index, wndProcPtr);
        }
        public static bool ShowWindow(HandleRef hWnd, bool windowVisible)
        {
            return ShowWindow(hWnd, windowVisible ? 1 : 0);
        }

        #endregion

        public ToolkitHwndHost(IntPtr childHandle)
        {
            this.childHandle = new HandleRef(this, childHandle);
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            int style = GetWindowLong(childHandle, WindowLongType.Style).ToInt32();
            // Removes Caption bar and the sizing border
            style = style & ~WS_CAPTION & ~WS_THICKFRAME;
            // Must be a child window to be hosted
            style |= WS_CHILD;

            SetWindowLong(childHandle, WindowLongType.Style, new IntPtr(style));

            //MoveWindow(childHandle, 0, 0, (int)ActualWidth, (int)ActualHeight, true);
            SetParent(childHandle, hwndParent.Handle);

            ShowWindow(childHandle, false);

            return childHandle;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            SetParent(childHandle, IntPtr.Zero);
        }

        protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }

        // ReSharper disable InconsistentNaming
        private const int WS_CAPTION = unchecked(0x00C00000);
        private const int WS_THICKFRAME = unchecked(0x00040000);
        private const int WS_CHILD = unchecked(0x40000000);
        // ReSharper restore InconsistentNaming

        public enum WindowLongType
        {
            UserData = -21,
            ExtendedStyle = -20,
            Style = -16,
            Id = -12,
            HwndParent = -8,
            HInstance = -6,
            WndProc = -4,
        }
    }
}
#endif