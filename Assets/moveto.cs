using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI
;

public class moveto : MonoBehaviour
{  
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    public float stopDistance = 0.1f;
    public List<GameObject> targets;
    public int targetIndex = 0; 


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        
        if(Vector3.Distance(transform.position, target.transform.position) < stopDistance)
        {
            animator.SetBool("isrunning", false);
            targetIndex++; 
            
            if(targetIndex >= targets.Count)
            {
                targetIndex = 0;
            }
            target = targets[targetIndex]; 

        }
        else
        {
            animator.SetBool("isRunning", true);
        }
       
    }
}
