using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionAsset scheme;
    InputAction touchPosition;

    
    TouchArgs touchArgs;

    float timePressed = 0;
    enum STATE
    {
        NONE,
        TAP,
        SWIPE,
        HOLD
    }
    STATE state;

    [System.Serializable]
    struct Threshold{
        public LayerMask layer;

        [Tooltip("Only used by hold")]
        [Range(0,1)] public float minPressTime;
        
        [Tooltip("Only used by swipe")]
        [Range(100, 1000)]public float minMoveDistance;
    }

    [SerializeField] Threshold tapThreshold, swipeThreshold, holdThreshold;



    


    void OnState(InputAction.CallbackContext c)
    {
        // MARK AS NEW TOUCH START
        if (c.phase == InputActionPhase.Started)
        {
            state = STATE.TAP;
            touchArgs.startPos = touchPosition.ReadValue<Vector2>();
        }

        // ON TOUCH END
        else if (c.phase == InputActionPhase.Canceled)
        {
            // BROADCAST IF IT'S A SWIPE OR TAP
            // (HOLD WAS ALREADY BROADCASTED WHILE HELD)
            if(state != STATE.HOLD) { 
                if (Vector2.Distance(touchArgs.startPos, touchArgs.currPos) > swipeThreshold.minMoveDistance)
                    state = STATE.SWIPE;
                else
                    state = STATE.TAP;
            }

            BroadcastToControllable();


            state = STATE.NONE;
            timePressed = 0;
            touchArgs = new();
        }
    }

    private void Awake()
    {
        scheme.Enable();
        touchPosition = scheme.FindAction("Position");

        scheme.FindAction("State").started += OnState;
        scheme.FindAction("State").canceled += OnState;
    }


    void Update()
    {
        if (state == STATE.NONE)
            return;

        touchArgs.currPos = touchPosition.ReadValue<Vector2>();
        timePressed += Time.deltaTime;

        switch (state)
        {
            case STATE.TAP:
            case STATE.SWIPE:
                if (timePressed > holdThreshold.minPressTime)
                    state = STATE.HOLD;
                break;
            case STATE.HOLD:
                BroadcastToControllable();
                break;
        }
    }


    void BroadcastToControllable()
    {
        this.touchArgs.direction = touchArgs.startPos - touchArgs.currPos;

        switch (state)
        {
            case STATE.TAP:
                {
                    touchArgs.hit = IControllable.Raycast<ITappable>(touchArgs, out ITappable component);
                    component?.onUserInput(touchArgs);
                }
                break;
            case STATE.SWIPE:
                {
                    touchArgs.hit = IControllable.Raycast<ISwipeable>(touchArgs, out ISwipeable component);
                    component?.onUserInput(touchArgs);
                }
                break;
            case STATE.HOLD:
                {
                    touchArgs.hit = IControllable.Raycast<IHoldable>(touchArgs, out IHoldable component);
                    component?.onUserInput(touchArgs);
                }
                break;
        }
        Debug.Log($"Action was : {state}");
    }
}
