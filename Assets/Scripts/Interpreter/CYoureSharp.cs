using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CYoureSharpPackage;
using TMPro;
using System;

namespace CYoureSharpPackage
{
    public class CYoureSharp : MonoBehaviour
    {
        static bool hadError = false;
        [SerializeField] private TMP_InputField userInput;
        [SerializeField] private TMP_Text outputText;

        public void onRunButtonPress()
        {
            hadError = false;
            string fullText = userInput.text;

            string[] lines = fullText.Split(new[]
            { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);


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
            Scanner scanner = new Scanner(source, lineNumber);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            Expr expression = parser.Parse();

            if (hadError)
            {
                return;
            }

            outputText.text = expression.ToString();
            Debug.Log("Parsed Expression: " + expression);

            Interpreter interpreter = new Interpreter(expression);
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        static void Report(int line, string where, string message)
        {
            Debug.LogError($"[line: {line}] Error {where}: {message}");
            hadError = true;
        }

        public static void ExternalReport(int line, string where, string message)
        {
            Report(line, where, message);
        }
    }
}