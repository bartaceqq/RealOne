using UnityEngine;

public class TalkWithShop : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField]private Animator animator;

    private GameObject shopwoman;
    private Transform shopwomantransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopwomantransform = GameObject.FindGameObjectWithTag("ShopGirl").transform;
        shopwoman = GameObject.FindGameObjectWithTag("ShopGirl");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, shopwoman.transform.position);
        Debug.Log(distance + "distrance from shoppppppppppppppppppppppppppppppppppppp");
        
    }
}
