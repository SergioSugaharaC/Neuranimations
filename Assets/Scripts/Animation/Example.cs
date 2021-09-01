using UnityEngine;
using UnityEditor;

public class Example : MonoBehaviour
{
    [SerializeField] private GameObject rootGO;
    [SerializeField] private string rootMotion;
    void Start()
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (rootGO != null &&
            rootGO.GetComponent<Animator>() != null)
        {
            Avatar avatar = AvatarBuilder.BuildGenericAvatar(rootGO, rootMotion);
            avatar.name = "InsertYourName";
            Debug.Log(avatar.isHuman ? "is human" : "is generic");

            Animator animator = rootGO.GetComponent<Animator>() as Animator;
            animator.avatar = avatar;
        }
    }
}