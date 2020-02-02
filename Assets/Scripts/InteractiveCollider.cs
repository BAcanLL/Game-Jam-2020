using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCollider : MonoBehaviour
{
    public List<IInteractable> currentInteractables = new List<IInteractable>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(currentInteractables.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null) 
        {
            //Debug.Log("added " + collision.gameObject);
            currentInteractables.Add(collision.gameObject.GetComponent<IInteractable>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("removed " + collision.gameObject);
        currentInteractables.Remove(collision.gameObject.GetComponent<IInteractable>());
    }

/*    private void OnCollisionStay(Collision collision)
    {
        if (!currentInteractables.Contains(collision.gameObject.GetComponent<IInteractable>()))
        {
            Debug.Log("touching");
            currentInteractables.Add(collision.gameObject.GetComponent<IInteractable>());
        }
    }*/
}
