using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int default_speed;
    private int current_speed;

    public bool canBePressed;

    public KeyCode keyCode;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        current_speed = default_speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, current_speed * Time.deltaTime, 0f);

        if (Input.GetKeyDown(keyCode) && canBePressed)
        {
            gameObject.SetActive(false);
            GameController.gameController.NoteHit();

        }

        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBePressed = false;
        if (gameObject.activeInHierarchy)
        {
            GameController.gameController.NoteMiss();
        }

    }
}
