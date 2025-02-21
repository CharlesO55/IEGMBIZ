using components.controllables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Removable : Tappable
{
    public override void onUserInput(TouchArgs e)
    {
        Debug.Log($"Tapped {name}");
        this.Remove();
    }

    private void Remove()
    {
        Vector3 pos = this.transform.position;
        pos.z--;
        PlayerProgress.I.SpawnCurrency(pos);
        Destroy(gameObject);
    }
}
