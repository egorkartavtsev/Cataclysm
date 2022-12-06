using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Abstractions;
using Helpers;

namespace Actions
{
    public class GatherResource : IAction
    {
        public bool IsDone { get; set; }
        public bool Available { get; set; }
        public GameObject TargetObject { get; set; }
        public ActionManager Interactor { get; set; }

        public void Do()
        {
        }

        public void Work()
        {
            ResourceObject resourceObject = TargetObject.GetComponent<ResourceObject>();
            int damage = 15;

            if (resourceObject.LocatedIn.Contains.Health > 0)
            {
                resourceObject.LocatedIn.Contains.Health -= damage;

                TargetObject.transform.parent
                    .gameObject
                    .GetComponent<IEventMessagesContainer>()
                    .ShowDamageAnimation(damage, TargetObject.transform);
            }
            else
            {
                resourceObject.Remove();
                IsDone = true;
            }

            TargetObject.GetComponent<ILootProducer>().ProduceLoot(damage);
        }

        public void SetAvailable()
        {
            Work();
            Available = true;
        }

        public void SetUnavailable()
        {
            Available = false;
        }
    }
}
