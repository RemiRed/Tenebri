using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTraps : MonoBehaviour {
/*
    [SerializeField]
    List<GameObject> traps = new List<GameObject>();
    [SerializeField]
    int maxTraps, minTraps;
    
	// Use this for initialization
	void Start () {
        RandomizeTrap();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("g"))
        {
            foreach(GameObject gre in traps)
            {
                gre.GetComponent<FallingDown>().clod.enabled = true;
            }
            RandomizeTrap();
        }
	}

    void RandomizeTrap()
    {

        int howMany = Random.Range(minTraps, maxTraps + 1);
        List<GameObject> tempTraps;
        tempTraps = new List<GameObject>(traps);
        for(int i = 0; i < howMany; i++)
        {
            int theOne = Random.Range(0, tempTraps.Count);
            tempTraps[theOne].GetComponent<FallingDown>().clod.enabled = false;
            tempTraps.Remove(tempTraps[theOne]);
        }
    }
    */
}
