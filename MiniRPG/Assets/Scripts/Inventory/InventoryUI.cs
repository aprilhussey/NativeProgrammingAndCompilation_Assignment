using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    InputActions inputActions;
    
    Inventory inventory;

    public Transform itemsParent;
    InventorySlot[] slots;

    public GameObject inventoryUI;

    public GameObject uiEventSystem;
    public GameObject inventoryEventSystem;

   void Awake()
    {
		inputActions = new InputActions();
		inputActions.Enable();
	}

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
            uiEventSystem.SetActive(false);
            inventoryEventSystem.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			inventoryEventSystem.SetActive(false);
			uiEventSystem.SetActive(true);
		}

		if (inputActions.Player.Inventory.triggered || inputActions.Inventory.Player.triggered)
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
