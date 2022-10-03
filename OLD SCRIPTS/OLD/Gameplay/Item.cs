using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Arrow,
    Life
}

public class Item : MonoBehaviour {

    [Header("Item")]
    public ItemType itemType;
    public int value;

    [Header("- Config")]
    [SerializeField] private Rigidbody rigidbod;

    private const float forceXZ = 2;
    private const float forceY = 5;

    private void OnEnable()
    {
        Vector3 force = new Vector3()
        {
            x = Random.Range(-forceXZ, forceXZ),
            y = Random.Range(0, forceY),
            z = Random.Range(-forceXZ, forceXZ)
        };

        rigidbod.AddForce(force, ForceMode.Impulse);
    }

    //private void OnTriggerEnter(Collider col) {
    //    if (col.CompareTag(Constants.Tag.PLAYER)) {
    //        col.GetComponent<ICollector>().GetItem(this);
    //        //ReturnToPool();
    //    }
    //}
}
