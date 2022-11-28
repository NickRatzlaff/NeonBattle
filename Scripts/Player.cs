using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private float directionX;
    
    
    [SerializeField] private float playerSpeed = 450.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float fallForce = 30.0f;

    private string moveAxis;
    private KeyCode jumpKey;
    private KeyCode itemKey;
    private Vector2 startPosition;

    private Inventory inventory;
    [SerializeField] private GameObject inventoryUIObj;
    private InventoryUI inventoryUI;

    private bool grounded = false;

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= PlayerOnGameStateChanged;
    }

    void Start()
    {
        //Configure individual player settings
        if (gameObject.name == "Player 1")
        {
            moveAxis = "Horizontal";
            jumpKey = KeyCode.Space;
            startPosition = new Vector2(-7.0f, 0.0f);
            itemKey = KeyCode.R;
        }
        else if (gameObject.name == "Player 2")
        {
            moveAxis = "AltHorizontal";
            jumpKey = KeyCode.UpArrow;
            startPosition = new Vector2(7.0f, 0.0f);
            itemKey = KeyCode.RightControl;
        }

        GameManager.OnGameStateChanged += PlayerOnGameStateChanged;
        body = gameObject.GetComponent<Rigidbody2D>();
        inventory = new Inventory();
        inventoryUI = inventoryUIObj.GetComponent<InventoryUI>();
    }

    void Update()
    {
        directionX = Input.GetAxisRaw(moveAxis);

        if (Input.GetKeyDown(itemKey))
        {
            inventory.UseItem(gameObject.name);
            Debug.Log(gameObject.name);
            inventoryUI.RemoveItem();
        }
    }

    void FixedUpdate()
    {
        //Move in direction
        body.velocity = new Vector2((directionX * playerSpeed * Time.deltaTime), body.velocity.y);

        //Jump if grounded
        if (Input.GetKey(jumpKey) && grounded)
        {  
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }
        //Quick drop if jump is released
        if (!Input.GetKey(jumpKey) && !grounded)
        {
            body.AddForce(Vector2.down * fallForce);
        }
    }

    private void PlayerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Title:
            {
                break;
            }
            case GameManager.GameState.RoundStart:
            {
                body.transform.position = startPosition;
                body.velocity = Vector2.zero;
                inventory.ClearItems();
                break;
            }
            case GameManager.GameState.Paused:
            {
                break;
            }
            case GameManager.GameState.InGame:
            {
                break;
            }
            case GameManager.GameState.EndGame:
            {
                break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.name.Contains("Floor") || collision.collider.name.Contains("Net"))
        {
            grounded = true;
        }

        if (collision.collider.gameObject.tag == "Item")
        {
            var itemObj = collision.collider.gameObject.GetComponent<ItemObject>();
            inventory.AddItem(itemObj.GetItemType());

            inventoryUI.AddItem(itemObj.GetItemType());
        }
    }

    public void SetSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }
}
