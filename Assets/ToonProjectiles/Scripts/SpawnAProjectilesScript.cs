using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAProjectilesScript : MonoBehaviour {


	public bool spawnRandom;
	public MovePlayer movePlayer;
	public GameObject firePoint;
	private GameObject effectToSpawn;
	public List<GameObject> VFXs = new List<GameObject> ();
	private float timeToFire = 0f;

    private void Start()
    {

		movePlayer.StartUpdateRay();
		
	}
    void Update () 
	{
        if (VFXs.Count > 0)
        {
			if (VFXs.Count > 0)
			{
                if (spawnRandom)
                {
					int rand = Random.Range(0, VFXs.Count);
					effectToSpawn = VFXs[rand];
				}
                else
                {
					effectToSpawn = VFXs[0];
				}
			}

            else
            {
				Debug.Log("Please assign one or more VFXs in inspector");
			}
				
			if (Input.GetKey(KeyCode.Space) && Time.time >= timeToFire || Input.GetMouseButton(0) && Time.time >= timeToFire) {
                timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
				SpawnVFX();
				
            }
		}
	}
	
	public void SpawnVFX () {
		GameObject vfx;
		if (firePoint != null) {
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);
			if(movePlayer != null){
				vfx.transform.localRotation = movePlayer.GetRotation ();
			} 
			else Debug.Log ("No RotateToMouseScript found on firePoint.");
		}
		else
			vfx = Instantiate (effectToSpawn);

		var ps = vfx.GetComponent<ParticleSystem> ();

		if (vfx.transform.childCount > 0) {
			ps = vfx.transform.GetChild (0).GetComponent<ParticleSystem> ();
		}
	}

	
}
