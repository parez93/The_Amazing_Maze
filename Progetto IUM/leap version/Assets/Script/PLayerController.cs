using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using Leap;

public class PLayerController : MonoBehaviour {

	public float speed;
	float x = 0;
	float y = 0;
	public Leap.Controller controller;
	public Leap.Frame frame;
	private Rigidbody rb;
	private int count;
    float Timer;
    private string fileName = "C:\\Users\\parez\\Desktop\\Progetto IUM\\ium_project\\Dati\\file.txt";

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		controller = new Leap.Controller ();
		controller.StartConnection ();
		print ("Service:    ");
		print (controller.IsServiceConnected);
		print ("\nController:   ");
		print (controller.IsConnected);
		if (!controller.IsConnected)
			controller.StopConnection ();
    }

    void Update()
    {
        Timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
    }

    void FixedUpdate ()
	{	

		/*
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		*/
		frame = controller.Frame ();
		foreach(Hand mano in frame.Hands)
			if (mano.IsRight) {
				x = mano.PalmPosition.x;
				y = mano.PalmPosition.z;
			}
		Vector3 movement = new Vector3 (x/100, 0.0f, -y/100);

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