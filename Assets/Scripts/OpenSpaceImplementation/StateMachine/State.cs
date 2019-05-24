using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation {
    public class AIState {
        public StateMachine stateMachine;
        public Func<Task> action;

        private AIState(){}

        public static AIState Create(Func<Task> action)
        {
            AIState state = new AIState();
            state.action = action;
            return state;
        }

        public async Task Update()
        {
            await action?.Invoke();
        }

        public override String ToString()
        {
            return action.Method.Name;
        }
    }
}
