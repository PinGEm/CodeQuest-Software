using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CYoureSharpPackage
{
    public class Interpreter
    {
        public Interpreter(Expr expr)
        {
            try
            {
                Execute(expr);
            }
            catch(System.Exception ex)
            {
                Debug.LogError($"Runtime Error: {ex.Message}");
            }
        }

        private void Execute(Expr expr)
        {
            switch (expr)
            {
                case Expr.SelectExpr select:
                    RunSelect(select.var1, select.var2);
                    break;
                case Expr.MoveExpr move:
                    RunMove(move.direction);
                    break;
                case Expr.SwapExpr swap:
                    RunSwap(swap.var1, swap.var2);
                    break;
                case Expr.FuncCallExpr func:
                    RunFunc(func.name);
                    break;
                default:
                    Debug.LogError("Unknown Expression Type");
                    break;
            }
        }

        private void RunMove(string direction)
        {
            // Place here necessary implementation in order to store the data inside the CYoureSharp.cs
            Debug.Log($"[INTERPRETER]: Moving {direction}");
        }

        private void RunSwap(string v1, string v2)
        {
            // Place here necessary implementation in order to store the data inside the CYoureSharp.cs
            Debug.Log($"[INTERPRETER]: Swapping {v1} with {v2}");
        }

        private void RunSelect(string v1, string v2)
        {
            // Place here necessary implementation in order to store the data inside the CYoureSharp.cs
            Debug.Log($"[INTERPRETER]: Selecting {v1} and {v2}");
        }

        private void RunFunc(string name)
        {
            // Place here necessary implementation in order to store the data inside the CYoureSharp.cs
            Debug.Log($"[INTERPRETER]: Running {name}");
        }
    }
}
