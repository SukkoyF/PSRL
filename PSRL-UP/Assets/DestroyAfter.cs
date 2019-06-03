using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float lifeTime;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
}
