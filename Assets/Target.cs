using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    [Range(1.0f, 20.0f)]
    public float speed = 5f; 
	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);

    }
}
