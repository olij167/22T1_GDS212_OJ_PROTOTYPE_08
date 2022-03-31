using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatsCollectedUI : MonoBehaviour
{
    TextMeshProUGUI catCounterText;
    public int catCounter;

    void Start()
    {
        catCounterText = GetComponent<TextMeshProUGUI>();
        //catCounterText.text = "Cats Collected: " + catCounter.ToString();
    }

    public void IncreaseCatCounter()
    {
        catCounter++;
        catCounterText.text = "Cats Collected: " + catCounter.ToString();

    }

    //public void ReturnCats()
    //{
    //    catsReturnedCounter += catCounter;
    //    catCounter = 0;
    //}
}
