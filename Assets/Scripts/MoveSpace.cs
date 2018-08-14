using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpace : MonoBehaviour
{

	public Transform objectToMove;

	public SteamVR_TrackedObject steamObj;

    IEnumerator coroutine;

    // Update is called once per frame
    void Update()
    {
        if (coroutine == null)
        {
            coroutine = Scale(steamObj);
            StartCoroutine(coroutine);
        }
    }


    IEnumerator Scale(SteamVR_TrackedObject obj)
    {
        SteamVR_Controller.Device dev;
        try
        {
            dev = SteamVR_Controller.Input((int)obj.index);
        }
        catch (System.IndexOutOfRangeException)
        {
            coroutine = null;
            yield break;
        }
        catch (System.Exception)
        {
            coroutine = null;
            throw;
        }

        if (!dev.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            coroutine = null;
            yield break;
        }
        
        Vector3 currPos = obj.transform.position;
        yield return null;

        while (!dev.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Vector3 diff = obj.transform.position - currPos;
            objectToMove.position += diff;
            currPos = obj.transform.position;
            yield return null;            
        }

        coroutine = null;
    }

}
