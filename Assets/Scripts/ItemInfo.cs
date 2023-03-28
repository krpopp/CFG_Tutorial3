using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{

    public string itemName;
    public int itemValue;

    PlayerControl playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    void OnMouseOver()
    {
        if (!playerScript.hasKey)
        {
            playerScript.itemText.text = itemName;
        }
    }

    void OnMouseExit()
    {
        playerScript.itemText.text = "nothing!";
    }

    void OnMouseDown()
    {
        playerScript.hasKey = true;
        playerScript.itemText.text = "nothing!";
        Destroy(gameObject);
    }

}
