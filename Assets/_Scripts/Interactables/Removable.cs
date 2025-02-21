using components.controllables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(RattleEffect))]
public class Removable : Tappable
{
    [SerializeField] int tapsRequired = 5;
    [SerializeField] int leafReward = 3;

    RattleEffect rattle;
    private void Awake()
    {
        rattle = GetComponent<RattleEffect>();
    }
    public override void onUserInput(TouchArgs e)
    {
        tapsRequired--;

        rattle.Shake();

        if(tapsRequired == 0)
        {
            this.Remove();
        }
    }

    private void Remove()
    {
        Vector3 pos = this.transform.position;
        pos.z--;
        PlayerProgress.I.SpawnCurrency(pos, leafReward);
        Destroy(gameObject);
    }
}
