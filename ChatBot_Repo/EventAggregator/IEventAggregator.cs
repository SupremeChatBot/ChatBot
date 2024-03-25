using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.EventAggregator
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> action);
        void Publish<T>(T message);
    }
}
