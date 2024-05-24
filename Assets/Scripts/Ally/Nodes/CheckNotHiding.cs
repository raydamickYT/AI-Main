using BehaviourTree;
using UnityEngine;

public class CheckShouldHide : Node
{
    public CheckShouldHide()
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        // Debug.LogWarning("was entered" + wasEntered);
    }

    public override void OnExit(){
        base.OnExit();
        // Debug.LogWarning("was exited" + !wasEntered);
    }
    public override NodeState Evaluate()
    {
        // Controleer hier of de ally nog steeds moet schuilen.
        // Dit kan gebaseerd zijn op een variabele in de blackboard of andere logica.
        // Voor dit voorbeeld controleer ik een fictieve variabele "shouldHide".
        string str = GlobalBlackboard.Instance.IsChasingPlayerStr;
        bool shouldHide = GlobalBlackboard.Instance.GetVariable<bool>(str);
        Debug.LogWarning("should hide: " + shouldHide);

        if (shouldHide)
        {
            state = NodeState.SUCCES;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
