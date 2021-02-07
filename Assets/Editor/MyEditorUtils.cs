using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//Ref: https://docs.unity3d.com/Manual/editor-EditorWindows.html
public class MyEditorUtils : EditorWindow
{
    [MenuItem("Window/My Editor Utils")]

    public static void ShowWindow()
    {
        GetWindow(typeof(MyEditorUtils));
    }

    void OnGUI()
    {
        GUILayout.Label("Player Settings", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear Player Items"))
        {
            ClearPlayerItems();
        }
    }

    private void ClearPlayerItems()
    {
        Debug.Log("<color=cyan>Cleaning player inventory</color>");
        var playerData = Resources.LoadAll<PlayerInventoryData>("");
        if (playerData.Length > 1)
        {
            GUILayout.Label("Select a inventory to clean", EditorStyles.boldLabel);
            Debug.Log("<color=cyan>Select a inventory to clean</color>");

            foreach (var item in playerData)
            {
                if (GUILayout.Button($"Clear {item.name}"))
                {
                    ClearPlayerItem(item);
                }
            }
        }
        else if (playerData.Length > 0)
        {
            ClearPlayerItem(playerData[0]);
        }
        else
        {
            Debug.Log("<color=cyan>Inventory not found</color>");
        }
    }

    private void ClearPlayerItem(PlayerInventoryData inventory)
    {
        foreach (var item in inventory.Items)
        {
            item.amount = 0;
            item.discovered = false;
        }
        Debug.Log($"<color=cyan>{inventory.name} cleared</color>");
    }
}
