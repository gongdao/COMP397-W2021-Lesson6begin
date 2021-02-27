using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line of sight")]
    public bool HasLOS;
    public Vector3 playerLocation;
    //public LayerMask collisionLayer;
    //public Vector3 LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);

    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //var size = new Vector3(4.0f, 2.0f, 10.0f);
        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, size, transform.forward, out hit,transform.rotation, 0.0f, collisionLayer);


        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, out hit, transform.rotation);
        if (HasLOS)
        {
            //Debug.Log(hit.transform.gameObject.name);
            agent.SetDestination(player.transform.position);
        }

        if(HasLOS && Vector3.Distance(transform.position, player.transform.position) < 2.5f)
        {
            // could be an attack
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
            transform.LookAt(transform.position - player.transform.forward);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)CryptoState.JUMP);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.RUN);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.transform.gameObject;
        }
    }

    /*void OnDrawGizmos()
    {
        var size = new Vector3(4.0f, 2.0f, 10.0f);
        Gizmos.color = Color.white;
        // Gizmos.DrawWireCube(transform.position + LOSoffset, size);
        Gizmos.DrawWireCube(transform.position + LOSoffset + transform.forward, size * 0.5f);
    }*/
}
