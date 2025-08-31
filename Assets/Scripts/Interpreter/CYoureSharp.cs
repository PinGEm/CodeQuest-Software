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
            Debug.Log("Testing and Running: " + source);
            // Scanner -> Parser -> Interpreter goes here
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            Debug.LogError($"[line: {line}] Error {where}: {message}");
            hadError = true;
        }
    }
}