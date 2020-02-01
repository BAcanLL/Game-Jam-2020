using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class UI
{
    public abstract void UpdateUI(float deltaTime);
};
public class PlayerUI : MonoBehaviour
{
    public GameObject player;

    List<UI> UIs;
    bool initialized;

    // Start is called before the first frame update
    void Start()
    {
        UIs = new List<UI>();
        initialized = false;
    }

    public void InitializeUI()
    {
        if (player)
        {
            InventoryManager inv = player.GetComponent<InventoryManager>();
            if (inv)
            {
                GameObject o = new GameObject("Inventory UI");
                o.transform.parent = transform;
                UI ui = new InventoryManager.InventoryUI(o, inv);
                UIs.Add(ui);
            }
        }
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            InitializeUI();
        }
        foreach (UI ui in UIs)
        {
            ui.UpdateUI(Time.deltaTime);
        }
    }
}
