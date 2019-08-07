using UnityEngine;

public class Unit : MonoBehaviour
{
    public Flocking flocking;

    private AllUnits manager;
    private Transform target;
    private Rigidbody2D m_rigidbody;
    private Vector2 currentForce;

    public AllUnits Manager
    {
        set
        {
            manager = value;
        }
    }

    public Transform Target
    {
        set
        {
            target = value;
        }
    }

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
    }

    private void FixedUpdate()
    {
        Flock();
    }

    private void Flock()
    {
        if (Random.Range(0, 50) < 1)
        {
            if (flocking.willful)
            {
                currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
            }
            else if (flocking.obedient)
            {
                Vector2 ali = Align();
                Vector2 coh = Cohesion();
                Vector2 gl;
                if (flocking.seekGoal)
                {
                    gl = Seek(target.position);
                    currentForce = gl + ali + coh;
                }
                else
                {
                    currentForce = ali + coh;
                }
                currentForce = currentForce.normalized;
            }
        }
        ApplyForce(currentForce);
    }

    private void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        if (force.magnitude > flocking.maxForce)
        {
            force = force.normalized * flocking.maxForce;
        }
        m_rigidbody.AddForce(force);

        if (m_rigidbody.velocity.magnitude > flocking.maxVelocity)
        {
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * flocking.maxVelocity;
        }
        Debug.DrawRay(transform.position, force, Color.red);
    }

    /// <summary>
    /// Average velocity of neighborhood
    /// </summary>
    /// <returns>Normalized average velocity  of neighborhood</returns>
    private Vector2 Align()
    {
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.Units)
        {
            if (other == gameObject)
            {
                continue;
            }

            float d = Vector2.Distance(transform.position, other.transform.position);

            if (d < flocking.neighbourDistance)
            {
                sum += other.GetComponent<Rigidbody2D>().velocity;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return sum;
        }
        return Vector2.zero;
    }

    /// <summary>
    /// Average position of neighborhood
    /// </summary>
    /// <returns>Average position of neighborhood</returns>
    private Vector2 Cohesion()
    {
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.Units)
        {
            if (other == gameObject)
            {
                continue;
            }

            float d = Vector2.Distance(transform.position, other.transform.position);

            if (d < flocking.neighbourDistance)
            {
                sum += (Vector2)other.transform.position;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return Seek(sum);
        }
        return Vector2.zero;

    }

    private Vector2 Seek(Vector2 t)
    {
        return (t - (Vector2)transform.position);
    }
}
