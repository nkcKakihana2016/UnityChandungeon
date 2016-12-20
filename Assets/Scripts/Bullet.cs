using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    

	// Use this for initialization
	void Start () {

       Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = playerTransform.position;
        transform.forward = playerTransform.forward;
		


	}
	
	// Update is called once per frame
	void Update () {
        float bulletSpeed = 10.0f;



        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        if (transform.position.magnitude > 30.0f)
        {
            Destroy(gameObject);
        }
		
	}

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
            Destroy(hit.gameObject);
        }
    }
}
