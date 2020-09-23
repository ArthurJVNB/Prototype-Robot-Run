using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int distanceFromPlayer = 100;
    [SerializeField] float repeatRate = 1;
    [SerializeField] Spawns spawn;

    GameObject player;

    [System.Serializable]
    public class Spawns
    {
        public Vector3[] positions;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("InstantiateNewObstacle", 0, repeatRate);
    }

    private void InstantiateNewObstacle()
    {
        List<Vector3> positions = new List<Vector3>();
        int numberOfObjectsToSpawn = Mathf.RoundToInt(Random.Range(1f, 3f));

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 position;
            
            do
            {
               position  = GetNewSpawnPosition();
            } while (positions.Contains(position));
            
            positions.Add(position);

            GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obstacle.transform.position = position;
            obstacle.transform.localScale = new Vector3(1.5f, 1.2f, 3);
            obstacle.AddComponent<Rigidbody>();
        }
        
    }

    private Vector3 GetNewSpawnPosition()
    {
        Vector3 chosenPosition = spawn.positions[Random.Range(0, spawn.positions.Length)];
        return new Vector3(chosenPosition.x, 2, player.transform.position.z + distanceFromPlayer);
        //return new Vector3(Random.Range(bounds.left, bounds.right), 0, player.transform.position.z + distanceFromPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        if (player)
            Gizmos.DrawCube(player.transform.position + Vector3.forward * distanceFromPlayer, Vector3.one * 2);

        //Gizmos.DrawCube(new Vector3(transform.position.x - bounds.left, transform.position.y, transform.position.z), new Vector3(0.2f, 2, 1000));
        //Gizmos.DrawCube(new Vector3(transform.position.x - bounds.right, transform.position.y, transform.position.z), new Vector3(0.2f, 2, 1000));

        if (spawn.positions.Length > 0)
        {
            Gizmos.color = Color.green;
            foreach (var position in spawn.positions)
            {
                Gizmos.DrawCube(position, Vector3.one);
            }
        }
    }
}
