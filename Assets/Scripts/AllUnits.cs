using UnityEngine;

public class AllUnits : MonoBehaviour
{
    public Transform target;
    public GameObject unitPrefab;
    public int numUnits = 10;
    public Vector2 spawnRange = new Vector3(5, 5);

    private GameObject[] units;
    public GameObject[] Units
    {
        get
        {
            return units;
        }
    }

    void Start()
    {
        units = new GameObject[numUnits];
        for (int i = 0; i < numUnits; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-spawnRange.x, spawnRange.x),
                Random.Range(-spawnRange.y, spawnRange.y));
            units[i] = Instantiate(unitPrefab, transform.position + unitPos, Quaternion.identity, transform);
            units[i].GetComponent<Unit>().Target = target;
            units[i].GetComponent<Unit>().Manager = this;
        }
    }
}
