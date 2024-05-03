using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPokeToAttach : MonoBehaviour
{
    [SerializeField] Transform pokeAttachPoint;

    private XRPokeInteractor _xrPokeInteractor;

    void Start()
    {
        _xrPokeInteractor = transform.parent.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokeAttachPoint();
    }

    void SetPokeAttachPoint()
    {
        if( pokeAttachPoint == null )
        { return; }
        if( _xrPokeInteractor == null )
        { return; }

        _xrPokeInteractor.attachTransform = pokeAttachPoint;
    }
}
