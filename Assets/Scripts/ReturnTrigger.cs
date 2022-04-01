using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnTrigger : MonoBehaviour
{
    public ControllBall ball;
    CatsCollectedUI catsCollectedUI;
    public int catsReturnedCounter, catsRequiredForUpgrade = 50, currentLevel = 1;
    [HideInInspector] public int maxCatsReturned = 100000;

    public float upgradeSizeIncrease;

    public TextMeshProUGUI catsReturnedText, nextUpgradeText, levelText, popUpText;

    public float popUpTimer, popUpTimerReset;

    public SpawnCats spawnCats;

    public GameObject endUI, gameUI;
    public SphereCollider magnetField;

    public TutorialUIController tutorial;

    public PopUps popUps;
    public GameObject popUpUI;


    void Start()
    {
        catsCollectedUI = ball.catsCollectedUI;

        catsReturnedText.text = "Cats Returned: " + catsReturnedCounter.ToString();

        //popUpText.enabled = false;
        popUpUI.SetActive(false);


        nextUpgradeText.text = "Next Level when " + catsRequiredForUpgrade.ToString() + " cats have been returned";
    }

    private void Update()
    {
        if (popUpUI.activeSelf)
        {
            popUpTimer -= Time.deltaTime;

            if (popUpTimer <= 0f)
            {
                popUpUI.SetActive(false);
            }
        }
        else popUpTimer = popUpTimerReset;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball.gameObject)
        {
            if (catsCollectedUI.catCounter > 0)
            {
                ReturnCats();
            }

           
        }

        magnetField.gameObject.GetComponent<CatMagnet>().attractedCatsList.Clear();

        if (other.gameObject.CompareTag("Cat"))
        {
            Destroy(other.gameObject);
            spawnCats.SpawnNewCats();
        }

        if (catsReturnedCounter >= catsRequiredForUpgrade)
        {
            magnetField.GetComponent<CatMagnet>().magnetRadius = Mathf.Log10(catsReturnedCounter);
            //magnetField.radius = Mathf.Sqrt((Mathf.PI + catsOnBall) / (4 * Mathf.PI));

            catsRequiredForUpgrade *= 2;

            currentLevel++;
            levelText.text = "Level " + currentLevel.ToString();

            nextUpgradeText.text = "Next Upgrade at " + catsRequiredForUpgrade.ToString() + " Cats Returned";
        }
    }

    public void ReturnCats()
    {
        
        catsReturnedCounter += catsCollectedUI.catCounter;


        catsCollectedUI.catCounter = 0;

        catsCollectedUI.catCounterText.text = "Cats Collected: " + catsCollectedUI.catCounter.ToString();


        catsReturnedText.text = "Cats Returned: " + catsReturnedCounter.ToString();

        if (tutorial.tutorialComplete)
        {
            PopUpText();
        }
        
        foreach (GameObject collectedCat in GameObject.FindGameObjectsWithTag("Collected"))
        {

            Destroy(collectedCat.gameObject);
            
        }

        spawnCats.catsInSceneCount = GameObject.FindGameObjectsWithTag("Cat").Length;
        //Debug.Log(spawnCats.catsInSceneCount + " Cats in Scene");


        if (spawnCats.catsInSceneCount <= spawnCats.respawnCatsAtNum)
        {
            //spawnCats.catNumToSpawn = spawnCats.maxCatNum - spawnCats.catsInSceneCount;
            spawnCats.SpawnNewCats();

        }

        if (catsReturnedCounter >= maxCatsReturned)
        {
            Camera.main.transform.LookAt(ball.transform);
            gameUI.SetActive(false);
            endUI.SetActive(true);
        }
    }

    public void PopUpText()
    {

        popUpUI.SetActive(true);
        popUpText.text = popUps.PopUpsList[Random.Range(0, popUps.PopUpsList.Count)];


        

        
    }
}
