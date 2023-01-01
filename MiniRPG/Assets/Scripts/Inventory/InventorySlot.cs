using UnityEngine;
using UnityEngine.UI;

// Updates the UI on the slot
// Functions that happen when interacting with an inventory slot

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;

	public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void DropItem()
    {
        if (item != null)
        {
            item.Drop();
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
