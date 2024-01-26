using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    [SerializeField] float speed = 0;

    private int count;
    [SerializeField] int maxCount;
    [SerializeField] TextMeshProUGUI countText;

    [SerializeField] GameObject winTextObject;
    [SerializeField] GameObject secretTextObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        secretTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);

        rb.AddForce(movement * speed);

        if (gameObject.transform.position.y < -10)
        {
            gameObject.transform.position = new Vector3(0, .5f, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count++;
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Secret"))
        {
            other.gameObject.SetActive(false);

            count += 5;
            secretTextObject.SetActive(true);
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= maxCount)
        {
            winTextObject.SetActive(true);
        }
    }
}
