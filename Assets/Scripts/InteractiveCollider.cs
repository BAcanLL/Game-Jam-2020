using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (IInteractable interactable in collision.gameObject.GetComponents<IInteractable>())
        {
            currentInteractables.Add(interactable);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (IInteractable interactable in collision.gameObject.GetComponents<IInteractable>())
        {
            currentInteractables.Remove(interactable);
        }
    }

    public bool interactAll()
    {
        bool status = true;

        foreach (IInteractable interactable in currentInteractables)
        {
            if (!interactable.interact(gameObject)) status = false;
        }

        return status;
    }

    public bool interactNext()
    {
        foreach (IInteractable interactable in currentInteractables)
        {
            if (interactable.interact(gameObject))
            {
                Debug.Log(gameObject + " interacted with " + interactable);

                currentInteractables.Remove(interactable);
                currentInteractables.Insert(currentInteractables.Count, interactable);

                return true;
            }
        }

        return false;
    }
}
