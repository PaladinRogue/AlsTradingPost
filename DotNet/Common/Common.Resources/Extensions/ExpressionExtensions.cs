using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Resources.Extensions
{
    public static class ExpressionExtensions
    {
        public static string LambdaToString<T>(this Expression<Func<T, bool>> expression)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            WalkExpression(replacements, expression);


            string body = expression.Body.ToString();

            foreach (ParameterExpression parameter in expression.Parameters)
            {
                string parameterName = parameter.Name;
                string parameterTypeName = parameter.Type.Name;
                body = body.Replace(parameterName + ".", parameterTypeName + ".");
            }

            foreach ((string key, string value) in replacements)
            {
                body = body.Replace(key, value);
            }

            return body;
        }

        private static void WalkExpression(
            IDictionary<string, string> replacements,
            Expression expression)
        {
            while (true)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        string replacementExpression = expression.ToString();
                        if (replacementExpression.Contains("value("))
                        {
                            string replacementValue = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
                            if (!replacements.ContainsKey(replacementExpression))
                            {
                                replacements.Add(replacementExpression, replacementValue.ToString());
                            }
                        }

                        break;

                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.OrElse:
                    case ExpressionType.AndAlso:
                    case ExpressionType.Equal:
                        if (expression is BinaryExpression binaryExpression)
                        {
                            WalkExpression(replacements, binaryExpression.Left);
                            expression = binaryExpression.Right;
                        }

                        continue;

                    case ExpressionType.Call:
                        if (expression is MethodCallExpression methodCallExpression)
                        {
                            foreach (Expression argument in methodCallExpression.Arguments)
                            {
                                WalkExpression(replacements, argument);
                            }
                        }

                        break;

                    case ExpressionType.Lambda:
                        LambdaExpression lambdaExpression = expression as LambdaExpression;
                        if (lambdaExpression != null)
                        {
                            expression = lambdaExpression.Body;
                        }
                        continue;

                    case ExpressionType.Constant:
                        break;
                }

                break;
            }
        }
    }
}