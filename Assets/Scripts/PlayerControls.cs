using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;
    public float speed;
    public bool isMoving;
    public Vector2 input;

    private Animator animator;

    public LayerMask solids;
    public LayerMask interact;

    [SerializeField] public GameObject hat;
    public string equippedHat;

    private void Start()
    {
        instance = this;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x != 0) input.y = 0;
            if(input.y != 0) input.x = 0;

            if(input != Vector2.zero)
            {

                animator.SetFloat("move_x", input.x);
                animator.SetFloat("move_y", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);

        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            EquipHat();
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("move_x"), animator.GetFloat("move_y"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
        
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interact);
        if (collider != null)
        {
            collider.GetComponent<Interface_interact>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solids | interact) != null)
        {
            return false;
        }
        return true;
    }

    public void EquipHat()
    {
        bool equipped = false;
        hat.GetComponent<SpriteRenderer>().enabled = false;

        if(!equipped && GameController.instance.itemsHeld[GameController.instance.itemIcon.currentItem - 1] != "")
        {
            hat.GetComponent<SpriteRenderer>().enabled = true;
            hat.GetComponent<SpriteRenderer>().sprite = GameController.instance.GetItemDetails(GameController.instance.itemsHeld[GameController.instance.itemIcon.currentItem - 1]).itemSprite;
            equippedHat = GameController.instance.itemsHeld[GameController.instance.itemIcon.currentItem - 1];
            equipped = true;
        }
        else
        {
            hat.GetComponent<SpriteRenderer>().enabled = false;
            equipped = false;
        }
    }
}