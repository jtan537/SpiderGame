using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cameron Hadfield
// Created: 6/3/21
// Last modified: 6/3/21
// Attach this to the player. it is intended to be something taken in by the player inventory controller
public class GrappleWeb : MonoBehaviour, IHand
{
    [SerializeField]
    private PlayerCrosshair Crosshair;

    [SerializeField]
    private float _range;

    [SerializeField]
    private GameObject indicatorPrefab; // this is temporary (hopefully) as we should replace it with a primitive or sprite
    private GameObject indicator; // This is an instance of the prefab that we want to use if it exists.

    public float Range { 
        get { return _range; } 
        set { _range = value > 0? value: 0; }
    }

    private bool aimed = false;
    private Vector3 target;

    public void Use()
    {
        if (!aimed)
        {
            AimGrappleWeb();
        }
        else
        {
            ShootGrappleWeb();
        }
    }
    private void AimGrappleWeb()
    {
        Vector3 hookEnd;
        float distance = DistanceCheck(out hookEnd);

        if (distance != -1)
        {
            IndicatorAt(hookEnd);
            target = hookEnd;
            aimed = true;
        }
        
    }
    private void ShootGrappleWeb()
    {
        CreateRope(transform.position, target);
        aimed = false;
    }
    #region helpers
    // returns only the distance
    private float DistanceCheck()
    {
        Vector3 collisionPoint;
        return DistanceCheck(out collisionPoint);
    }
    // returns the distance and allows you to get the point of collision
    // Will return -1 if nothing was hit
    private float DistanceCheck(out Vector3 collisionPoint)
    {
        Ray ray = new Ray(transform.position, Crosshair.GetCrosshairForward());
        RaycastHit hitinfo;

        // Raycast to find the nearest collider
        bool hit = Physics.Raycast(ray, out hitinfo, _range);

        if (hit){
            collisionPoint = hitinfo.point;

            // We only return distance now, but maybe want more in the future? unsure
            return hitinfo.distance;
        }
        else
        {
            collisionPoint = Vector3.zero;
            return -1;
        }
    }
    #endregion
    private void CreateRope(Vector3 start, Vector3 end)
    {
        GameObject rope = new GameObject("GrappleRope");
        Rope shotRope = rope.AddComponent<Rope>();
        shotRope.begin = start;
        shotRope.end = end;
        shotRope.Render();
    }
    private void IndicatorAt(Vector3 point)
    {
        RequestIndicator().transform.position = point; 
    }
    private GameObject RequestIndicator()
    {
        if (indicator == null)
        {
            indicator = Instantiate(indicatorPrefab);
        }

        return indicator;
    }
    private void DestroyMe()
    {
        // Have to hardcode this for now because of invokes... not permanent anyway ƪ(˘⌣˘)ʃ
        Destroy(indicator);
    }
}
