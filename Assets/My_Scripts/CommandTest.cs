using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace My_Scripts
{
    public enum Direct
    {
        Up,
        Down,
        Left,
        Right
    }
    public class CommandTest : MonoBehaviour
    {
        public User user;
        
        public void ClickUp()
        {
            user.Move(Direct.Up,1);
        }
        public void ClickDown()
        {
            user.Move(Direct.Down,1);
        }
        public void ClickLeft()
        {
            user.Move(Direct.Left,1);
        }
        public void ClickRight()
        {
            user.Move(Direct.Right,1);
        }

        public void Undo()
        {
            user.Undo(1);
        }

        public void Redo()
        {
            user.Redo(1);
        }
    }

    public abstract class MyCommand
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    public class MoveCommand : MyCommand
    {
        private int _operand;
        private Direct _operator;
        private Leg _leg;
        private Transform _player;

        public MoveCommand(Transform player,Leg leg, Direct @operator, int operand)
        {
            this._player = player;
            this._leg = leg;
            this._operand = operand;
            this._operator = @operator;
        }
        
        /// <summary>
        /// 执行行走命令
        /// </summary>
        public override void Execute()
        {
            _leg.Move(_player,_operator,_operand);
        }

        public override void UnExecute()
        {
            _leg.Move(_player,Undo(_operator),_operand);
        }

        private Direct Undo(Direct @operator)
        {
            switch (@operator)
            {
                case Direct.Left: return Direct.Right;
                case Direct.Right: return Direct.Left;
                case Direct.Up: return Direct.Down;
                case Direct.Down: return Direct.Up;
                default:
                    throw new ArgumentException("@operator");
            }
        }
    }

    public class Leg
    {
        //public int stepCount = 0;

        public void Move(Transform player,Direct @operator, int operand)
        {
            switch (@operator)
            {
                case Direct.Left: player.Translate(Vector3.left*operand);
                    break;
                case Direct.Right: player.Translate(Vector3.right*operand);
                    break;
                case Direct.Up: player.Translate(Vector3.up*operand);
                    break;
                case Direct.Down:player.Translate(Vector3.down*operand);
                    break;
            }
            Debug.Log("玩家向"+@operator+"移动了"+operand);
        }
    }
    
}