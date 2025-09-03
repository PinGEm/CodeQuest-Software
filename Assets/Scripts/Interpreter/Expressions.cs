using System.Collections;
using System.Collections.Generic;

namespace CYoureSharpPackage
{
    public abstract class Expr
    {
        public class MoveExpr : Expr
        {
            public readonly string direction;

            public MoveExpr(string direction)
            {
                this.direction = direction;
            }

            public override string ToString()
            {
                return $"MoveExpr({direction})";
            }
        }

        public class SwapExpr : Expr
        {
            public readonly string var1;
            public readonly string var2;

            public SwapExpr(string var1, string var2)
            {
                this.var1 = var1;
                this.var2 = var2;
            }

            public override string ToString()
            {
                return $"SwapExpr({var1},{var2})";
            }
        }

        public class SelectExpr : Expr
        {
            public readonly string var1;
            public readonly string var2;

            public SelectExpr(string var1, string var2)
            {
                this.var1 = var1;
                this.var2 = var2;
            }

            public override string ToString()
            {
                return $"SelectExpr({var1},{var2})";
            }
        }

        public class FuncCallExpr : Expr
        {
            public readonly string name;

            public FuncCallExpr(string name)
            {
                this.name = name;
            }

            public override string ToString()
            {
                return $"FuncCallExpr({name})";
            }
        }
    }
}