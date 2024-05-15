using UnityEngine;
using UnityEngine.AI;

public class RandomPointGenerator
{
    float maxX, minX;
    private float maxZ, minZ;
    private float maxY, minY;
    private float helicopterMisionAparitionRate = 0.2f;


    private int areaMask = (1 << 0) + (0 << 1) + (1 << 2) + (1 << 3) + (1 << 4); //all areaMask except not walkable

    public RandomPointGenerator(float maxX, float maxZ, float minX, float minZ)
    {
        this.maxX = maxX;
        this.maxZ = maxZ;
        this.minX = minX;
        this.minZ = minZ;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        /*    (1 << 0) + (0 << 1) + (1 << 2) + (1 << 3) + (1 << 4)
    = 1 + 0 + 4 + 8 + 16
    = 29

        Layer 0 : "Walkable" => 1
        Layer 1 : "Not walkable" => 0
        Layer 2 : "Jump" => 1
        Layer 3 : "Road" => 1
        Layer 4 : "Helicopter" => 1

        for(int i = 0; i < 100; i++)
        {


            SetHelicopterPoint();
        }for(int i = 0; i < 100; i++)
        {


            SetTaxiPoint();
        }
    }

    void Update()
    {
        /*Vector3 point;
        if (RandomPosition(out point, areaMask, 10))
        {
            Instantiate(beacon, point, Quaternion.identity);
        }



    }
*/
    public Vector3 GetTaxiPoint()
    {
        Vector3 point = Vector3.zero;

        if (RandomPosition(out point, areaMask, 10))
        {
            return point;
        }

        Debug.Log("Generation failed");

        return point;
    }
    public Vector3 GetHelicopterPoint()
    {
        Vector3 point = Vector3.zero;
        if (RandomPosition(out point, areaMask, 35))
        {

            return point;
            //Instantiate(beacon2, point, Quaternion.identity);
        }
        return point;
    }

    public bool RandomPosition(out Vector3 result, int areamask, int height)
    {
        for (int i = 0; i < 30; i++)
        {
            //Generates a random point inside of max area
            var randomX = Random.Range(minX, maxX);
            var randomZ = Random.Range(minZ, maxZ);


            Vector3 randomPoint = new Vector3(randomX, height, randomZ);
            NavMeshHit hit;

            //Calculates the point inside the navmesh
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, areamask))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    private float range = 50f;
    /*bool RandomPoint(Vector3 center, float range, out Vector3 result, int areamask)
    {
        for (int i = 0; i < 30; i++)
        {
            //Generates a random point inside of a 100 meters area
            Vector3 randomPoint = center + Random.insideUnitSphere * range;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, areamask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
*/
}
