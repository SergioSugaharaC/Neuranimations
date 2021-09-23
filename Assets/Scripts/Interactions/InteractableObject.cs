using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class InteractableObject : MonoBehaviour{
    [System.Serializable] public struct Contacts{
         public GameObject RightHand;
         public GameObject LeftHand;
         public GameObject RightFoot;
         public GameObject LeftFoot;
         public GameObject Hips;
    }

    [SerializeField] private Contacts ContactPoints;
    private List<RigLayer> rigs;
    private Transform targets;
    private Animator anim;

    private bool isActing = false;

    // Start is called before the first frame update
    void Start(){
        //print(ContactPoints.RightHand.transform.position);

        if(gameObject.tag == "Door"){
            anim = gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActing){
            //print("acting");
            CorrectPose();
        }
    }

    Transform GetContact(string name){
        switch(name){
            case "RightHand":
                return ContactPoints.RightHand.transform;
            case "LeftHand":
                return ContactPoints.LeftHand.transform;
            case "RightFoot":
                return ContactPoints.RightFoot.transform;
            case "LeftFoot":
                return ContactPoints.LeftFoot.transform;
            case "Hips":
                return ContactPoints.Hips.transform;
            default:
                return null;
        }
    }

    void CorrectPose(){
        Transform tar;
        Transform point;

        for(int i=0; i<rigs.Count; i++){
            if(rigs[i].active){
                tar = targets.GetChild(i).transform;
                point = GetContact(tar.name);
                tar.position = Vector3.Lerp(tar.position, point.position, Time.deltaTime * 2.5f);
            }
        }
    }

    void DoAction(){
        rigs[0].active = (ContactPoints.RightHand != null && ContactPoints.RightHand.active);
        rigs[1].active = (ContactPoints.LeftHand != null && ContactPoints.LeftHand.active);
        rigs[2].active = (ContactPoints.RightFoot != null && ContactPoints.RightFoot.active);
        rigs[3].active = (ContactPoints.LeftFoot != null && ContactPoints.LeftFoot.active);
        //if(ContactPoints.Hips != null) rigs[0].active = true; not declared

        switch(gameObject.tag){
            case "Door":
                DoorInteraction();
                break;
            case "Chair":
                ChairInteraction();
                break;
            default:
                break;
        }
    }

    void DoorInteraction(){
        //if(anim.GetBool("isOpen"))
        bool open = anim.GetBool("isOpen");
        anim.SetBool("isOpen", !open);
        ContactPoints.RightHand.SetActive(!ContactPoints.RightHand.active);
        ContactPoints.LeftHand.SetActive(!ContactPoints.LeftHand.active);
        isActing = true;
    }

    void ChairInteraction(){
        // wait for sit and correct pose
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Human")){
            rigs = other.gameObject.GetComponent<RigBuilder>().layers;
            targets = other.transform.Find("Targets");
            DoAction();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Human")){
            rigs = other.gameObject.GetComponent<RigBuilder>().layers;
            foreach (RigLayer layer in rigs){
                layer.active = false;
            }
        }
    }


}
