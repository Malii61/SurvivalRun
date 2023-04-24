using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class MathematicalOperationCalculator
{
    private static System.Data.DataTable table = new System.Data.DataTable();
    private static Dictionary<string, float> otherOperations = new Dictionary<string, float>()
    {
        {"sin90",1},
        {"sin0",0},
        {"sin30",.5f},
        {"cos90",0},
        {"cos0",1},
        {"cos30",.5f},
    };
    public static int FindResult(string expression)
    {
        expression = AdjustExpression(expression);
        int i = (int)(table.Compute(expression, ""));
        Debug.Log(i);
        //if (int.TryParse((string)table.Compute(expression, ""), out int result))
        //{
        //    return result;
        //}

        return PointManager.Instance.GetPoint(PersonType.soldier);
    }

    private static string AdjustExpression(string expression)
    {
        if (expression.Contains("x"))
        {
            string textToBeReplaced = PointManager.Instance.GetPoint(PersonType.soldier).ToString(); 
            //if(expression.ElementAt(0) != 'x')
            //{
            //    textToBeReplaced = "*" + textToBeReplaced;
            //}
            expression = expression.Replace("x", textToBeReplaced);
        }
        for(int i = 0; i < otherOperations.Count; i++)
        {
            KeyValuePair<string,float> operation = otherOperations.ElementAt(i);
            if (expression.Contains(operation.Key))
            {
                expression = expression.Replace(operation.Key, operation.Value.ToString());
            }
        }
        return expression;
    }
}
