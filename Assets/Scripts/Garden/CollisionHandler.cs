using UnityEngine;

public class CollisionHandler : Interactable
{
    [SerializeField] private int interactRange;
    private bool touching;
    private GameObject player;

    readonly string HOUSE = "House";
    readonly string SHOP = "Stand";

    private void Awake()
    {
        touching = false;
        player = null;
        if (!(gameObject.name.Equals(HOUSE) || gameObject.name.Equals(SHOP)))
            throw new System.Exception("Need to pass correct name for collision!");
    }

    private void Update()
    {
        if (player != null && !touching)
        {
            float dist = Vector3.Distance(gameObject.transform.position, transform.position) - Vector3.Distance(
                player.transform.position, transform.position);
            if(dist < interactRange)
            {
                player.GetComponent<PlayerController>().CurrentInteractable = null;
                player = null;
            }
        }
    }
    public override void Interact()
    {
        if (!Clock.instance.shopVisited && gameObject.name.Equals(SHOP))
        {
            Clock.instance.shopVisited = true;
             // LoadingManager.instance.ShopScene();    CALL WHEN SHOP CAN NAVIGATE BACK
        } else if(Clock.instance.shopVisited && gameObject.name.Equals(HOUSE))
        {
            Clock.instance.NextDay();
        }
    }

    public override void StartCanInteract()
    {
        this.enabled = true;
    }

    public override void StopCanInteract()
    {
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
        player = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }
}
