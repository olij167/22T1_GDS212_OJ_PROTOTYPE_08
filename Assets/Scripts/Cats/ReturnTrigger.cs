using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnTrigger : MonoBehaviour
{
    public ControllBall ball;
    CatsCollectedUI catsCollectedUI;
    public CatsReturnedCounter catsReturnedCounter;
    public int catsRequiredForUpgrade = 50, currentLevel = 1;
    public int maxCatsReturned = 100000;

    public float upgradeSizeIncrease;

    public TextMeshProUGUI catsReturnedText, nextUpgradeText, levelText, popUpText;

    public float popUpTimer, popUpTimerReset;

    public SpawnCats spawnCats;

    public GameObject endUI, gameUI;
    public SphereCollider magnetField;

    public TutorialUIController tutorial;

    public PopUps popUps;
    public GameObject popUpUI;

    public ParticleSystem fireWorks;

    AudioSource levelUpAudioSource;

    public AudioClip levelUpSound, catsReturnedSound;

    public PlayerController player;
    public CamController cam;

    bool gameContinued;


    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        catsCollectedUI = ball.catsCollectedUI;

        catsReturnedText.text = "Cats Returned: " + catsReturnedCounter.catsReturned.ToString();

        //popUpText.enabled = false;
        popUpUI.SetActive(false);

        levelUpAudioSource = gameObject.GetComponent<AudioSource>();

        if (catsReturnedCounter.catsReturned >= catsRequiredForUpgrade)
        {
            gameContinued = true;
        }

        nextUpgradeText.text = "Next Level when " + catsRequiredForUpgrade.ToString() + " cats have been returned";
    }

    private void Update()
    {
        if (gameContinued && catsReturnedCounter.catsReturned >= catsRequiredForUpgrade)
        {
            GameContinued();
        }
        else gameContinued = false;

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

            if (catsReturnedCounter.catsReturned < catsRequiredForUpgrade)
            {
                if (!levelUpAudioSource.isPlaying)
                    levelUpAudioSource.PlayOneShot(catsReturnedSound);
            }

            else //(catsReturnedCounter.catsReturned >= catsRequiredForUpgrade)
            {
                magnetField.GetComponent<CatMagnet>().magnetRadius = Mathf.Log10(catsReturnedCounter.catsReturned);
                //magnetField.radius = Mathf.Sqrt((Mathf.PI + catsOnBall) / (4 * Mathf.PI));

                catsRequiredForUpgrade *= 2;

                currentLevel++;
                fireWorks.Play();
                levelUpAudioSource.PlayOneShot(levelUpSound);
                levelText.text = "Lure" + "\n" + "Level " + currentLevel.ToString();

                nextUpgradeText.text = "Next Upgrade at " + catsRequiredForUpgrade.ToString() + " Cats Returned";
            }

        }

        magnetField.gameObject.GetComponent<CatMagnet>().attractedCatsList.Clear();

        if (other.gameObject.CompareTag("Cat"))
        {
            Destroy(other.gameObject);
            spawnCats.SpawnNewCats();
        }
    }

    public void ReturnCats()
    {
        
        catsReturnedCounter.catsReturned += catsCollectedUI.catCounter;
        SaveSystem.Save(catsReturnedCounter);

        catsCollectedUI.catCounter = 0;

        catsCollectedUI.catCounterText.text = "Cats Collected: " + catsCollectedUI.catCounter.ToString();


        catsReturnedText.text = "Cats Returned: " + catsReturnedCounter.catsReturned.ToString();

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

        if (catsReturnedCounter.catsReturned >= maxCatsReturned)
        {
            player.enabled = false;
            cam.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            gameUI.SetActive(false);
            endUI.SetActive(true);
            
        }
    }

    public void PopUpText()
    {

        popUpUI.SetActive(true);
        popUpText.text = popUps.PopUpsList[Random.Range(0, popUps.PopUpsList.Count)];
        
    }

    public void GameContinued()
    {
        if (catsReturnedCounter.catsReturned >= catsRequiredForUpgrade)
        {
            magnetField.GetComponent<CatMagnet>().magnetRadius = Mathf.Log10(catsReturnedCounter.catsReturned);

            catsRequiredForUpgrade *= 2;

            currentLevel++;

            levelText.text = "Lure" + "\n" + "Level " + currentLevel.ToString();

            nextUpgradeText.text = "Next Upgrade at " + catsRequiredForUpgrade.ToString() + " Cats Returned";
        }
    }
}
