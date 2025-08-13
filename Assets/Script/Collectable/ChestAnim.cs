using UnityEngine;

public class ChestAnim : MonoBehaviour
{
    private Animator animator;
    private bool chestOpen = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O) && !chestOpen) OpenChest();
    }

    private void OpenChest()
    {
        chestOpen = true;
        animator.SetBool("IsOpening", true);
    }
}
