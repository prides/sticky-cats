using UnityEngine;

public class DragRigidbody : MonoBehaviour
{
    public float forceAmount = 500;

    [SerializeField]
    ProtoGameManager gameManager;
    [SerializeField, TagSelector]
    string dragableTag;

    [SerializeField]
    Rigidbody2D selectedRigidbody;
    [SerializeField]
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //Check if we are hovering over Rigidbody, if so, select it
            selectedRigidbody = GetRigidbodyFromMouseClick();
            if (selectedRigidbody != null)
            {
                gameManager.CatSelected(selectedRigidbody.gameObject);
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            //Release selected Rigidbody if there any
            Release();
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    public void Release()
    {
        selectedRigidbody = null;
    }

    Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Collider2D collider = Physics2D.OverlapPoint(targetCamera.ScreenToWorldPoint(Input.mousePosition));
        if (collider == null)
        {
            return null;
        }

        if (collider.tag != dragableTag)
        {
            return null;
        }

        return collider.GetComponent<Rigidbody2D>();
    }
}
