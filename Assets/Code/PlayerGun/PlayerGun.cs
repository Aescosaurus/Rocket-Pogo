using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float damage = 10f;
    public float impactForce = 30f;

    public Camera fpsCam;

    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }


    void Shoot()
    {
        
        Ray ray = new Ray(transform.position + fpsCam.transform.forward * 2, fpsCam.transform.forward);
        RaycastHit hit;
        //you can also add a range if you want, just add the variable at the top.
        if (Physics.Raycast(ray, out hit, 9000, ~LayerMask.GetMask("player")))
        {
            //Debug.Log(hit.transform.name);
            Debug.DrawLine(ray.origin, hit.point, Color.green, 300f);
            //get the info for a target
            //if (hit.transform.gameObject.tag == "enemy")
            //{
            Target target = hit.transform.GetComponent<Target>();
            //if we hit a target, do the damage.
            if (target != null)
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 300f);
                target.TakeDamage(damage);
                Debug.Log(target.health);
            }
            //}

            //moves the rigidbody DISABLE IF IT BREAKS MY GAME
            //also a great place to do your gravity changing code
            //MAKE SURE TO SPECIFY IF THE FLOOR IS A MOVE ABLE OBJECT OR
            //NOT WITH A TAG. LIKE && gameobject.tag("movable")
            /*if (hit.rigidbody == null)
            {
                //moves the object a little bit.
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }*/
        }
        /*
         * void FixedUpdate() 
         * {

            RaycastHit cameraHit;
            Ray cameraAim = playerCamera.GetComponent < Camera > ().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 ));
            Physics.Raycast(cameraAim, out cameraHit, 100f);
            Debug.DrawLine(cameraAim.origin, cameraHit.point, Color.green);
            Vector3 cameraHitPoint = cameraHit.point;
            float cameraDistance = cameraHit.distance;

            if (Physics.Raycast(cameraAim, out cameraHit)){
                // something was hit
                RaycastHit playerHit;
                Physics.Raycast(transform.position + new Vector3 (0f, 1.8f, 0f), cameraHitPoint-transform.position - new Vector3 (0f, 1.8f, 0f), out playerHit, 100f);
                Debug.DrawRay(transform.position + new Vector3 (0f, 1.8f, 0f), cameraHitPoint-transform.position - new Vector3 (0f, 1.8f, 0f), Color.red);

                float playerDistance = playerHit.distance;
                Debug.Log ("Distance from player: "+ playerDistance);
                Debug.Log ("Distance from camera: "+ cameraDistance);
            }

        }
         */
    }
}
