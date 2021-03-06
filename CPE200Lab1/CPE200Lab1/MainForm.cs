﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private bool twotimeop;
        private string firstOperand;
        private string operate;
        private string operated;
        private CalculatorEngine engine;
        private bool twotime;
        private double memories;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            twotimeop = false;
        }

        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (twotimeop)
            {
                lblDisplay.Text = "0";
                twotimeop = false;
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }
        private void btnOpearatorplus_click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "1/x":
                    lblDisplay.Text = (1 / Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;
                case "√":
                    lblDisplay.Text = (Math.Sqrt(Convert.ToDouble(lblDisplay.Text))).ToString();
                    break;
                case "%":
                    lblDisplay.Text = (Convert.ToDouble(lblDisplay.Text) / 100).ToString();
                    break;
            }
            isAllowBack = false;
        }
        private void btnOperator_Click(object sender, EventArgs e)
        {
            string secondOperand = "0";
            operate = ((Button)sender).Text;
            if (((Button)sender).Text != "%")
            {
                operated = operate;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            if (twotime)
            {
                Console.WriteLine(firstOperand);
                Console.WriteLine(secondOperand);
                if (((Button)sender).Text == "%")
                {
                    secondOperand = ((Convert.ToDouble(firstOperand) / 100) * Convert.ToDouble(lblDisplay.Text)).ToString();
                    lblDisplay.Text = secondOperand;
                }
                if (((Button)sender).Text == "+")
                {
                    firstOperand = secondOperand;
                }
                Console.WriteLine(secondOperand);
                Console.WriteLine(operated);
                secondOperand = lblDisplay.Text;
                string result = engine.calculate(operated, firstOperand, secondOperand);
                firstOperand = result;
                operate = ((Button)sender).Text;
                lblDisplay.Text = result;
                twotimeop = true;
                secondOperand = "0";
                return;
            }
            twotime = true;
            twotimeop = true;
            firstOperand = lblDisplay.Text;
            switch (((Button)sender).Text)
            {
                case "+":
                    break;
                case "-":
                    break;
                case "x":
                    break;
                case "÷":
                    isAfterOperater = true;
                    break;
                case "%":
                    lblDisplay.Text = engine.calculate(operate, firstOperand, secondOperand);
                    Console.WriteLine(operate);
                    break;
            }
            isAllowBack = false;
            Console.WriteLine((Button)sender);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = engine.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
            twotime = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnmemory_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "M+":
                    memories += Convert.ToDouble(lblDisplay.Text);
                    break;
                case "M-":
                    memories -= Convert.ToDouble(lblDisplay.Text);
                    break;
                case "MR":
                    lblDisplay.Text = memories.ToString();
                    break;
                case "MC":
                    memories = 0;
                    break;
                case "MS":
                    memories = Convert.ToDouble(lblDisplay.Text);
                    break;
            }
            isAfterEqual = true;
        }
    }
}