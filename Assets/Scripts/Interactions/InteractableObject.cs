using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;
using UnityEngine;

public class InteractableObject : MonoBehaviour{
    [System.Serializable] public struct Contacts{
         public GameObject Hips;
         public GameObject LeftHand;
         public GameObject RightHand;
         public GameObject LeftFoot;
         public GameObject RightFoot;
    }

    [SerializeField] private Contacts ContactPoints;

    private Animator anim;
    //[SerializeField] private tag t;
    //private BoxCollider coll;


    // Start is called before the first frame update
    void Start(){
        //coll = gameObject.GetComponent<BoxCollider>();
        //if(coll != null) print("has col");
        print(ContactPoints.RightHand.transform.position);
        string to_search_tag="Player";
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++) {
            print(i + ": " + UnityEditorInternal.InternalEditorUtility.tags[i]);
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(to_search_tag)) {
                Debug.Log ("At Position " + i + " is the Tag " + to_search_tag + " found :) ");
                break;// attention : the first index is 0 !!!
            } else {}
        }


        if(gameObject.tag == "Door"){
            anim = gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //var l = RigBuilder.Rigging;
    }

    void DoAction(){
        if(gameObject.tag == "Door"){
            anim.SetBool("isOpen", true);
        }
    }

    void OnTriggerEnter(Collider other){
        print("touched");
        if (other.gameObject.CompareTag("Human")){
            List<RigLayer> rigs = other.gameObject.GetComponent<RigBuilder>().layers;
            
            //print(rigs.RigLayer);
            print(rigs[0].active);
            rigs[0].active = true;
            print("correct");
            DoAction();
        }
    }


}
