using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour
{
    [Range(0,100)]
    public float chancesToAppear;

    public Sprite[] possibleSprites;

    private void Awake()
    {
        SpriteRenderer _SR = GetComponent<SpriteRenderer>();

        if(Random.value * 100 >chancesToAppear)
        {
            gameObject.SetActive(false);
        }

        _SR.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
    }
}
