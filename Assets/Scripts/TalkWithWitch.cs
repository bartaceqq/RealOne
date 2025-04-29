using UnityEngine;
using UnityEngine.UI;

public class TalkWithWitch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera cofeecam;
    [SerializeField] private EnableText enableText;
    [SerializeField] private Inventory inventory;
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 7f;
    [SerializeField] private Image interactionImage;
    [SerializeField] private Image blackbox;

    private bool isTalking = false;
    private bool firstTalkFinished = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        interactionImage.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Debug.Log("Distance to witch: " + distance.ToString("F2"));

        if (distance < interactionDistance)
        {
            if (!isTalking)
            {
                interactionImage.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isTalking)
                {
                    interactionImage.enabled = false;

                    if (!firstTalkFinished)
                    {
                        // First conversation
                        StartCoroutine(enableText.ReadTextLetterByLetter());
                        firstTalkFinished = true;
                    }
                    else
                    {
                        if (inventory.items.Contains(1))
                        {
                            enableText.LoadNewTextFile("HaveKey");
                            StartCoroutine(enableText.ReadTextLetterByLetter());

                            // Remove the key from inventory
                            inventory.items.Remove(1);

                            // Find the slot with id 1 and reset it
                            foreach (SlotScript slot in FindObjectsOfType<SlotScript>())
                            {
                                if (slot.id == 1)
                                {
                                    slot.Reset();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            enableText.LoadNewTextFile("DontHaveKey");
                            StartCoroutine(enableText.ReadTextLetterByLetter());
                        }
                    }

                    Debug.Log("Talking started.");
                    mainCamera.enabled = false;
                    cofeecam.enabled = true;
                    animator.SetTrigger("Talk");
                    isTalking = true;
                }
                else
                {
                    interactionImage.enabled = true;
                    animator.ResetTrigger("Talk");
                    isTalking = false;
                    mainCamera.enabled = true;
                    cofeecam.enabled = false;
                }
            }
        }
        else
        {
            interactionImage.enabled = false;
            isTalking = false;
        }
    }
}
