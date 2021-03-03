using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonvojSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyPrefab.Count; i++)
        {
            Instantiate(enemyPrefab[i]);
            enemyPrefab[i].GetComponent<KonvojFollower>().queuePosition = i;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
