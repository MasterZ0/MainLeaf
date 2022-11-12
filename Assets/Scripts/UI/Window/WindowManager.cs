using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.UI.Window
{
    /// <summary>
    /// Controls which Window are open
    /// </summary>
    public static class WindowManager
    {
        public static bool HasWindowOpen => CurrentWindow != null;

        public static event Action OnCloseLastWindow;

        private static IWindow CurrentWindow;
        private static readonly Stack<SaveWindow> WindowsHistory = new Stack<SaveWindow>(); // Clear after changing room?

        public static void RequestOpenWindow(IWindow window)
        {
            if (HasWindowOpen)
            {
                SaveWindow save = new SaveWindow(CurrentWindow);
                WindowsHistory.Push(save);
                CurrentWindow.CloseWindow();
            }

            CurrentWindow = window;
            CurrentWindow.OpenWindow();
            EventSystem.current.SetSelectedGameObject(CurrentWindow.FirstGameObject);
        }

        public static void CloseCurrentWindow()
        {
            CurrentWindow.CloseWindow();

            if (WindowsHistory.TryPop(out SaveWindow last))
            {
                last.ReturnToWindow();
                CurrentWindow = last.Window;
            }
            else
            {
                OnCloseLastWindow();
                CurrentWindow = null;
            }
        }

        /// <summary> Close current window and clean the history </summary>
        /// <remarks> Recommended when switching scenes </remarks>
        public static void CloseAllWindows()
        {
            WindowsHistory.Clear();
            CurrentWindow?.CloseWindow();
            CurrentWindow = null;
        }

        public static bool CheckCurrentWindow(IWindow window)
        {
            return CurrentWindow == window;
        }

        private class SaveWindow
        {
            public IWindow Window { get; }
            public GameObject LastGameObject { get; }

            public SaveWindow(IWindow currentWindow)
            {
                Window = currentWindow;
                LastGameObject = EventSystem.current.currentSelectedGameObject;
            }

            public void ReturnToWindow()
            {
                Window.OpenWindow();

                if (LastGameObject) // Auto select
                    EventSystem.current.SetSelectedGameObject(LastGameObject);
            }
        }
    }
}