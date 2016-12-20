using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public Transform chara;

	// Use this for initialization
	void Start () {

        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
           chara.transform.position = new Vector3(-0.75f, 2.0f, 36.75f);
        }
    }

}
