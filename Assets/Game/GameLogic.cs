using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            GameObject o = t.gameObject;
            if (o.layer == LayerMask.NameToLayer("Master Layer") && !o.GetComponent<Replicator>())
            {
                o.AddComponent<Replicator>().ui = false;
            }
            else if (o.layer == LayerMask.NameToLayer("UI") && !o.GetComponent<Replicator>())
            {
                o.AddComponent<Replicator>().ui = true;
            }

        }
    }
}
