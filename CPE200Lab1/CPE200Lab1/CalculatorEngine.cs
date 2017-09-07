﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CPE200Lab1
{
    class CalculatorEngine
    {
        private bool isNumber(string str)
        {
            double retNum;
            return Double.TryParse(str, out retNum);
        }

        private bool isOperator(string str)
        {
            switch (str)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    return true;
            }
            return false;
        }
        public string Process(string str)
        {
            string[] parts = str.Split(' ');
            string x;
            if (!(isNumber(parts[0]) && isOperator(parts[1]) && isNumber(parts[2])))
            {
                return "E";
            }
            else
            {
                for (int j = 0; j < (parts.Length + 1) / 3; j++)
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i] == "÷")
                        {
                            parts[i - 1] = calculate(parts[i], parts[i - 1], parts[i + 1], 4);
                            if (parts.Length > 3)
                            {
                                parts[i + 1] = parts[i + 2];
                                parts[i] = parts[i + 2] = " ";
                                i = 0;
                                x = String.Join("", parts);
                                parts = x.Split(' ');
                            }
                        }
                    }
                for (int j = 0; j < (parts.Length + 1) / 3; j++)
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i] == "X")
                        {
                            parts[i - 1] = calculate(parts[i], parts[i - 1], parts[i + 1], 4);
                            if (parts.Length > 3)
                            {
                                parts[i + 1] = parts[i + 2];
                                parts[i] = parts[i + 2] = " ";
                                i = 0;
                                x = String.Join("", parts);
                                parts = x.Split(' ');
                            }
                        }
                    }
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i] == "+")
                    {
                        parts[i - 1] = calculate(parts[i], parts[i - 1], parts[i + 1], 4);
                        if (parts.Length > 3)
                        {
                            parts[i + 1] = parts[i + 2];
                            parts[i] = parts[i + 2] = " ";
                            i = 0;
                            x = String.Join("", parts);
                            parts = x.Split(' ');
                        }
                    }
                }
                for (int j = 0; j < (parts.Length + 1) / 3; j++)
                    for (int i = 0; i < parts.Length; i++)
                    { 
                        if (parts[i] == "-")
                        {
                            parts[i - 1] = calculate(parts[i], parts[i - 1], parts[i + 1], 4);
                            if (parts.Length > 3)
                            {
                                parts[i + 1] = parts[i + 2];
                                parts[i] = parts[i + 2] = " ";
                                i = 0;
                                x = String.Join("", parts);
                                parts = x.Split(' ');
                            }
                        }
                        Console.WriteLine(parts[0]);
                    }
                return parts[0];
            }
        }
        public string unaryCalculate(string operate, string operand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "√":
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = Math.Sqrt(Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString();
                    }
                case "1/x":
                    if (operand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (1.0 / Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString();
                    }
                    break;
            }
            return "E";
        }

        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;
                case "%":
                    if (firstOperand == "0")
                    {
                        return (Convert.ToDouble(secondOperand) / 100).ToString();
                    }
                    return (Convert.ToDouble(firstOperand) * (Convert.ToDouble(secondOperand) / 100)).ToString();
            }
            return "E";
        }
    }
}
