#region Using directives

#define USE_TRACING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Reflection;
//using Google.GData.Extensions;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// standard string tokenizer class. Pretty much cut/copy/paste out of MSDN. 
    /// </summary>
    public class TokenCollection : IEnumerable
    {
        private string[] elements;

        /// <summary>Constructor, takes a string and a delimiter set</summary> 
        public TokenCollection(string source, char[] delimiters)
        {

            if (source != null)
            {
                this.elements = source.Split(delimiters);
            }
        }

        /// <summary>Constructor, takes a string and a delimiter set</summary> 
        public TokenCollection(string source, char delimiter, bool separateLines, int resultsPerLine)
        {
            if (source != null)
            {
                if (separateLines)
                {
                    // first split the source into a line array
                    string[] lines = source.Split(new char[] { '\n' });
                    int size = lines.Length * resultsPerLine;
                    this.elements = new string[size];
                    size = 0;
                    foreach (String s in lines)
                    {
                        // do not use Split(char,int) as that one
                        // does not exist on .NET CF
                        string[] temp = s.Split(delimiter);
                        int counter = temp.Length < resultsPerLine ? temp.Length : resultsPerLine;

                        for (int i = 0; i < counter; i++)
                        {
                            this.elements[size++] = temp[i];
                        }
                        for (int i = resultsPerLine; i < temp.Length; i++)
                        {
                            this.elements[size - 1] += delimiter + temp[i];
                        }

                    }
                }
                else
                {
                    string[] temp = source.Split(delimiter);
                    resultsPerLine = temp.Length < resultsPerLine ? temp.Length : resultsPerLine;
                    this.elements = new string[resultsPerLine];

                    for (int i = 0; i < resultsPerLine; i++)
                    {
                        this.elements[i] = temp[i];
                    }
                    for (int i = resultsPerLine; i < temp.Length; i++)
                    {
                        this.elements[resultsPerLine - 1] += delimiter + temp[i];
                    }
                }
            }
        }

        /// <summary>
        /// creates a dictionary of tokens based on this tokencollection
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CreateDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < this.elements.Length; i += 2)
            {
                string key = this.elements[i];
                string val = this.elements[i + 1];
                dict.Add(key, val);
            }

            return dict;
        }

        /// <summary>IEnumerable Interface Implementation, for the noninterface</summary> 
        public TokenEnumerator GetEnumerator() // non-IEnumerable version
        {
            return new TokenEnumerator(this);
        }

        /// <summary>IEnumerable Interface Implementation</summary> 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new TokenEnumerator(this);
        }

        /// <summary>Inner class implements IEnumerator interface</summary> 
        public class TokenEnumerator : IEnumerator
        {
            private int position = -1;
            private TokenCollection tokens;

            /// <summary>Standard constructor</summary> 
            public TokenEnumerator(TokenCollection tokens)
            {
                this.tokens = tokens;
            }

            /// <summary>IEnumerable::MoveNext implementation</summary> 
            public bool MoveNext()
            {
                if (this.tokens.elements != null && position < this.tokens.elements.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>IEnumerable::Reset implementation</summary> 
            public void Reset()
            {
                position = -1;
            }

            /// <summary>Current implementation, non interface, type-safe</summary> 
            public string Current
            {
                get
                {
                    return this.tokens.elements != null ? this.tokens.elements[position] : null;
                }
            }

            /// <summary>Current implementation, interface, not type-safe</summary> 
            object IEnumerator.Current
            {
                get
                {
                    return this.tokens.elements != null ? this.tokens.elements[position] : null;
                }
            }
        }
    }
}
