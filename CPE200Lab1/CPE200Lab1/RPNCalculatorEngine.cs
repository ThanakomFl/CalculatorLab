using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string RPNProcess(string str)
        {
            string first, second, operand,result;
            Stack myStack = new Stack();
            List<string> parts = str.Split(' ').ToList<string>();
                for (int i = 0; i < parts.Count; i++)
                {
                    if (isNumber(parts[i]))
                    {
                        myStack.Push(parts[i]);
                    }
                    if (isOperator(parts[i]))
                    {
                        operand = parts[i];
                        second = myStack.Pop().ToString();
                        first = myStack.Pop().ToString();
                        Console.WriteLine(second);
                        Console.WriteLine(first);
                        Console.WriteLine(operand);
                        myStack.Push(calculate(operand, first, second, 4).ToString());
                    }
                }
            result = myStack.Pop().ToString();
            return result;
        }
    }
}
       
  
