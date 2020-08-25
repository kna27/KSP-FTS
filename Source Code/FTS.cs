using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplosionAbort
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ExplosionAbort : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                    List<Part> parts = FlightGlobals.ActiveVessel.parts;
                    QuickSaveLoad.QuickSave();
                    if (FlightGlobals.ActiveVessel.altitude <= 1000 || parts.Count >= 150)
                    {
                        StartCoroutine(ExplodeParts(0.001f));
                    }
                    else
                    {
                        StartCoroutine(ExplodeParts(0.005f));
                    }

            }
        }

        IEnumerator ExplodeParts(float time)
        {
            List<Part> parts = FlightGlobals.ActiveVessel.parts;
            for (int i = parts.Count - 1; i >= 0; i--)
            {
                parts[i].explode();
                yield return new WaitForSeconds(time);
            }
        }
    }
}
