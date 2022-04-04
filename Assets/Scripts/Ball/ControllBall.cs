using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllBall : MonoBehaviour
{
    public CharacterController player;
    //PlayerController playerController;
    public Transform playerForwardPos;
    //Vector3 playerOffset;
    Vector3 lastPosition;

    Rigidbody rb;

    public float maxDistancefromPlayer;

    public CatsCollectedUI catsCollectedUI;

    AudioSource catSoundSource;

    public List<AudioClip> catSounds;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //playerController = player.GetComponent<PlayerController>();
        lastPosition = new Vector3();
        catSoundSource = gameObject.GetComponent<AudioSource>();
        //playerOffset = new Vector3(player.transform.position.x + maxDistancefromPlayer, 0f , player.transform.position.z + maxDistancefromPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position != lastPosition)
        {
            Vector3 spinDirection = new Vector3(-player.velocity.x, -player.velocity.y, -player.velocity.z);
            rb.AddRelativeTorque(spinDirection, ForceMode.Force);
        }
        else rb.angularVelocity = Vector3.zero;
        

        if (Vector3.Distance(player.transform.position, transform.position) > maxDistancefromPlayer)
        {
            transform.position = new Vector3(playerForwardPos.position.x, playerForwardPos.transform.position.y, playerForwardPos.position.z);
            //player.transform.forward;
        }

        lastPosition = player.transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            collision.transform.parent.parent = transform;
            

            catsCollectedUI.IncreaseCatCounter();
            collision.transform.tag = "Collected";
            collision.transform.parent.tag = "Collected";
            collision.transform.GetComponent<Rigidbody>().useGravity = false;
            collision.transform.GetComponent<Rigidbody>().isKinematic = true;

            catSoundSource.PlayOneShot(catSounds[Random.Range(0, catSounds.Count)]);

            //foreach (GameObject child in collision.transform.parent)
            //{
            //    child.GetComponent<Collider>().enabled = false;
            //}

            collision.transform.GetComponent<Collider>().enabled = false;
        }
    }
}
