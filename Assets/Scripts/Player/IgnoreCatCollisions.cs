using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCatCollisions : MonoBehaviour
{
    public int playerLayer, catLayer, catPartsLayer;
    private void Start()
    {
        Physics.IgnoreLayerCollision(playerLayer, catLayer);
        Physics.IgnoreLayerCollision(playerLayer, catPartsLayer);

    }
}
