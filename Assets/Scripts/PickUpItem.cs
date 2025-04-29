using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    private float distance;

    [SerializeField] private int id;
    [SerializeField] private GameObject Item;
    [SerializeField] private GameObject Player;
    [SerializeField] private SlotScript[] slots;
    [SerializeField] private Image pressEimage; // This is the item-specific "Press E" image
    [SerializeField] private Sprite slotImage;
    [SerializeField] private Camera mainCamera;

    private bool pickedUp = false;

    private bool isLookingAtItem;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (Item == null)
        {
            Item = this.gameObject;
        }

        if (pressEimage != null)
            pressEimage.enabled = false;
    }

    private void Update()
    {
        distance = Vector3.Distance(Item.transform.position, Player.transform.position);

        isLookingAtItem = false;

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.transform.root == Item.transform.root)
            {
                isLookingAtItem = true;
            }
        }

        if (distance < 3f && isLookingAtItem && !pickedUp)
        {
            if (pressEimage != null)
            {
                pressEimage.enabled = true; // Show the "Press E" image
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!slots[i].occupied)
                    {
                        slots[i].SetItem(slotImage, id);
                        slots[i].id = id;
                        pickedUp = true;
                        Destroy(Item);
                        if (pressEimage != null)
                        {
                            pressEimage.enabled = false; // Hide the "Press E" image when picked up
                        }
                        break;
                    }
                }
            }
        }
        else if (pressEimage != null)
        {
            pressEimage.enabled = false; // Hide the image when not looking at the item
        }
    }
}
