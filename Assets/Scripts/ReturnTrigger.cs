using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnTrigger : MonoBehaviour
{
    public ControllBall ball;
    CatsCollectedUI catsCollectedUI;
    public int catsReturnedCounter, catsRequiredForUpgrade = 100;

    public float upgradeSizeIncrease;

    public TextMeshProUGUI catsReturnedText, nextUpgradeText;
    void Start()
    {
        catsCollectedUI = ball.catsCollectedUI;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball.gameObject)
        {
            if (catsCollectedUI.catCounter > 0)
            {
                ReturnCats();

                if (catsReturnedCounter >= catsRequiredForUpgrade)
                {
                    ball.transform.localScale = new Vector3(ball.transform.localScale.x + .25f, ball.transform.localScale.y + 2, ball.transform.localScale.z + 2);

                    catsRequiredForUpgrade *= 2;
                }
            }
        }
    }

    public void ReturnCats()
    {
        catsReturnedCounter += catsCollectedUI.catCounter;
        catsCollectedUI.catCounter = 0;

        catsReturnedText.text = "Cats Returned: " + catsReturnedCounter.ToString();

        nextUpgradeText.text = "Next Upgrade at " + catsRequiredForUpgrade.ToString() + " Cats Returned";
        
        foreach (Transform collectedCat in ball.transform)
        {
            Destroy(collectedCat.gameObject);
        }
    }
}
