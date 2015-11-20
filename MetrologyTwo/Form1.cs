using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MetrologyTwo
{
    public partial class Form1 : Form
    {
        string[] ArrayOfFunctions;
        public Form1()
        {
            InitializeComponent();
        }

        public struct InfoAboutVariables
        {
            public string TitleOfVariable;
            public int AmountVariableInCode;
            public InfoAboutVariables(string StringOfName, int AmountOfMeetVariable)
            {
                TitleOfVariable = StringOfName;
                AmountVariableInCode = AmountOfMeetVariable;
            }
        }



        void DeleteCommentsInCode(ref string NewDataOfMainFileWithoutComments)
        {
            bool FlagOfStringComment = false;

            for (int i = 0; i < TextBoxOfMainText.Text.Length; i++)
            {
                if ((TextBoxOfMainText.Text[i] == '/') && (TextBoxOfMainText.Text[i + 1] == '/'))
                    while ((TextBoxOfMainText.Text[i] != '\n'))
                    {
                        i++;
                    }

                if ((TextBoxOfMainText.Text[i] == '/') && (TextBoxOfMainText.Text[i + 1] == '*'))
                {
                    while (FlagOfStringComment == false)
                    {
                        if ((TextBoxOfMainText.Text[i] == '*') && (TextBoxOfMainText.Text[i + 1] == '/'))
                            FlagOfStringComment = true;
                        i++;
                    }
                    i += 2;
                    FlagOfStringComment = false;
                }

                if (i >= TextBoxOfMainText.Text.Length)
                    NewDataOfMainFileWithoutComments += "";
                else
                    NewDataOfMainFileWithoutComments += Convert.ToString(TextBoxOfMainText.Text[i]);
            }

            TextBoxOfMainText.Text = NewDataOfMainFileWithoutComments;
        }

        List<InfoAboutVariables> DoFindVariablesInLine(ref List<InfoAboutVariables> NamesOfVariablesOurCode, string[] ContainsOfLineWithCode)
        {
            for (int i = 0; i < ContainsOfLineWithCode.Length; i++)
            {
                if ((ContainsOfLineWithCode[i].Contains("=") == true) || (ContainsOfLineWithCode[i].Contains(";") == true))
                {
                    if (ContainsOfLineWithCode[i].Contains(";") == true)
                    {

                        ContainsOfLineWithCode[i] = ContainsOfLineWithCode[i].Replace(";", "");
                        NamesOfVariablesOurCode.Add(new InfoAboutVariables(ContainsOfLineWithCode[i], -1));
                    }
                    else
                    {
                        NamesOfVariablesOurCode.Add(new InfoAboutVariables(ContainsOfLineWithCode[i - 1], -1));
                        break;
                    }
                }
            }

            return NamesOfVariablesOurCode;
        }

        void DoEditOurString(ref string OurVariable)
        {
            int StartPositionForDelete = 0, FinishPositionOfdelete = 0;

            for (int k = 0; k < OurVariable.Length; k++)
            {
                if (OurVariable[k] == '=')
                {
                    StartPositionForDelete = k;
                }
                else
                    if ((OurVariable[k] == ',') || (OurVariable[k] == ';'))
                    FinishPositionOfdelete = k;

                if ((StartPositionForDelete != 0) && (FinishPositionOfdelete != 0))
                {
                    OurVariable = OurVariable.Remove(StartPositionForDelete, FinishPositionOfdelete - StartPositionForDelete);
                    StartPositionForDelete = 0;
                    FinishPositionOfdelete = 0;
                }
            }
        }
        List<InfoAboutVariables> DoFindCountOfVariables(ref List<InfoAboutVariables> OurVariablesInCode, string OurAnalyzingCode)
        {
            MatchCollection MatchOfMyVariables = Regex.Matches(@OurAnalyzingCode, @"\bvar[^;]*;");

            foreach (Match i in MatchOfMyVariables)
            {
                string OurVariable = i.Value;
                int LengthOfVar_ = 4;

                OurVariable = OurVariable.Remove(0, LengthOfVar_);

                MatchCollection MatchOfDeleteObjects = Regex.Matches(@OurVariable, @"\bvar[^;]*;");

                foreach (Match j in MatchOfDeleteObjects)
                    OurVariable = OurVariable.Replace(j.Value, "");

                DoEditOurString(ref OurVariable);
                
                MatchCollection MatchOfVariable = Regex.Matches(@OurVariable, @"[[a-zA-Z_]+[0-9]*]*(?=\s*,*=*\s*;*)");

                foreach (Match j in MatchOfVariable)
                {
                    OurVariablesInCode.Add(new InfoAboutVariables(j.Value, -1));
                }
            }

            return OurVariablesInCode;
        }

        List<InfoAboutVariables> DoFindCountOfMeetOfVariables(ref List<InfoAboutVariables> AmountMeetOfVariables, string OurAnalyzingCode)
        {
            InfoAboutVariables[] MyArrayOfCopyMyList = AmountMeetOfVariables.ToArray();

            AmountMeetOfVariables = MyArrayOfCopyMyList.ToList<InfoAboutVariables>();

            for (int i = 0; i < AmountMeetOfVariables.Count; i++)
            {
                string Variable = AmountMeetOfVariables[i].TitleOfVariable;
                bool FlagOfOurCode = true;
                int LengthOfStringOfDataOfMainFile = 0;

                while (FlagOfOurCode)
                {
                    string LineWithCode = "";

                    LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref OurAnalyzingCode);

                    if (LineWithCode.Contains(AmountMeetOfVariables[i].TitleOfVariable) == true)
                        MyArrayOfCopyMyList[i].AmountVariableInCode++;

                    if (OurAnalyzingCode.Length < LengthOfStringOfDataOfMainFile)
                        FlagOfOurCode = false;
                }
            }

            AmountMeetOfVariables = MyArrayOfCopyMyList.ToList<InfoAboutVariables>();

            return AmountMeetOfVariables.ToList();
        }

        void DoOutputInfomationAboutResult(List<InfoAboutVariables> ResultVariables)
        {
            TextBoxOfResultText.Text += "Глобальные переменные: ";
            for (int i = 0; i < ResultVariables.Count; i++)
            {
                TextBoxOfResultText.Text +=Environment.NewLine + ResultVariables[i].TitleOfVariable + "-" + Convert.ToString(ResultVariables[i].AmountVariableInCode) + Environment.NewLine;
            }
        }



        string DoFindAllFunctions(string DataOfMainFileWithoutComments)
        {
            int CounterOfElementsOFMyMatch = 0;
            MatchCollection myMatch = Regex.Matches(@DataOfMainFileWithoutComments, @"\b\s*function[\s*\n*]*[a-zA-Z_0-9]+\s*\([^\)]*\)[\s*\n*]*\{");
            ArrayOfFunctions = new string[myMatch.Count];
            string AdditionalVariableForOurTextWhereWillDeleteFunction = DataOfMainFileWithoutComments;

            foreach (Match i in myMatch)
            {
                CounterOfElementsOFMyMatch++;

                int LeftBracketsOfFunction = 1, RightBracketsOfFunction = 0, IndexOfOurFunction = i.Index,
                    LengthOfOurFunction = i.Length;
                string OurFunctionInDataOfMain = i.Value;

                while (LeftBracketsOfFunction != RightBracketsOfFunction)
                {

                    if (DataOfMainFileWithoutComments[IndexOfOurFunction + LengthOfOurFunction] == '{')
                        LeftBracketsOfFunction += 1;
                    else
                        if (DataOfMainFileWithoutComments[IndexOfOurFunction + LengthOfOurFunction] == '}')
                        RightBracketsOfFunction += 1;

                    OurFunctionInDataOfMain += DataOfMainFileWithoutComments[IndexOfOurFunction + LengthOfOurFunction];
                    IndexOfOurFunction += 1;
                }

                ArrayOfFunctions[CounterOfElementsOFMyMatch - 1] = OurFunctionInDataOfMain;
                AdditionalVariableForOurTextWhereWillDeleteFunction = AdditionalVariableForOurTextWhereWillDeleteFunction.Replace(OurFunctionInDataOfMain, "");
                TextBoxOfMainText.Text = AdditionalVariableForOurTextWhereWillDeleteFunction;
            }

            DataOfMainFileWithoutComments = AdditionalVariableForOurTextWhereWillDeleteFunction;

            return DataOfMainFileWithoutComments;
        }

        string DoReadStringOfNewDataOfMainFileWithoutComments(ref int LengthOfStringOfDataOfMainFile, ref string OurMainCode)
        {
            string LineWithCode = "";

            while ((LengthOfStringOfDataOfMainFile < OurMainCode.Length) && (OurMainCode[LengthOfStringOfDataOfMainFile] != '\n'))
            {
                LineWithCode += Convert.ToString(OurMainCode[LengthOfStringOfDataOfMainFile]);
                LengthOfStringOfDataOfMainFile++;
            }

            LengthOfStringOfDataOfMainFile++;

            return LineWithCode;
        }

        List<InfoAboutVariables> DoFindCountOfMeetOfVariable(ref List<InfoAboutVariables> GlobalVariables, string StringOfOurFunction, int IndexOfList)
        {
            InfoAboutVariables[] MyArrayOfCopyMyList = GlobalVariables.ToArray();

            GlobalVariables = MyArrayOfCopyMyList.ToList<InfoAboutVariables>();

            string Variable = GlobalVariables[IndexOfList].TitleOfVariable;
            bool FlagOfOurCode = true;
            int LengthOfStringOfDataOfMainFile = 0;

            while (FlagOfOurCode)
            {
                string LineWithCode = "";

                LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref StringOfOurFunction);

                if (LineWithCode.Contains(GlobalVariables[IndexOfList].TitleOfVariable) == true)
                    MyArrayOfCopyMyList[IndexOfList].AmountVariableInCode++;

                if (StringOfOurFunction.Length < LengthOfStringOfDataOfMainFile)
                    FlagOfOurCode = false;
            }

            GlobalVariables = MyArrayOfCopyMyList.ToList<InfoAboutVariables>();

            return GlobalVariables;
        }

        void DoReadOurLocalVariablesInFunction(ref string OurLocalValuesOfFunction,ref string FunctionName, ref string StringOfOurFunction)
        {
            string LineWithCode = "";
            int LengthOfStringOfDataOfMainFile = 0,StartPositionInName = 0, FinishPositionInName = 0; ;

            LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref StringOfOurFunction);

            if (LineWithCode.Contains("function"))
            {

                for (int j = 0; j < LineWithCode.Length; j++)
                {
                    if (LineWithCode[j] == '(')
                    {
                        StartPositionInName = j;
                        do
                        {
                            OurLocalValuesOfFunction += LineWithCode[j];
                            j++;
                        } while (LineWithCode[j] != ')');

                        FinishPositionInName = j + 1;
                        OurLocalValuesOfFunction += ')';
                    }
                }

                FunctionName = LineWithCode.Remove(StartPositionInName, FinishPositionInName - StartPositionInName);
                FunctionName = FunctionName.Replace("function", "");
                FunctionName = FunctionName.Replace('{', ' ');

            }
        }

        void DoCompareGlobalWithLocalWariables(ref List<InfoAboutVariables> GlobalVariables,ref List<InfoAboutVariables> LocalVariables, ref string StringOfOurFunction)
        {
            for (int j = 0; j < GlobalVariables.Count; j++)
            {
                bool FlagOfTheSameVariables = false;

                for (int k = 0; k < LocalVariables.Count; k++)
                {
                    if (LocalVariables[k].TitleOfVariable != GlobalVariables[j].TitleOfVariable)
                        FlagOfTheSameVariables = true;
                    else
                    {
                        FlagOfTheSameVariables = false;
                        break;
                    }

                    if ((FlagOfTheSameVariables) && (k == LocalVariables.Count - 1))
                    {
                        GlobalVariables = DoFindCountOfMeetOfVariable(ref GlobalVariables, StringOfOurFunction, j);
                    }
                }

            }
        }

        void OutPutLocalVariables(ref string FunctionName, ref List<InfoAboutVariables> LocalVariables)
        {
            FunctionName = "Function " + FunctionName + "(";
            for (int j = 0; j < LocalVariables.Count; j++)
            {
                FunctionName += LocalVariables[j].TitleOfVariable + " " + LocalVariables[j].AmountVariableInCode + Environment.NewLine;

            }
            FunctionName += ")";
            LocalVariables.Clear();
            TextBoxOfResultText.Text += FunctionName + Environment.NewLine;
        }
        List<InfoAboutVariables> DoFindCountLocalVariables(ref List<InfoAboutVariables> LocalVariables,ref List<InfoAboutVariables> GlobalVariables)
        {
            string StringOfOurFunction = "";
            
            for (int i = 0; i < ArrayOfFunctions.Length; i++)
            {
                
                string OurLocalValuesOfFunction = "", FunctionName = "";
                StringOfOurFunction = ArrayOfFunctions[i];

                DoReadOurLocalVariablesInFunction(ref OurLocalValuesOfFunction,ref FunctionName,ref StringOfOurFunction);   
                
                MatchCollection MatchOfDeleteObjects = Regex.Matches(@OurLocalValuesOfFunction, @"\b[[a-zA-Z_]+[0-9]*]*(?=\s*,*=*\s*;*)");

                foreach (Match j in MatchOfDeleteObjects)
                    LocalVariables.Add(new InfoAboutVariables(j.Value, -1));

                DoFindCountOfVariables(ref LocalVariables, StringOfOurFunction);

                LocalVariables = DoFindCountOfMeetOfVariables(ref LocalVariables, StringOfOurFunction);

                DoCompareGlobalWithLocalWariables(ref GlobalVariables,ref LocalVariables,ref StringOfOurFunction);

                OutPutLocalVariables(ref FunctionName, ref LocalVariables);

            }
            return LocalVariables;
        }
        void DoMainTask()
        {
            string NewDataOfMainFileWithoutComments = "";

            List<InfoAboutVariables> GlobalVariables = new List<InfoAboutVariables>();

            DeleteCommentsInCode(ref NewDataOfMainFileWithoutComments);

            NewDataOfMainFileWithoutComments = DoFindAllFunctions(NewDataOfMainFileWithoutComments);

            GlobalVariables = DoFindCountOfVariables(ref GlobalVariables, NewDataOfMainFileWithoutComments);

            GlobalVariables = DoFindCountOfMeetOfVariables(ref GlobalVariables, NewDataOfMainFileWithoutComments);

            List<InfoAboutVariables> LocalVariables = new List<InfoAboutVariables>();

            LocalVariables = DoFindCountLocalVariables(ref LocalVariables,ref GlobalVariables);
            
            DoOutputInfomationAboutResult(GlobalVariables);
        }
        private void ButtonOfProccedTask_Click(object sender, EventArgs e)
        {
            ButtonOfProccedTask.Enabled = false;
            DoMainTask();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName, Encoding.Default);

                TextBoxOfMainText.Text = sr.ReadToEnd();
                MainTextBox.Text = TextBoxOfMainText.Text;
                sr.Close();

                if (TextBoxOfMainText.Text != "")
                    ButtonOfProccedTask.Enabled = true;
            }
        }
    }
}
