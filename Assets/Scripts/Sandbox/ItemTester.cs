using UnityEngine;


public class ItemTester : MonoBehaviour
{
    [SerializeField]
    private ItemData _item1;
    [SerializeField]
    private ItemData _item2;
    [SerializeField]
    private ItemData _item3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"this is the price: {_item1.Price}");
        Debug.Log($"this is the name: {_item2.name}");
        Debug.Log($"this is the icon: {_item3.Icon}");
    }
}
