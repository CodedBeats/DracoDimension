using UnityEngine;


public class ItemTester : MonoBehaviour
{
    [SerializeField]
    private ItemData _item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"this is the price: {_item.Price}");
    }
}
