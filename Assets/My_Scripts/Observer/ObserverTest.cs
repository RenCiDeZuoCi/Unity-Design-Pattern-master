using System;
using System.Collections.Generic;
using UnityEngine;

namespace My_Scripts.Observer
{
    public class ObserverTest:MonoBehaviour
    {
        TimeData _timeData=new TimeData();

        private void Start()
        {
            _timeData.Attach(new PlayerObserver("玩家芈月"));
            _timeData.Attach(new PlayerObserver("玩家橘右君"));
            _timeData.Minute = DateTime.Now.Minute;
        }
    }
    /// <summary>
    /// 抽象观察者
    /// </summary>
    abstract class Observer
    {
        public virtual void Update(){}
    }

    class PlayerObserver : Observer
    {
        private string _name;

        public PlayerObserver(string name)
        {
            this._name = name;
        }

        public override void Update()
        {
            Debug.Log("玩家"+_name+"收到了通知，发起进攻");
        }
    }

    /// <summary>
    /// 抽象主题
    /// </summary>
    abstract class Subject
    {
        protected List<Observer> _observers=new List<Observer>();

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            
        }
    }
    
    /// <summary>
    /// 具体主题
    /// </summary>
    class TimeData : Subject
    {
        private int _minute;
        public int Minute
        {
            get => _minute;
            set
            {
                if (_minute != value)
                {
                    _minute = value;
                    Notify();
                }
            }
        }

        public new void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
            Debug.Log("主题发出通知:时间变化Minute:"+Minute);
        }
    }
}