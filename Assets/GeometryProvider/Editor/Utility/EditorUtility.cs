using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    public class Tab
    {
        public readonly string Name;
        public readonly Action OnDraw;

        public Tab(string name, Action onDraw)
        {
            Name = name;
            OnDraw = onDraw;
        }
    }

    public static class EditorUtility
    {
        public static string DrawTabs(
            string state,
            params Tab[] tabs)
        {
            if (null == tabs || tabs.Length == 0)
            {
                return null;
            }

            if (null == state)
            {
                state = tabs[0].Name;
            }

            GUILayout.BeginHorizontal();

            Tab selectedTab = null;
            for (int i = 0, len = tabs.Length; i < len; i++)
            {
                Tab tab = tabs[i];
                if (state == tab.Name)
                {
                    if (null == selectedTab)
                    {
                        selectedTab = tab;
                    }

                    GUI.enabled = false;
                    GUILayout.Button(tab.Name);
                    GUI.enabled = true;
                }
                else
                {
                    if (GUILayout.Button(tab.Name))
                    {
                        selectedTab = tab;
                    }
                }
            }

            GUILayout.EndHorizontal();

            if (null != selectedTab.OnDraw)
            {
                selectedTab.OnDraw();
            }

            return selectedTab.Name;
        }
        
        public static void SelectAndFocus(Transform transform)
        {
            Selection.activeTransform = transform;

            if (null != SceneView.lastActiveSceneView)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}