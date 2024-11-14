using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int id;
    public GameObject prefab;

    private void Update()
    {
        if (transform.position.x < -25f || transform.position.x > 25f)
        {
            Manager.instance.ReturnObject(gameObject, id);
        }
    }
}
