using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    private float distance;
    
    [SerializeField] private int id;
    [SerializeField] private GameObject Item;
    [SerializeField] private GameObject Player;
    [SerializeField] private SlotScript[] slots;
    [SerializeField] private Image pressEimage;
    [SerializeField] private Sprite slotimage;
    [SerializeField] private Camera mainCamera;

    private bool pickedUp = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (Item == null)
        {
            Item = this.gameObject; // 👈 Set itself if not assigned
        }
        /*
        pressEimage.enabled = false;
        */
    }


    void Update()
    {
        distance = Vector3.Distance(Item.transform.position, Player.transform.position);
        if (id == 2)
        {
            Debug.Log("Distance to item: " + distance.ToString("F2") + "ttttttttttttttttttttttttttttttttttttt");
        }

        bool isLookingAtItem = false;

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            if (hit.collider.transform.root == Item.transform.root)
            {
                isLookingAtItem = true;
            }
        }


        if (distance < 3f && isLookingAtItem)
        {
            Debug.Log("Item is looking at");
            if (!pickedUp)
            {
                pressEimage.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!slots[i].occupied)
                    {
                        slots[i].SetItem(slotimage, id);
                        slots[i].id = id;// Pass both sprite + name!
                        pickedUp = true;
                        Destroy(Item);
                        pressEimage.enabled = false;
                        break; // Only add into one free slot
                    }
                }
            }
        }
        else
        {
            if (!pickedUp)
            {
                /*
                pressEimage.enabled = false;
                */
            }
        }
    }
}