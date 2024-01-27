using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace i5.VirtualAgents.Examples
{
    public class ItemDropControllerScript : SampleScheduleController
    {
        public bool startItemPickup;
        [SerializeField] private GameObject cpu;

        [SerializeField] private Transform dropItemHere;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (startItemPickup)
            {
                taskSystem.Tasks.GoToAndPickUp(cpu, default);
                taskSystem.Tasks.GoToAndDropItem(dropItemHere);
                startItemPickup = false;
            }
        }
    }
}