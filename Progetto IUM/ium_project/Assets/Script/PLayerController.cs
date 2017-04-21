using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

public class PLayerController : MonoBehaviour {

	public float speed;


	private Rigidbody rb;
	private int count;
    float Timer;
    private string fileName = "C:\\Users\\parez\\Desktop\\Progetto IUM\\ium_project\\Dati\\file.txt";

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;

    }

    void Update()
    {
        Timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
    }

    void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "PickUp"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
		}

        if (other.gameObject.CompareTag("CapsuleLast") && count >= 4)
        {
            //Stoppa il tempo e salvalo su file
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

	void SetCountText ()
	{
		if (count >= 5)
		{
            File.AppendAllText(fileName, Timer + Environment.NewLine);
            //Debug.LogError("Time: " + Timer + "sec");
            Application.LoadLevel("end");
            //Dire cosa succede
        }

	}
}