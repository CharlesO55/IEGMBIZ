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


            Ray ray = Camera.main.ScreenPointToRay(e.startPos);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 500);
            foreach (RaycastHit2D h in hits)
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