using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxOpen : MonoBehaviour {

    Animator boxAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider boxCol)
    {
        if (boxCol.gameObject.tag == "Player"&&Input.GetKey(KeyCode.E)==true)
        {
            boxAnimator.SetBool("Open",true);
        }
    }

}
