using UnityEngine;

public class Target : MonoBehaviour
{
    [Range(1.0f, 20.0f)]
    public float speed = 5f;

    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
    }
}
