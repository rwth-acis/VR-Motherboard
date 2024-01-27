using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using i5.VirtualAgents.AgentTasks;

namespace i5.VirtualAgents.Examples
{
    public class ControllerScript : SampleScheduleController
    {
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private bool useTaskShortcuts;
        [SerializeField] private bool walk = true;
        [SerializeField] private bool waveRightArm = true;
        //part for itempickup
        public bool startItemPickup;
        [SerializeField] private GameObject cpu;

        [SerializeField] private Transform dropItemHere;

        protected override void Start()
        {
        
            base.Start();
            if (walk)
            {
                for (int i = 0; i < waypoints.Count; i++)
                {
                    taskSystem.Tasks.GoTo(waypoints[i].position);
                }
            }
            if (waveRightArm)
            {
                AgentAnimationTask wave1 = new AgentAnimationTask("WaveRight", 5, "", "Right Arm");
                taskSystem.ScheduleTask(wave1, 0, "Right Arm");
            }

        }
        private void Update()
        {
            if (startItemPickup)
            {
                taskSystem.Tasks.GoTo(cpu.transform);
                taskSystem.Tasks.GoToAndPickUp(cpu, default);

                taskSystem.Tasks.GoToAndDropItem(dropItemHere);
                startItemPickup = false;
                StartCoroutine(rescaleCPU());
                taskSystem.Tasks.GoTo(waypoints[1].transform);
  

            }
        }

        private IEnumerator rescaleCPU()
        {
            yield return new WaitForSeconds(4);
            Vector3 oldsize = cpu.transform.localScale;
            cpu.transform.localScale = oldsize * 14;
        }
    }
}
