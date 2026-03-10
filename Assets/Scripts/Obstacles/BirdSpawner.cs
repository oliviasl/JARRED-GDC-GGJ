using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private int maxBirds = 3;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private GameObject windshield;

    [Header("Spawn Bounds")] 
    [SerializeField] private int minX = -50;
    [SerializeField] private int maxX = 50;
    [SerializeField] private int minY = -50;
    [SerializeField] private int maxY = 50;

    private float timer;
    private List<GameObject> birds = new List<GameObject>();

    public void RemoveBird(GameObject bird)
    {
        birds.Remove(bird);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCooldown && birds.Count < maxBirds)
        {
            GameObject bird = Instantiate(birdPrefab) as GameObject;
            bird.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
            
            // randomize spawn position
            int posX = Random.Range(minX, maxX);
            int posY = Random.Range(minY, maxY);
            Vector3 spawnPos = transform.position;
            spawnPos.x += posX;
            spawnPos.y += posY;
            bird.transform.position = spawnPos;
            
            BirdObstacle obstacle = bird.GetComponent<BirdObstacle>();
            if (obstacle)
            {
                obstacle.SetBirdSpawner(this);
                obstacle.SetPlaneWindshield(windshield);
            }
            birds.Add(bird);
            
            timer = 0f;
        }
    }
    
    void OnDrawGizmos()
    {
        Vector3 boxCenter = transform.position;
        Vector3 boxSize = new Vector3(Mathf.Abs(minX - maxX), Mathf.Abs(minY - maxY), 1);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
