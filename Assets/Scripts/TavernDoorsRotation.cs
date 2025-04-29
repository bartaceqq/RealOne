using UnityEngine;

public class TavernDoorsRotation : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField]private GameObject doors;

    private float distance;
    private bool opened = false;
    private bool isInCooldown = false;

    void Start()
    {
        if (doors == null)
        {
            Debug.LogWarning("No object found with tag 'TavernDoors'");
        }
    }

    void Update()
    {
        if (doors != null && player != null)
        {
            distance = Vector3.Distance(player.transform.position, doors.transform.position);
            Debug.Log("Distance to tavern doors: " + distance.ToString("F2"));

            if (distance < 5f && Input.GetKeyDown(KeyCode.E) && !isInCooldown)
            {
                if (!opened)
                {
                    animator.SetBool("Opened", true);
                    opened = true;
                }
                else
                {
                    animator.SetBool("Opened", false);
                    opened = false;
                }

                // Start cooldown after each interaction
                StartCoroutine(DoorCooldown());
            }
        }
    }

    private System.Collections.IEnumerator DoorCooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(2f);  // Delay between opening/closing
        isInCooldown = false;
    }
}