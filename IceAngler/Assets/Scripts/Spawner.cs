using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public Transform SpawnTransform { get { return spawnTransform; } set { spawnTransform = value; } }

    [SerializeField] private MarineObject EelPrefab;
    [SerializeField] private MarineObject SharkPrefab;
    [SerializeField] private MarineObject BootPrefab;

    private List<MarineObject> marineObjects;

    protected Transform spawnTransform;
    protected Vector3 spawnPosition;
    protected Quaternion spawnRotation;
    protected Vector3 spawnScale;

    int offsetY;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnTransform)
        {
            spawnPosition = spawnTransform.position;
            spawnRotation = spawnTransform.rotation;
            spawnPosition = spawnTransform.localScale;
        }
        else
        {
            spawnTransform = this.transform;

            spawnPosition = spawnTransform.position;
            spawnRotation = spawnTransform.rotation;
            spawnPosition = spawnTransform.localScale;
        }

        marineObjects = new List<MarineObject>();

        marineObjects.Add(EelPrefab);
        marineObjects.Add(SharkPrefab);
        marineObjects.Add(BootPrefab);

        for(int i = 0; i < 3; i++)
            SpawnPrefab(GetMarineObjectToSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn(MarineObject prefab, int numTypes) // consider abstract base class if we need to hide things.
    {
        for(int i = 0; i < numTypes; i++)
        {
            SpawnPrefab(prefab);
        }
    }

    private void SpawnPrefab(MarineObject prefab)
    {
        MarineObject marineObject = ObjectPool.SharedInstance.GetPooledObject();

        if (marineObject != null)
        {
            marineObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + offsetY, this.transform.position.z);
            marineObject.IsActivated = true;
        }
        offsetY -= 100;
    }

    private MarineObject GetMarineObjectToSpawn()
    {
        int randIndex = Random.Range(0, marineObjects.Count);
        return marineObjects[randIndex];
    }
}