using UnityEngine;
using UnityEngine.InputSystem;
using components.controllables;

namespace systems
{
    public class InputManager : MonoBehaviour
    {
        TouchArgs touchArgs;

        enum STATE
        {
            NONE,
            TAP,
            SWIPE,
            HOLD
        }
        STATE state;


        
        [SerializeField] InputActionAsset inputMap;
        [SerializeField] InputActionReference tapAction;  // Quick tap
        [SerializeField] InputActionReference moveAction;
        [SerializeField] InputActionReference holdAction;

        #region LIFECYCLE
        private void Awake()
        {
            inputMap.Enable();

            tapAction.action.performed += TapHandle;    // First tap -> swipe -> hold

            moveAction.action.performed += MoveHandle;  // Shift to 

            holdAction.action.performed += HoldHandle;  // Shift to Hold when triggered

            holdAction.action.canceled += Release;
        }

        private void OnDestroy()
        {
            inputMap.Enable();

            tapAction.action.performed -= TapHandle;    // First tap -> swipe -> hold

            moveAction.action.performed -= MoveHandle;  // Shift to 

            holdAction.action.performed -= HoldHandle;  // Shift to Hold when triggered

            holdAction.action.canceled -= Release;
        }

        #endregion

        void Release(InputAction.CallbackContext c)
        {
            Debug.Log("release");

            state = STATE.NONE;
            touchArgs.isTouchEnd = true;

            touchArgs.hit = IControllable.Raycast<IHoldable>(touchArgs, out IHoldable component);
            component?.onUserInput(touchArgs);


            touchArgs = default;
        }

        void TapHandle(InputAction.CallbackContext c)
        {
            touchArgs = new TouchArgs(c.ReadValue<Vector2>());

            touchArgs.hit = IControllable.Raycast<ITappable>(touchArgs, out ITappable component);

            component?.onUserInput(touchArgs);
        }
        void HoldHandle(InputAction.CallbackContext c)
        {
            Debug.Log("Hold");

            // Overwrite the startpos and switch state
            touchArgs.startPos = c.ReadValue<Vector2>();
            touchArgs.hit = IControllable.Raycast<IHoldable>(touchArgs, out IHoldable component);

            if (component != null)
            {
                state = STATE.HOLD;
                component?.onUserInput(touchArgs);
            }
        }

        void MoveHandle(InputAction.CallbackContext c)
        {
            touchArgs.direction = c.ReadValue<Vector2>();

            if (state == STATE.HOLD)
            {
                touchArgs.currPos += touchArgs.direction;

                touchArgs.hit = IControllable.Raycast<IHoldable>(touchArgs, out IHoldable component);
                component?.onUserInput(touchArgs);
            }

            else
            {
                touchArgs.hit = IControllable.Raycast<ISwipeable>(touchArgs, out ISwipeable component);
                component?.onUserInput(touchArgs);
            }
        }
    }
}