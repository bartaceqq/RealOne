using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwordController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private EnemyScript EnemyScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }
}
