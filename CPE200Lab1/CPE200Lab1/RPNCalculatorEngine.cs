using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    public class RPNCalculatorEngine : CalculatorEngine
    {
        public new string Process(string str)
        {
            Stack<string> rpnStack = new Stack<string>();
            string result;
            string firstOperand, secondOperand;
            int nummm = 0;
            int opp = 0;
            if (str == null || str == "")
            {
                return "E";
            }
            List<string> parts = str.Split(' ').ToList<string>();

            foreach (string token in parts)
            {
                if (isNumber(token))
                {
                    if (token.Substring(0, 1) == "+") return "E";
                    rpnStack.Push(token);
                    nummm++;
                }
                else if (isOperator(token))
                {
                
                    if(rpnStack.Count < 2)
                    {
                        return "E";
                    }
                    if (!isOperator(token))
                    {
                        return "E";
                    }
                    secondOperand = rpnStack.Pop();
                    firstOperand = rpnStack.Pop();
                    result = calculate(token, firstOperand, secondOperand, 6);
                    if (result is "E")
                    {
                        return result;
                    }
                    rpnStack.Push(result);
                    opp++;
                }
                else if(token.Length > 1)
                {
                    if (token.Substring(0, 1) == "+") return "E";
                }
                
            }
            if (opp != nummm - 1)
                {
                    return "E";
                }
            //FIXME, what if there is more than one, or zero, items in the stack?
            result = rpnStack.Pop();
            double x = Convert.ToDouble(result);
            return x.ToString();
        }
    }
}
