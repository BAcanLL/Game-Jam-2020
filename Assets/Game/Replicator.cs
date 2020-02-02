using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ui;
    GameObject replica;
    SpriteRenderer replicaRenderer;

    bool multiSprite;
    void Start()
    {
        multiSprite = GetComponent<ReverseSprite>();
        if (GetComponent<RectTransform>())
        {
            replica = new GameObject("UI Replica", typeof(RectTransform));
        }
        else
        {
            replica = new GameObject();
        }

        if (GetComponent<Renderer>())
        {
            replicaRenderer = replica.AddComponent<SpriteRenderer>();
        }
        replica.layer = LayerMask.NameToLayer("Clone Layer");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.parent.GetComponent<Replicator>())
        {
            replica.transform.SetParent(transform.parent.GetComponent<Replicator>().replica.transform);
        }
        else
        {
            replica.transform.SetParent(transform.parent);
        }
        replica.SetActive(gameObject.activeSelf);

        if (ui)
        {
            replica.transform.localPosition = transform.localPosition;
            replica.transform.rotation = transform.rotation;
            if (replicaRenderer)
            {
                replicaRenderer.flipX = GetComponent<SpriteRenderer>().flipX;
            }
        }
        else
        {
            replica.transform.position = new Vector3(-transform.position.x, -transform.position.y, transform.position.z);
            replica.transform.rotation = Quaternion.Inverse(transform.rotation);
            if (replicaRenderer)
            {
                replicaRenderer.flipX = !GetComponent<SpriteRenderer>().flipX;
            }
        }
        replica.transform.localScale = transform.localScale;

        replica.SetActive(gameObject.activeSelf);

        if (replicaRenderer)
        {
            replicaRenderer.color = GetComponent<SpriteRenderer>().color;

            if (multiSprite)
            {
                replicaRenderer.sprite = GetComponent<ReverseSprite>().GetReverseSprite();
            }
            else
            {
                replicaRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}
