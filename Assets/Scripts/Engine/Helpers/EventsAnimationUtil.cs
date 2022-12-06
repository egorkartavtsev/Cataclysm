using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Abstractions;

namespace Helpers
{
    public static class EventsAnimationUtil
    {
        public static IEnumerator<Type> AnimateDamage(IEventMessagesContainer owner, GameObject dmgLabel)
        {
            Vector3 pos = dmgLabel.transform.position;
            float A = 1f;

            while (pos.y < 3)
            {
                pos.y += Time.deltaTime;
                A = Mathf.Clamp01(dmgLabel.GetComponent<TextMesh>().color.a - (0.7f * Time.deltaTime));

                dmgLabel.GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, A);
                dmgLabel.transform.position = pos;
                yield return null;
            }

            owner.RemoveDmgLabel(dmgLabel);
        }
        public static IEnumerator<Type> AnimateInfo(IEventMessagesContainer owner, GameObject dmgLabel)
        {
            Vector3 pos = dmgLabel.transform.position;
            float A = 1f;

            while (pos.y < 3)
            {
                pos.y += Time.deltaTime;
                A = Mathf.Clamp01(dmgLabel.GetComponent<TextMesh>().color.a - (0.7f * Time.deltaTime));

                dmgLabel.GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, A);
                dmgLabel.transform.position = pos;
                yield return null;
            }

            owner.RemoveDmgLabel(dmgLabel);
        }

        public static IEnumerator<WaitForSeconds> Wait(float sec, IAction action)
        {
            yield return new WaitForSeconds(sec);

            action.SetAvailable();
        }
    }
}
