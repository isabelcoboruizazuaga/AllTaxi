using UnityEngine;
using UnityEngine.AI;

public class RandomPointGenerator
{
    float maxX, minX;
    private float maxZ, minZ;


    private int areaMask = (1 << 0) + (0 << 1) + (1 << 2) + (1 << 3) + (1 << 4); //all areaMask except not walkable

    public RandomPointGenerator(float maxX, float maxZ, float minX, float minZ)
    {
        this.maxX = maxX;
        this.maxZ = maxZ;
        this.minX = minX;
        this.minZ = minZ;
    }
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

        if (RandomPosition(out point, areaMask, 30))
        {
            return point;
        }

        Debug.Log("Helicopter Generation failed");

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

}
