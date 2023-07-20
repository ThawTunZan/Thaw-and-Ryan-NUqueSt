using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangingLayerForTorch : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer torchRenderer;
    // Start is called before the first frame update
    void Start()
    {
        torchRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        torchRenderer.sortingOrder = 5;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        torchRenderer.sortingOrder = 15;
    }
}
