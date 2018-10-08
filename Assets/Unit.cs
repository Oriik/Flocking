using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public GameObject target;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;
	
	void Start ()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
	}

    Vector2 seek(Vector2 t)
    {
        return (t - location);
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        this.GetComponent<Rigidbody2D>().AddForce(force);
        Debug.DrawRay(this.transform.position, force, Color.white);
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        Vector2 gl;
        gl = seek(goalPos);
        currentForce = gl;
        currentForce = currentForce.normalized;

        applyForce(currentForce);
    }

	void Update () {
        flock();
        goalPos = target.transform.position;
	}
}
