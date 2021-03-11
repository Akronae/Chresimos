using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Chresimos.Core.Helpers
{
    public class ExpressionHelper
    {
        public static object GetMemberExpressionValue (MemberExpression expression)
        {
            var dependencyChain = GetMemberExpressionDependencyChain(expression);

            if (!(dependencyChain.Last().Expression is ConstantExpression baseExpression))
            {
                throw new Exception(
                    $"Last expression {dependencyChain.Last().Expression} of dependency chain of {expression} is not a constant." +
                    "Thus the expression value cannot be found.");
            }

            var resolvedValue = baseExpression.Value;

            for (var i = dependencyChain.Count; i > 0; i--)
            {
                var expr = dependencyChain[i - 1];
                resolvedValue = new PropOrField(expr.Member).GetValue(resolvedValue);
            }

            return resolvedValue;
        }

        public static List<MemberExpression> GetMemberExpressionDependencyChain (MemberExpression expression)
        {
            // Dependency chain of a MemberExpression is of the form:
            // MemberExpression expression
            //    MemberExpression expression.Expression
            //        ... MemberExpression expression.[...].Expression
            //            ConstantExpression expression.[...].Expression.Expression <- base object
            var dependencyChain = new List<MemberExpression>();
            var pointingExpression = expression;
            while (pointingExpression != null)
            {
                dependencyChain.Add(pointingExpression);
                pointingExpression = pointingExpression.Expression as MemberExpression;
            }

            return dependencyChain;
        }
    }
}