using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float clickRadius = 1.0f;

    private Camera camera;
    private PlayerController currentPlayer;
    private IControllable controlled;

    private bool controlling;
    private bool clicked;
    private bool touchControls;

    private int controllableLayerMask;

    private void FindControllable()
    {
        //Get world position of cursor/touch
        Vector2 castPosition;
        if (touchControls)
        {
            castPosition = Input.GetTouch(0).position;
        }
        else
        {
            castPosition = Input.mousePosition;
        }

        castPosition = camera.ScreenToWorldPoint(castPosition);

        //Check if clicked directly on object. Needed for situation where two objects are close and one of them was clicked directly.
        //In such situation we want to choose object that was directly clicked
        RaycastHit2D hit = DirectHit(castPosition);
        if (hit.collider == null)
        {
            //If no object was clicked directly check if there is some controllable object in close distance and choose it
            hit = CloseHit(castPosition);
        }

        Collider2D hitCollider = hit.collider;

        if (hitCollider != null)
        {
            //If some object was hit, set control for that object
            SetControl(hitCollider);
        }
    }

    //Using static Vectors from VectorUtility for casting to avoid using "new" implicitly every frame
    private RaycastHit2D DirectHit(Vector2 castPosition)
    {
        return Physics2D.Raycast(castPosition, VectorUtility.zero, 0.0f, controllableLayerMask);
    }

    private RaycastHit2D CloseHit(Vector2 castPosition)
    {
        return Physics2D.CircleCast(castPosition, clickRadius, VectorUtility.zero, 0.0f, controllableLayerMask);
    }

    private void SetControl(Collider2D hitCollider)
    {
        controlling = true;

        controlled = hitCollider.GetComponent<IControllable>();

        //If clicked object is not controllable, do nothing
        if (controlled == null)
        {
            return;
        }

        if (controlled is PlayerController)
        {
            //If clicked object is player, previous player looses focus
            currentPlayer.LooseFocus();
            //Clicked player becomes currentPlayer
            currentPlayer = controlled as PlayerController;
            //New focus object becomes controlled
        }

        //Set control to clicked object
        controlled.SetControlled(true);
    }

    private void Update()
    {
        //Choose right click condition depending on input type
        if (touchControls)
        {
            clicked = Input.touchCount > 0;
        }
        else
        {
            clicked = Input.GetMouseButton(0);
        }

        //Using bool clicked instead of event onClick because we want player to be able to grab object when he clicks somewhere else
        //and then moves to the object.
        if (!controlling && clicked)
        {
            //Every frame if not controlling any object and player is clicking, try to find objects to control
            FindControllable();
        }
        else if (controlling && !clicked)
        {
            //When player is not clicking but controlling some object, stop controlling this object
            //Controlling only one object at a time because we want it to work similarly on mouse and touchscreen
            controlling = false;
            controlled.SetControlled(false);
        }
    }

    private void Awake()
    {
        //Cache and initialize
        camera = Camera.main;
        touchControls = Input.touchSupported;
        //Set focus on one of the players on game start
        currentPlayer = FindObjectOfType<PlayerController>();
        currentPlayer.SetFocus();

        //Use layers in casting for better optimization
        controllableLayerMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Throwable");
    }
}