using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;


public class Fire : MonoBehaviour
{
    static int totalFires = 0;

    List<IBurnable> victims;

    [SerializeField] int spreadTime = 5;
    [SerializeField] int burnDamage = 5;


    void Start()
    {
        totalFires++;
        InvokeRepeating("Spread", spreadTime, spreadTime);
        InvokeRepeating("Burn", 0, 1);
    }

    void Spread()
    {
        if (totalFires > 5)
            return;

        Vector3 offset = new Vector3(Random.Range(-2, 3), Random.Range(-1, 1), 0);
        Instantiate(gameObject, this.transform.position + offset, Quaternion.identity, transform.parent);
    }


    void Burn()
    {
        /*foreach (IBurnable b in victims)
        {
            if(b != null)
                b.Burn(burnDamage);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IBurnable>(out IBurnable c))
        {
            victims.Add(c);
        }
    }



    private void OnDestroy()
    {
        totalFires--;
    }
}
