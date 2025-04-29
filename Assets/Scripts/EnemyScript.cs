using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 3f;

    void Update()
    {
        if (player == null) return;

        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Rotate to face the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0; // Optional: prevent tilting up/down
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (distance > attackRange)
        {
            // Move toward player
            transform.position += direction * moveSpeed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Stop and attack
            animator.SetBool("isWalking", false);
            animator.SetTrigger("Attack");
        }
    }
}