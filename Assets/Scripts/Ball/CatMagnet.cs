using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatMagnet : MonoBehaviour
{
    public float magnetForce;

    public GameObject ball;

    public int catsOnBall;

    public List<Rigidbody> attractedCatsList;

    SphereCollider magnetField;
    public float magnetRadius;

    public TextMeshProUGUI baseMagnetText, collectedModifierText, totalMagnetRangeText;
    void Start()
    {
        attractedCatsList = new List<Rigidbody>();
        magnetField = gameObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        catsOnBall = ball.transform.childCount - 1;

        //magnetField.radius = magnetRadius + Mathf.Sqrt((Mathf.PI + catsOnBall) / (4 * Mathf.PI));
        magnetField.radius = (magnetRadius+1) * Mathf.Sqrt((Mathf.PI + catsOnBall) / (4 * Mathf.PI));

        for (int i = 0; i < attractedCatsList.Count; i++)
        {
            
            attractedCatsList[i].velocity = (transform.position - (attractedCatsList[i].transform.position + attractedCatsList[i].centerOfMass)) * magnetForce * Time.deltaTime;
        }

        baseMagnetText.text = "Base Lure Range: " + magnetRadius.ToString("0.00");
        collectedModifierText.text = "Collected Cats Lure Modifier: " + (magnetField.radius - magnetRadius).ToString("0.00");
        totalMagnetRangeText.text = "Total Lure Range: " + magnetField.radius.ToString("0.00");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (!attractedCatsList.Contains(rb))
            {
                attractedCatsList.Add(rb);
            }
        }

        if (other.gameObject.CompareTag("Collected"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (attractedCatsList.Contains(rb))
            {
                attractedCatsList.Remove(rb);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (attractedCatsList.Contains(rb))
            {
                attractedCatsList.Remove(rb);
            }
        }
    }
}
