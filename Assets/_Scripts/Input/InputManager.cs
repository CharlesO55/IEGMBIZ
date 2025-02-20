using UnityEngine;
using UnityEngine.InputSystem;
using components.controllables;

namespace systems
{
    public class InputManager : MonoBehaviour
    {
        #region INPUT ACTIONS
        [SerializeField] InputActionAsset scheme;
        InputAction touchPosition;
        #endregion

        TouchArgs touchArgs;

        enum STATE
        {
            NONE,
            TAP,
            SWIPE,
            HOLD
        }
        STATE state;

        [Tooltip("Exceeding will trigger Hold")]
        [SerializeField] float holdTimeThreshold = 0.5f;
        [Tooltip("Exceeding will trigger swipe")]
        [SerializeField] float swipeDistanceThreshold = 300;



        #region LIFECYCLE
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
            touchArgs.timePressed += Time.deltaTime;

            switch (state)
            {
                case STATE.TAP:
                case STATE.SWIPE:
                    if (touchArgs.timePressed > holdTimeThreshold)
                        state = STATE.HOLD;
                    break;
                case STATE.HOLD:
                    BroadcastToControllable();
                    break;
            }
        }
        #endregion

        #region METHODS
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
                if (state != STATE.HOLD)
                {
                    if (Vector2.Distance(touchArgs.startPos, touchArgs.currPos) > swipeDistanceThreshold)
                        state = STATE.SWIPE;
                    else
                        state = STATE.TAP;
                }

                BroadcastToControllable();


                state = STATE.NONE;
                touchArgs = new();
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
        #endregion
    }
}