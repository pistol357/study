using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleModule : MonoBehaviour
{
    public List<Phase> Phases = new List<Phase>();

    public bool IsFinished { get; private set; }

    /*
    private void Awake() => Init();

    private void Init()
    {
        foreach(Phase phase in Phases)
        {
            phase.Init();
        }
    }
    */

    public void AddPhase(Phase phase)
    {
        Phases.Add(phase);
    }

    public void ResetPhases()
    {
        IsFinished = false;

        foreach(Phase phase in Phases)
        {
            phase.Reset();
        }
    }

    public IEnumerator PhasesRoutine()
    {
        for(int i = 0; i < Phases.Count; i++)
        {
            Phase currentPhase = Phases[i];

            while (!currentPhase.IsFinished)
            {
                currentPhase.Update();
                yield return null;
            }

            yield return currentPhase.WaitDelay;
        }

        IsFinished = true;
    }
}
