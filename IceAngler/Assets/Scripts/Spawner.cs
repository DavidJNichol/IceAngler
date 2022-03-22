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

        marineObjects.Add(new Eel());


        Spawn(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn(int numTypes) // consider abstract base class if we need to hide things.
    {
        for(int i = 0; i < numTypes; i++)
        {
            SpawnPrefab(EelPrefab, 1);
        }
    }

    private void SpawnPrefab(MarineObject prefab, int count)
    {
        for(int i = 0; i < count; i++)
            Instantiate<MarineObject>(prefab, this.transform.position, this.transform.rotation).IsActivated = true;
    }

    private MarineObject GetMarineObjectToSpawn()
    {

        return null;
    }
}
