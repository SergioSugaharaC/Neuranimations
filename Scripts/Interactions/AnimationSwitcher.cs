using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Animations;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private List<AnimationTag> animations;
    private List<string> tagList = new List<string>();
    private Animator animator;
    private AnimatorController animCtr;

    // Start is called before the first frame update
    void Start()
    {
        tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
        animator = gameObject.GetComponent<Animator>();
        animCtr = animator.runtimeAnimatorController as AnimatorController;
        AddAnimationStates();
        /*
        print(animCtr.animationClips.Length);
        print(animCtr.animationClips[0]);
        if(animCtr.animationClips.Contains(animations[0].anim)) print("is in");
        if(animCtr.animationClips[0] == animations[0].anim)
            print("yes");
        else
            print("no");
        */
    }

    void AddAnimationStates(){
        foreach (AnimationTag anim in animations){
            if(!animCtr.animationClips.Contains(anim.anim))
                animCtr.AddMotion(anim.anim);
        }
    }

    void OnTriggerEnter(Collider other){
        foreach (AnimationTag anim in animations){
            if(other.gameObject.tag == tagList[anim.tag-1]){
                animator.Play(anim.anim.name);
            }
            
        }
    }
}

[System.Serializable]
public class AnimationTag
{
    public AnimationClip anim;
    // Additional field for storing the currently selected enemy index
    public int tag;
    public string selected;

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(AnimationTag))]
    private class AnimationTagPropertyDrawer : PropertyDrawer {

        // This will be generated during the OnGUI call
        // Maybe not the cleanest way but it works for now ;)
        private float height;
        private string[] availableTags;

        List<string> tagList = new List<string>();
        
        public void GetTags(){
            tagList.Add("<NoTag>");
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
        }

        // This method is required so other property drawers (like the Wave[] waves)
        // know how much space to reserve for drawing this property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
            return height + EditorGUIUtility.singleLineHeight;
        }
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            GetTags();
            // reset the height
            height = 0;

            // Using BeginProperty / EndProperty on the parent property means that    
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);
            {
                // Get the rect for where to draw the label/foldout
                var labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                position.y += EditorGUIUtility.singleLineHeight;
                height += EditorGUIUtility.singleLineHeight;

                // Draw the foldout
                property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, property.displayName);
                
                if (property.isExpanded){
                    // indent children for better readability
                    EditorGUI.indentLevel++;
                    {
                        // Get serialized Properties
                        var serializedAnimations = property.FindPropertyRelative(nameof(anim));
                        var serializedTag = property.FindPropertyRelative(nameof(tag));
                        //var serializedSelected = property.FindPropertyRelative(nameof(selected));

                        // Calculate rects
                        var animHeight = EditorGUI.GetPropertyHeight(serializedAnimations);
                        var animRect = new Rect(position.x, position.y, position.width, animHeight);
                        position.y += animHeight;
                        height += animHeight;
                        
                        var selectedEnemyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                        position.y += EditorGUIUtility.singleLineHeight;
                        height += EditorGUIUtility.singleLineHeight;

                        // Draw Fields
                        availableTags = new string[tagList.Count];
                        for (var i = 0; i < tagList.Count; i++){
                            availableTags[i] = tagList[i];
                        }
                        
                        EditorGUI.PropertyField(animRect, serializedAnimations);
                        serializedTag.intValue = EditorGUI.Popup(selectedEnemyRect, serializedTag.displayName, serializedTag.intValue, availableTags);
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}
