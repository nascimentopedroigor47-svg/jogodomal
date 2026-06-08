using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private Collider geralCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool isRagdoll = false;


    void Start()
    {

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }


    void Update()
    {

    }

    private void ToggleRagdoll(bool isAnimation)
    {
        isRagdoll = !isAnimation;
        myCollider.enabled = isAnimation;

        foreach (Rigidbody ragdollBody in rigidbodies)
        {
            ragdollBody.isKinematic = isAnimation;

            GetComponent<Animator>().enabled = isAnimation;

            if (isAnimation) RandomAnimation();

        }

    }

    void RandomAnimation()
    {

        int randomNum = UnityEngine.Random.Range(0, 2);

        Animator animator = GetComponent<Animator>();

        if (randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !isRagdoll)
        {

            ToggleRagdoll(false);

            StartCoroutine(GetBackup());

            geralCollider.enabled = false;

        }
    }

    private IEnumerator GetBackup()
    {
        yield return new WaitForSeconds(respawnTime);
        geralCollider.enabled = true;
        ToggleRagdoll(true);
    }



}