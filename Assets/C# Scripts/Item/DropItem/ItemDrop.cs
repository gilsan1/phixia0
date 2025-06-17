//드롭 아이템 스크립트
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private ItemBase itemData;

    public void SetItem(ItemBase data)
    {
        itemData = data;
    }

    public ItemBase GetItem()
    {
        return itemData;
    }

    private void Start()
    {
        // 자동으로 콜라이더와 리지드바디 추가
        if (GetComponent<Collider>() == null)
        {
            SphereCollider col = gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.5f;
        }

        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && itemData != null)
            {
                InventorySystem.Instance.TryAddItem(itemData);
                Destroy(gameObject);
            }
        }
    }
}