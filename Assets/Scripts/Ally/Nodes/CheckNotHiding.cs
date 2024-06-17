using BehaviourTree;
using UnityEngine;

public class CheckShouldHide : Decorator
{
    public CheckShouldHide(Node _child) : base(_child)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        // Debug.LogWarning("was entered" + wasEntered);
    }

    public override void OnExit()
    {
        base.OnExit();
        // Debug.LogWarning("was exited" + !wasEntered);
    }
    public override NodeState Evaluate()
    {
        // Controleer hier of de ally nog steeds moet schuilen.
        // Dit kan gebaseerd zijn op een variabele in de blackboard of andere logica.
        // Voor dit voorbeeld controleer ik een fictieve variabele "shouldHide".
        string str = blackboard.IsChasingPlayerStr;
        bool shouldHide = blackboard.GetVariable<bool>(str);
        // Debug.LogWarning("should hide: " + GlobalBlackboard.Instance.GetVariable<bool>(str));

        if (!shouldHide)
        {
            blackboard.ClearData(AllyBT.Settings.TreeStr);

            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            if (!blackboard.GetVariable<bool>("hasThrown"))
            {
                state = child.Evaluate();
            }
            return state;
        }
    }

    public override void SetupBlackboard(GlobalBlackboard blackboard)
    {
        base.SetupBlackboard(blackboard);
        child.SetupBlackboard(blackboard);
    }
}
