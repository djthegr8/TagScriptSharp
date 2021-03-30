using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace TagScriptSharp
{
    public class Interpreter
    {
        
    }

    public class Node
    {
        public string Output = "";
        public Verb Verb;
        public Tuple<int, int> Coordinates;
        public Node(Tuple<int, int> _Coordinates, Verb ver = null)
        {
            Verb = ver;
            Coordinates = _Coordinates;
        }

        public override string ToString()
        {
            return Verb + " at " + Coordinates;
        }

        public static List<Node> BuildNodeTree(string message)
        {
            List<Node> nodes = new();
            var previous = @"";
            List<int> starts = new();
            for (var i = 0; i < message.Length; i++)
            {
                var ch = message[i];
                switch (ch)
                {
                    case '{' when previous != @"\\":
                        starts.Add(i);
                        break;
                    case '}' when previous != @"\\":
                    {
                        if (starts.Count == 0)
                        {
                            continue;
                        }

                        var val = starts[^1];
                        starts.RemoveAt(starts.Count - 1);
                        var coords = new Tuple<int, int>(val, i);
                        var n = new Node(coords);
                        nodes.Add(n);
                        break;
                    }
                }
                previous = ch;
            }

        }
    }
}