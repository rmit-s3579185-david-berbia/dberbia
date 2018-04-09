using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script by David Berbia s3579185
public class RatMovement : MonoBehaviour
{

	// Floating point variable to store the player's movement speed.
	public float speed;
	public float moveHorizontal;
	public float moveVertical;
	public float yAxis;
	public float yRotation;
	public Vector3 movement;
	public Vector3 eularAngleVel;
	public Rigidbody rb;
	public float rotate;
	public float tilt;

    public Transform leaderPosition;


    //our leaders gun
    public GameObject bullet;
    public Transform bulletPos;

    // Other scripts
    public bool LeaderAttacking;

 


	void Awake()
	{

		//Set Rigidbody
		rb = GetComponent<Rigidbody>();
      
        
	}

	// I dont use update in Unity, 
	// I prefer FixedUpdate as it is consistent
	void FixedUpdate()
	{
        
		
        
        //Set movement parameters
		//Horizontal
		moveHorizontal = Input.GetAxis("Horizontal");

		//Vertical
		moveVertical = Input.GetAxis("Vertical");

		//Y Axis for 3D
		yAxis = GetComponent<Rigidbody>().position.y;
        Quaternion from = Quaternion.Euler(moveVertical, yAxis, moveHorizontal);
        Quaternion toRight = Quaternion.Euler(moveVertical, 90f, moveHorizontal);
        Quaternion toLeft = Quaternion.Euler(moveVertical, -90f, moveHorizontal);
        Quaternion toBack = Quaternion.Euler(moveVertical, 180f, moveHorizontal);
        //Used to rotate Y Axis
        yRotation = GetComponent<Rigidbody>().rotation.y;

		//set Movements as Vector3
		movement = new Vector3(moveHorizontal,yRotation,moveVertical);

		//Make object move using Rigidbody
		GetComponent<Rigidbody>().velocity = movement * speed;

		//Create a wobble affect when moving, like a slide/skid
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);


       
        



		//------------------------------------UP---------------------------------------//
		if (moveVertical > 0.01)
		{
			movement = new Vector3 (moveHorizontal, yRotation, moveVertical);
			GetComponent<Rigidbody> ().velocity = movement * speed;
           
		} 
		//------------------------------------DOWN---------------------------------------//
		else if (moveVertical < -0.01) 
		{
			movement = new Vector3 (moveHorizontal, yRotation, moveVertical);
			GetComponent<Rigidbody> ().velocity = movement * speed;
           // transform.rotation = Quaternion.Lerp(from, toBack, Time.time * 5);

        }

		//------------------------------------RIGHT---------------------------------------//
		if(moveHorizontal>0.1) 
		{
			//transform.RotateAround (transform.position,transform.up,Time.deltaTime * 90f);
			//transform.Rotate (0, 90, 0);
			movement = new Vector3(moveHorizontal,yRotation, moveVertical);
            //transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
            GetComponent<Rigidbody>().velocity = movement * speed;
          //  transform.rotation = Quaternion.Lerp(from, toRight, Time.time * speed);

        }
		//------------------------------------LEFT---------------------------------------//
		else if(moveHorizontal<-0.1) 
		{ 
			//transform.RotateAround (transform.position,transform.up,Time.deltaTime * -90f);
			//transform.Rotate (0, -90, 0);
			movement = new Vector3(moveHorizontal,yRotation,moveVertical);
            //transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
           GetComponent<Rigidbody>().velocity = movement * speed;
         //   transform.rotation = Quaternion.Lerp(from, toLeft, Time.time * speed);
        }


        //-------------------TEST BUTTON 1----------------------------------------//

        if (Input.GetKey(KeyCode.Space))
        {
           // print("leader is attacking");

            //Shooting bullets
            Fire();

            // LeaderAttacking = true;
          //  FollowerAI.LeaderIsAttacking = true;
            
            

        }
        //-------------------TEST BUTTON 1----------------------------------------//

       
        
    }


    public void Fire()
    {
        //shoot a bullet
        //you need the position from where the bullet comes from
        GameObject bullet1 = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        bullet1.GetComponent<Rigidbody>().AddForce(bulletPos.transform.forward * 300);
        bullet1.GetComponent<Rigidbody>().velocity = bulletPos.transform.forward * 20;
      
        //and some force to move it along a path
        //and we need to destroy the bullet so it doesnt clog up our game
        //done in script attached to prefab bullet in prefabs folder
    }




} // END MOUSEMOVEMENT