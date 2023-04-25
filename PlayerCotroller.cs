using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCotroller : MonoBehaviour
{
    public float speed;
    public int hostsCount = 0;

    //light
    public GameObject spotLight;
    private Animator animator;

    public bool hasAllHosts = false;

    public PowerUps power;

    //SFX
    public AudioSource HitSFX;
    public AudioSource HitHostSFX;
    public AudioSource pickUpSFX;
    public AudioSource footStepsSFX;
    public AudioSource speedSFX;


    private void Start()
    {
        animator = GetComponent<Animator>();
        
        power = PowerUps.NONE;
    }


    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        var leftIn = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var rightIn = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        var upIn = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var downIn = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        if (leftIn)
        {

            dir.x = -1;
            animator.SetInteger("Direction", 3);
            spotLight.transform.rotation = Quaternion.Euler(0,0,90);
            footStepsSFX.enabled = true;
        }
        else if (rightIn)
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
            spotLight.transform.rotation = Quaternion.Euler(0, 0, -90);
            footStepsSFX.enabled = true;
        }

        if (upIn)
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
            spotLight.transform.rotation = Quaternion.Euler(0, 0, 0);
            footStepsSFX.enabled = true;
        }
        else if (downIn)
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
            spotLight.transform.rotation = Quaternion.Euler(0, 0, 180);
            footStepsSFX.enabled = true;
        }
        if(rightIn || leftIn || upIn|| downIn)
            footStepsSFX.enabled = true;
        else
            footStepsSFX.enabled = false;


        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }

    internal void springeObj(GameObject objToSpring)
    {
        HitHostSFX.Play();
        SpringJoint2D hinge = objToSpring.GetComponent<SpringJoint2D>();
        hinge.enabled = true;
        hinge.connectedBody = GetComponent<Rigidbody2D>();
        hinge.connectedAnchor = transform.InverseTransformPoint(objToSpring.transform.position);

        hostsCount++;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hostages"))
        {
            springeObj(other.gameObject);
        }
    }

}
