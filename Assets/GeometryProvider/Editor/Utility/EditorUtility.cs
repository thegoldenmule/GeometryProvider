using System;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// For creating tabbed interfaces.
    /// </summary>
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

    /// <summary>
    /// Various in-editor functions.
    /// </summary>
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
                    GUILayout.Button(tab.Name, GUILayout.Height(25));
                    GUI.enabled = true;
                }
                else
                {
                    if (GUILayout.Button(tab.Name, GUILayout.Height(25)))
                    {
                        selectedTab = tab;
                    }
                }
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            if (null != selectedTab && null != selectedTab.OnDraw)
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