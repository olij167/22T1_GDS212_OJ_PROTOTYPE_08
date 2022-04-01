using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;


public class TutorialUIController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialInstructionsText;
    
    public List<TutorialUI> tutorialUIList;
    public int count = -1;

    public bool tutorialComplete, inputPerformed;

    public float timePerInstruction, instructionTimer;

    //Vector3 lastMousePos = Vector3.zero;

    //public AudioSource voiceOverSource;

    private void Start()
    {

        //lastMousePos = Input.mousePosition;
        //voiceOverSource = GetComponent<AudioSource>();

        //voiceOverSource.clip = tutorialUIList[0].voiceOver;
        //voiceOverSource.Play();
    }

    private void Update()
    {
        instructionTimer -= Time.deltaTime;

        if (instructionTimer <= 0f && !tutorialComplete)
        {
            count++;
            ProgressTutorial();
        }

        if (tutorialComplete)
        {
            foreach (TutorialUI tutorialUI in tutorialUIList)
            {
                tutorialUI.tutorialStepComplete = false;
                tutorialInstructionsText.enabled = false;
                tutorialPanel.SetActive(false);

            }

            enabled = false;
            //gameObject.SetActive(false);
            
        }

       
    }

    void ProgressTutorial()
    {
        if (count >= tutorialUIList.Count)
        {
            tutorialComplete = true;
        }

        instructionTimer = timePerInstruction;

        //tutorialIcon.sprite = tutorialUIList[count].tutorialIcon;
        tutorialInstructionsText.text = tutorialUIList[count].tutorialInstructions;

        if (instructionTimer >= 0f)
        {
            tutorialUIList[count].tutorialStepComplete = true;
            //instructionTimer = timePerInstruction;
        }

    }




    //void Update()
    //{
    //    if (!tutorialComplete)
    //    {



    //        if (!tutorialUIList[count].isLookAroundTutorial)
    //        {
    //            foreach (KeyCode tutorialInput in tutorialUIList[count].tutorialInputList)
    //            {
    //                if (Input.GetKeyDown(tutorialInput))
    //                {
    //                    inputPerformed = true;

    //                }
    //            }

    //            if (inputPerformed)
    //            {
    //                if (instructionTimer <= 0f)
    //                {
    //                    tutorialUIList[count].tutorialStepComplete = true;

    //                    if (tutorialUIList[count + 1] != null)
    //                    {
    //                        count++;
    //                    }
    //                    else tutorialComplete = true;
    //                }
    //                inputPerformed = false;
    //            }
    //        }
    //    }


            //    //if (tutorialUIList[i].isLookAroundTutorial)
            //    //{
            //    //    voiceOverSource.clip = tutorialUIList[i].voiceOver;
            //    //    voiceOverSource.Play();

            //    //    Vector3 mouseDelta = Input.mousePosition - lastMousePos;

            //    //    if (mouseDelta.x != 0)
            //    //    {
            //    //        inputPerformed = true;


            //    //    }


            //    //    if (inputPerformed)
            //    //    {
            //    //        if (!voiceOverSource.isPlaying)
            //    //        {
            //    //            tutorialUIList[i].tutorialStepComplete = true;
            //    //            if (tutorialUIList[count + 1] != null)
            //    //            {
            //    //                count++;
            //    //            }
            //    //            else tutorialComplete = true;
            //    //        }
            //    //        inputPerformed = false;
            //    //    }
            //    //}
            //    //}



            //}
}
