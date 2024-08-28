using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer.Selectables
{
    public static class SelectablesHandler
    {
        public static readonly Color hoverColor = Color.FromArgb(229, 241, 251);
        public static readonly Color selectedColor = Color.FromArgb(205, 228, 246);

        private static List<KeyValuePair<Control, List<ISelectable>>> selectionList = new();

        private static readonly MouseDownMSGFilter mouseDownFilter = new();

        internal static Point mouseDownPosition;

        public static void Init()
        {
            Application.AddMessageFilter(mouseDownFilter);
        }

        public static void NotifySelected(Control container, ISelectable selectable)
        {
            foreach (var knownContainerKVP in selectionList)
            {
                if (knownContainerKVP.Key == container)
                {

                    if (!knownContainerKVP.Value.Contains(selectable))
                        knownContainerKVP.Value.Add(selectable);
                    return;
                }
            }

            selectionList.Add(new(container, new() { selectable }));
        }

        public static void NotifyDeselected(Control? container, ISelectable selectable)
        {
            if (container == null)
                goto fallback;

            foreach (var knownContainerKVP in selectionList)
            {
                if (knownContainerKVP.Key == container)
                {
                    if (!knownContainerKVP.Value.Contains(selectable))
                        knownContainerKVP.Value.Remove(selectable);

                    if (knownContainerKVP.Value.Count == 0)
                        selectionList.Remove(knownContainerKVP);
                    return;
                }
            }

            fallback:
            foreach (var thing in selectionList)
            {
                if (thing.Value.Contains(selectable))
                    thing.Value.Remove(selectable);
            }
        }

        public static void ClearAllSelectables()
        {
            List<KeyValuePair<Control, List<ISelectable>>> selectionListClone = new(selectionList);
            foreach (var knownContainerKVP in selectionListClone)
            {
                List<ISelectable> selectionListValueClone = new(knownContainerKVP.Value);
                foreach (var selectable in selectionListValueClone)
                {
                    selectable.NotifySelectableIsDeselected();
                }
            }

            selectionList.Clear();
        }

        public static Point GetLastMouseDown()
        {
            return mouseDownPosition;
        }

        public static List<ISelectable>? TryGetSelectedChildren(Control self)
        {
            foreach (var kvp in selectionList)
            {
                if (kvp.Key == self)
                {
                    return kvp.Value;
                }
            }

            return null;
        }

        public static bool IsMouseOffsetTooLarge()
        {
            var mPos = Cursor.Position;

            if (Math.Abs(mouseDownPosition.X - mPos.X) > 20 || Math.Abs(mouseDownPosition.Y - mPos.Y) > 20)
                return true;
            return false;
        }

        private class MouseDownMSGFilter : IMessageFilter
        {
            private const int WM_LBUTTONDOWN = 0x201;
            private const int WM_LBUTTONUP = 0x0202;
            private const int WM_LBUTTONDBLCLK = 0x0203;

            public bool PreFilterMessage(ref Message msg)
            {
                if (msg.Msg == WM_LBUTTONDOWN || msg.Msg == WM_LBUTTONDBLCLK)
                {
                    mouseDownPosition = Cursor.Position;
                }

                if (msg.Msg == WM_LBUTTONUP || msg.Msg == WM_LBUTTONDBLCLK)
                {
                    var mPos = Cursor.Position;

                    if (IsMouseOffsetTooLarge())
                        return false;

                    Keyboard keyboard = new();
                    if (!keyboard.CtrlKeyDown && !keyboard.ShiftKeyDown)
                        ClearAllSelectables();

                    foreach (var crtl in selectionList)
                    {
                        if (!crtl.Key.ClientRectangle.Contains(crtl.Key.PointToClient(mPos)))
                            foreach (var child in crtl.Value)
                                child.NotifySelectableIsDeselected();
                    }
                }
                return false;
            }
        }
    }
}
