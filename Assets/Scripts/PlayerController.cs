using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Controller
{
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode ActionKey;
    public KeyCode ShootKey;


    private GameObject holdSlot;

    protected override Vector2 MoveVector => CollectMoveVector();

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(MoveVector);
        if (Input.GetKeyDown(ActionKey))
            Action();
        if (Input.GetKeyDown(ShootKey))
            Shoot();
    }

    void Move(Vector2 direction)
    {
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity,
                                                direction * runMaxSpeed,
                                                runAcceleration * Time.deltaTime);
    }

    void Action()
    {
        if (holdSlot != null)
        {
            Drop(holdSlot);
            return;
        }

        IInteractable nearest = collector.NearestInteractable;
        if (nearest != null)
            nearest.Interact(this);
    }

    void Shoot()
    {
        Debug.Log("Тут будет выстрел");
    }

    Vector2 CollectMoveVector()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(UpKey)) dir += Vector2.up;
        if (Input.GetKey(DownKey)) dir += Vector2.down;
        if (Input.GetKey(LeftKey)) dir += Vector2.left;
        if (Input.GetKey(RightKey)) dir += Vector2.right;

        return dir.normalized;
    }

    public override void Raise(GameObject go)
    {
        go.transform.parent = HoldPoint;
        go.transform.localPosition = Vector3.zero;
        holdSlot = go;
    }

    public override void Drop(GameObject go)
    {
        if (go == null) return;

        go.transform.parent = null;
        go.TryGetComponent<Collider2D>(out Collider2D collider);
        collider.enabled = true;
        holdSlot = null;
    }

    public override void Throw(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
