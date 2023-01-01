using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public Transform itemsParent;
    InventorySlot[] slots;

    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        bool inventoryUIActive = inventoryUI.activeInHierarchy;
        
        if (inventoryUIActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

		bool inventoryInput = PlayerController.instance.Inventory();

		if (inventoryInput)
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
	}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
