using UnityEngine;

namespace BehaviourTree
{
    public class Decorator : Node
    {
        protected Node child;

        public Decorator(Node _child)
        {
            child = _child;
            child.Parent = this;
        }

        public override void SetupBlackboard(GlobalBlackboard blackboard)
        {
            base.SetupBlackboard(blackboard);
            child.SetupBlackboard(blackboard);
        }
    }
}