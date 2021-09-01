using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PluginInformationWindow : EditorWindow
{
    [MenuItem("Neuranimation/Information")]
    public static void ShowWindow(){
        GetWindow<PluginInformationWindow>("Plugin Information");
    }

    void OnGUI () {
        GUILayout.Label("Neuranimation:\nReactive Real Time Animations using Phase Function Neural Networks", EditorStyles.boldLabel);
    
        if(GUILayout.Button("See Previous Versions")){
            Application.OpenURL("https://github.com/SergioSugaharaC/Neuranimations");
        }
    }
}
