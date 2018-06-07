using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    // Класс AVLTreeNode

    public class AvlNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        AvlTree<TNode> _tree;

        AvlNode<TNode> _left; // левый  потомок
        AvlNode<TNode> _right; // правый потомок


        #region Конструктор

        public AvlNode(TNode value, AvlNode<TNode> parent, AvlTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        #endregion

        #region Свойства 

        public AvlNode<TNode> Left
        {
            get { return _left; }

            internal set
            {
                _left = value;

                if (_left != null)
                {
                    _left.Parent = this; // установка указателя на родительский элемент
                }
            }
        }

        public AvlNode<TNode> Right
        {
            get { return _right; }

            internal set
            {
                _right = value;

                if (_right != null)
                {
                    _right.Parent = this; // установка указателя на родительский элемент
                }
            }
        }

        // Указатель на родительский узел

        public AvlNode<TNode> Parent { get; internal set; }

        // значение текущего узла 

        public TNode Value { get; private set; }

        // Сравнивает текущий узел по указаному значению, возвращет 1, если значение экземпляра больше переданного значения,  
        // возвращает -1, когда значение экземпляра меньше переданого значения, 0 - когда они равны.     

        #endregion

        #region CompareTo

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        #endregion

        #region Balance

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }

                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        private int Height(AvlNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(Height(node.Left), Height(node.Right));
            }

            return 0;
        }

        private int LeftHeight
        {
            get { return Height(Left); }
        }

        private int RightHeight
        {
            get { return Height(Right); }
        }

        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }


        private int BalanceFactor
        {
            get { return RightHeight - LeftHeight; }
        }

        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }

        #endregion

        #region LeftRotation

        private void LeftRotation()
        {
            // До
            //     12(this)     
            //      \     
            //       15     
            //        \     
            //         25     
            //     
            // После     
            //       15     
            //      / \     
            //     12  25  

            // Сделать правого потомка новым корнем дерева.
            AvlNode<TNode> newRoot = Right;
            ReplaceRoot(newRoot);

            // Поставить на место правого потомка - левого потомка нового корня.    
            Right = newRoot.Left;
            // Сделать текущий узел - левым потомком нового корня.    
            newRoot.Left = this;
        }

        #endregion

        #region RightRotation

        private void RightRotation()
        {
            // Было
            //     c (this)     
            //    /     
            //   b     
            //  /     
            // a       
            //     
            // Стало    
            //       b     
            //      / \     
            //     a   c  

            // Левый узел текущего элемента становится новым корнем
            AvlNode<TNode> newRoot = Left;
            ReplaceRoot(newRoot);

            // Перемещение правого потомка нового корня на место левого потомка старого корня
            Left = newRoot.Right;

            // Правым потомком нового корня, становится старый корень.     
            newRoot.Right = this;
        }

        #endregion

        #region LeftRightRotation

        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }

        #endregion

        #region RightLeftRotation

        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }

        #endregion

        #region Перемещение корня

        private void ReplaceRoot(AvlNode<TNode> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Head = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
            newRoot.Balance();
        }

        #endregion
    }
}