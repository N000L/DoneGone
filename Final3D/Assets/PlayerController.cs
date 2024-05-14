using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool smoothTransition = false;
    public float transitionSpeed = 10f;
    public float transitionRotationSpeed = 500f;
    public GameObject battleSystem;

    private Vector3 targetGridPos;
    private Vector3 prevTargetGridPos;
    private Vector3 targetRotation;

    public Collider[] hitColliders;
    public GameObject colliderChecker;
    public bool moveCheck;
    public int stepCount;

    public void RotateLeft()
    {
        if (AtRest)
        {
            targetRotation -= Vector3.up * 90f;
        }
    }
    
    public void RotateRight()
    {
        if (AtRest)
        {
            targetRotation += Vector3.up * 90f;
        }
    }

    public void MoveForward()
    {
        if (AtRest)
        {
            if (moveCheck)
            {
                targetGridPos += transform.forward;
                stepCount += 1;
            }
        }
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);
    }

    
    void FixedUpdate()
    {
        if (battleSystem.GetComponent<BattleSystem>().state == BattleState.NOTSTARTED)
        {
            MovePlayer();
        }
        
        hitColliders = Physics.OverlapSphere(colliderChecker.transform.position, 0.25f);
        if (hitColliders.Length > 0)
        {
            if (hitColliders[0].gameObject.tag == "Wall")
            {
                moveCheck = false;
            }
            
        }
        else
        {
            moveCheck = true;
        }
    }

    void MovePlayer()
    {
        if (true)
        {
            prevTargetGridPos = targetGridPos;

            Vector3 targetPosition = targetGridPos;

            if (targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0f;
            if (targetRotation.y < 0f) targetRotation.y = 270f;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),
                Time.deltaTime * transitionRotationSpeed);
        }
        else
        {
            //targetGridPos = prevTargetGridPos;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision"); 
    }
    
    bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
