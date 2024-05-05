
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float Velocity;
    [Space]

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

	//****
	[Header("Jumping")]
	public float jumpForce = 4.0f; // The force applied upwards when jumping
	public KeyCode jumpKey = KeyCode.Space; // The key used to trigger a jump

	public GameObject projectilePrefab;
	public Transform shootingPoint; // A point from which the projectiles are instantiated
	public KeyCode shootKey = KeyCode.Mouse0; // Default shoot button, e.g., left mouse click


	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		InputMagnitude ();

        // Handle jumping logic
    	if (Input.GetKeyDown(jumpKey) && isGrounded) {
        	    verticalVel += jumpForce;
    			anim.SetTrigger("Jump"); // Assuming "Jump" is a trigger in your Animator
    	}

    	isGrounded = controller.isGrounded; // Update grounded status
    	if (isGrounded && verticalVel < 0) {
        	verticalVel = -2f; // A small downward force to keep the character grounded
    	} else {
        	verticalVel += Physics.gravity.y * Time.deltaTime; // Apply gravity
    	}

    	moveVector = new Vector3(0, verticalVel * Time.deltaTime, 0);
    	controller.Move(moveVector);

		    // Shooting logic
    	if (Input.GetKeyDown(shootKey))
    	{
        	Shoot();
    	}
    }

    void PlayerMoveAndRotation() {
    	InputX = Input.GetAxis("Horizontal");
    	InputZ = Input.GetAxis("Vertical");

    	var forward = cam.transform.forward;
    	var right = cam.transform.right;

    	forward.y = 0f;
    	right.y = 0f;
    	forward.Normalize();
    	right.Normalize();

    	desiredMoveDirection = forward * InputZ + right * InputX;

    	if (blockRotationPlayer == false) {
        	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    	}
    	if (isGrounded || verticalVel > 0) { // Allow movement in air if jumping
        	controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
    	}
	}

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude() {
		//Calculate Input Vectors
		InputX = Input.GetAxis ("Horizontal");
		//InputZ = Input.GetAxis ("Vertical");

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player

		if (Speed > allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StopAnimTime, Time.deltaTime);
		}
	}

	void Shoot()
	{
    	Debug.Log("Shoot function called.");
    	if (projectilePrefab && shootingPoint)
    	{
        	Debug.Log("Instantiating bullet at: " + shootingPoint.position);
        	GameObject bullet = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        	bullet.transform.forward = transform.forward;  // Ensure it points in the forward direction
    	}
    	else
    	{
        	if (!projectilePrefab) Debug.LogError("Projectile prefab is null.");
        	if (!shootingPoint) Debug.LogError("Shooting point is null.");
    	}
	}
}
