using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public Inventory inventory;
    public int id;
    public string itemName;
    public Sprite defsprite; // Default sprite
    public bool occupied = false;
    public Image slotUIImage;

    public void SetItem(Sprite newSprite, int ide)
    {
        Debug.Log("Item added to slot: " + ide);

        if (slotUIImage != null)
        {
            slotUIImage.sprite = newSprite;
            slotUIImage.enabled = true;
        }

        occupied = true;
        id = ide;

        if (inventory != null)
        {
            if (!inventory.items.Contains(ide))
                inventory.items.Add(ide);
        }
    }

    public void Reset()
    {
        Debug.Log("Slot reset!");

        if (slotUIImage != null && defsprite != null)
        {
            slotUIImage.sprite = defsprite;
            slotUIImage.enabled = true; // Keep it enabled but show default sprite
        }

        id = 0;
        itemName = "";
        occupied = false;
    }
}