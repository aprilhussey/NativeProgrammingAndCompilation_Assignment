using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject prefab = null;

    public virtual void Use()
    {
		// Use the item
		// Something might happen
		Debug.Log("Using " + name);
        Inventory.instance.Remove(this);
    }

    public void Drop()
    {
		Inventory.instance.Remove(this);

        Debug.Log("Dropping " + name);
	}
}
