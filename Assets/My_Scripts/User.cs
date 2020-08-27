using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace My_Scripts
{
    public class User:MonoBehaviour
    {
        private Leg _leg=new Leg();
        private List<MyCommand> _commands=new List<MyCommand>();
        private int _currnet = 0;

        public void Redo(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                if (_currnet < _commands.Count - 1)
                {
                    MyCommand command = _commands[++_currnet];
                    command.Execute();
                }
                else
                {
                    Debug.Log("已经到了初始位置");
                }
            }
        }

        public void Undo(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                if (_currnet > 0)
                {
                    MyCommand command = _commands[--_currnet];
                    command.UnExecute();
                }
                else
                {
                    Debug.Log("已经到了最新的位置");
                }
            }
        }

        public void Move(Direct @operator, int operand)
        {
            MyCommand command = new MoveCommand(transform, _leg, @operator, operand);
            command.Execute();
            _commands.Add(command);
            _currnet++;
        }
    }
}