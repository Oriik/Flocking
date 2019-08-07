using UnityEngine;

[CreateAssetMenu]
public class Flocking : ScriptableObject
{
    public bool seekGoal = true;
    public bool obedient = true;
    public bool willful = false;

    [Range(0, 200)]
    public int neighbourDistance = 50;

    [Range(0, 20)]
    public float maxVelocity = 2.0f;

    [Range(0, 10)]
    public float maxForce = 0.5f;
}
