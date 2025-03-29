using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";
    private Animator animator;
    public Players player; // —сылка на конкретного игрока

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 moveInput = player.GetMoveInput();
            animator.SetBool(IS_RUNNING, player.IsRunning());
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }
}
