using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public ActionBase[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].Act(controller);
    }

    private void CheckTransition(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceded = transitions[i].decision.All(d => d.Decide(controller));

            if (decisionSucceded)
            {
                controller.TransitionToState(transitions[i].trueState);
            } else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
