using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<object>> subscribers = new Dictionary<Type, List<object>>();

        public void Subscribe<T>(Action<T> action)
        {
            if (!subscribers.ContainsKey(typeof(T)))
                subscribers[typeof(T)] = new List<object>();

            subscribers[typeof(T)].Add(action);
        }

        public void Publish<T>(T message)
        {
            if (subscribers.ContainsKey(typeof(T)))
            {
                var actions = subscribers[typeof(T)].Cast<Action<T>>();
                foreach (var action in actions)
                {
                    action.Invoke(message);
                }
            }
        }
    }
}