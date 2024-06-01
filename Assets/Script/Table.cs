using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class Table : MyObject
{
    [SerializeField] private List<Transform> valisePos;
    
    
    protected override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    public override void Interact()
    {
        base.Interact();

        if(PlayerController.Instance.valiseOwning.Count == 0) return;
        int index = 0;
        foreach (var valise in PlayerController.Instance.valiseOwning)
        {
            valise.isOwned = false;
            valise._collider.enabled = true;
            valise.transform.DOMove(valisePos[index].position, .2f);
            valise.transform.DORotate(new Vector3(0, 0, 0), .2f);
            index++;
        }
    }
}
