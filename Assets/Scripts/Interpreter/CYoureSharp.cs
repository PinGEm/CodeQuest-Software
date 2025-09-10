using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CYoureSharpPackage;
using TMPro;
using System;
using UnityEngine.UI;

namespace CYoureSharpPackage
{
    public class CYoureSharp : MonoBehaviour
    {
        static bool hadError = false;
        [SerializeField] private TMP_InputField userInput;
        [SerializeField] private static TextMeshProUGUI errorMessage;
        private static int ext_LineNumber = 0;
        bool showPicture = false;

        [SerializeField] private GameObject referenceImage;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private GameObject winUI;
        //[SerializeField] private TMP_Text outputText;

        private void Awake()
        {
            errorMessage = GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<TextMeshProUGUI>();
        }

        public void onShowPicturePress()
        {
            if (!showPicture)
            {
                buttonText.text = "Hide Reference Picture";
                showPicture = true;
                referenceImage.SetActive(true);
                // Show Picture
            }
            else if (showPicture)
            {
                buttonText.text = "Show Reference Picture";
                showPicture = false;
                referenceImage.SetActive(false);
                // Disable Picture
            }
        }

        public void onRunButtonPress()
        {
            errorMessage.text = "";
            hadError = false;
            string fullText = userInput.text;

            fullText = fullText.Replace('\v', '\n');

            if (fullText.Length == 0 || fullText == string.Empty)
            {
                BasicReport("Error: There is no code output");
            }

            string[] lines = fullText.Split(new[]
            { "\r\n", "\r", "\n"}, System.StringSplitOptions.None);
/*            foreach (char c in lines[0])
            {
                Debug.Log($"Character: '{c}' :: ASCII VALUE IS: {System.Convert.ToInt32(c)}");
            }*/


            List<string> lines_ = new List<string>();

            foreach (string line in lines_)
            {
                Debug.Log(line + " ");
            }


            // FOR ARRAYS
            for (int i = 0; i < lines.Length; i++)
            {
                if (hadError)
                {
                    Debug.LogError("FAILURE IN SYNTAX, skipping line...");
                    hadError = false; // reset hadError so debug message will also reset
                    continue;
                }

                Debug.Log($"Line {i + 1}: " + lines[i]);
                Run(lines[i], i + 1);
            }
        }

        void Run(string source, int lineNumber)
        {
            ext_LineNumber = lineNumber;
            Debug.Log(ext_LineNumber);

            Scanner scanner = new Scanner(source, lineNumber);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            Expr expression = parser.Parse();

            if (hadError)
            {
                return;
            }

            //outputText.text = expression.ToString();
            Debug.Log("Parsed Expression: " + expression);

            Interpreter interpreter = new Interpreter(expression);

            if (CheckWinCondition())
            {
                winUI.SetActive(true);
                Debug.Log("You won!");
            }
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        static void Report(int line, string where, string message)
        {
            errorMessage.text = $"[line: {line}] Error {where}: {message}";
            Debug.LogError($"[line: {line}] Error {where}: {message}");
            hadError = true;
        }

        public static void ExternalReport(int line, string where, string message)
        {
            Report(line, where, message);
        }

        public static void ExternalReport(string message)
        {
            BasicReport(message);
        }

        public static void BasicReport(string message)
        {
            errorMessage.text = $"Error Encountered: {message}";
            Debug.LogError($"Error Encountered: {message}");
        }

        private bool CheckWinCondition()
        {
            if (DataManager.Instance.blocks[(0,0)].gameObject.name != "0")
            {
                Debug.Log("the block at 0,0 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(1, 0)].gameObject.name != "1")
            {
                Debug.Log("the block at 1,0 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(2, 0)].gameObject.name != "2")
            {
                Debug.Log("the block at 2,0 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(0, 1)].gameObject.name != "3")
            {
                Debug.Log("the block at 0,1 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(1, 1)].gameObject.name != "4")
            {
                Debug.Log("the block at 1,1 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(2, 1)].gameObject.name != "5")
            {
                Debug.Log("the block at 2,1 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(0, 2)].gameObject.name != "6")
            {
                Debug.Log("the block at 0,2 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(1, 2)].gameObject.name != "7")
            {
                Debug.Log("the block at 1,2 is NOT the right block");
                return false;
            }

            if (DataManager.Instance.blocks[(2, 2)].gameObject.name != "8")
            {
                Debug.Log("the block at 2,2 is NOT the right block");
                return false;
            }

            return true;
        }
    }
}