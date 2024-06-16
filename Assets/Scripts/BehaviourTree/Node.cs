using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviourTree;


namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCES,
        FAILURE
    }
    public class Node
    {
        protected NodeState state;
        public bool WasEntered = false;
        public Node Parent;
        protected GlobalBlackboard blackboard;
        protected List<Node> Children = new();

        public Node()
        {
            Parent = null;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public virtual void SetupBlackboard(GlobalBlackboard blackboard)
        {
            this.blackboard = blackboard;
        }
    }
}
