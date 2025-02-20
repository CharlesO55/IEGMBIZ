using UnityEngine;


namespace components.controllables
{
    public interface IHoldable : IControllable { }
    public interface ITappable : IControllable { }
    public interface ISwipeable : IControllable { }

    public interface IControllable
    {
        abstract void onUserInput(TouchArgs e);

        public static GameObject Raycast<T>(TouchArgs e, out T component) where T : IControllable
        {
            if (e.hit != null)
            {
                component = e.hit.GetComponent<T>();
                return e.hit;
            }


            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(e.startPos), 500);
            foreach (RaycastHit h in hits)
            {
                if (h.collider.TryGetComponent<T>(out component))
                {
                    return h.collider.gameObject;
                }
            }

            component = default;
            return null;
        }

    }
}