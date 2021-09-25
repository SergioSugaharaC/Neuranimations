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
            OpenGitRepo();
        }

        if(GUILayout.Button("Access Model")){
            OpenModelRepo();
        }

        if(GUILayout.Button("See the Documentation")){
            OpenGuide();
        }
    }

    [MenuItem("Neuranimation/Versions")]
    public static void OpenGitRepo(){
        Application.OpenURL("https://github.com/SergioSugaharaC/Neuranimations");
    }

    [MenuItem("Neuranimation/Model")]
    public static void OpenModelRepo() {
        Application.OpenURL("https://github.com/Fireinfern/Neuranimation_PFNN");
    }

    [MenuItem("Neuranimation/Guide")]
    public static void OpenGuide() {
        Application.OpenURL("https://www.sebastiansilva.xyz/neuranimation-docs/usage/");
    }
}
