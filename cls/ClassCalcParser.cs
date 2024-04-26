/* https://www.codeproject.com/Articles/21137/Inside-the-Mathematical-Expressions-Evaluator */
using System.Text;
using System.Text.RegularExpressions;

namespace ClipMenu
{
    public partial class CalcClass
    {
        Stack<double> operands;
        Stack<string> operators;
        string token;
        int tokenPos;
        string expression;

        public string TokenString { get { return expression; } }

        public CalcClass() { Reset(); }

        public void Reset()
        {
            operands = new Stack<double>();
            operators = new Stack<string>();
            operators.Push(Token.Sentinel);
            token = Token.None;
            tokenPos = -1;
        }

        public double Evaluate(string expr)
        {   // (negative look behind: ?<!...)-(positive look ahead: ?=...)
            // + : A plus sign matches one or more of the preceding character
            // * : An asterisk matches zero or more of the preceding character
            expr = Regex.Replace(expr, @"(?<!\d+\s*)-(?=\d+)", "_"); // unary minus
            expr = Regex.Replace(expr, @"(?<=\d+\s*)(e|pi)", "*$1", RegexOptions.IgnoreCase); // E oder pi multiplizieren statt verketten
            expr = expr.ToLower().Replace("~", ""); // muss vor Math.PI und Math.E stehen; String.Replace ist immer Case-Sensitive!
            expr = expr.Replace("pi", Math.PI.ToString());
            expr = expr.Replace("e", Math.E.ToString());
            expr = expr.Replace(',', '.'); // muss nach Math.PI und Math.E stehen!
            Reset();
            expression = expr;
            if (Normalize(ref expression)) { return Parse(); }
            else { return 0; } // ThrowException("Leer");
        }

        private double Parse()
        {
            ParseBinary();
            Expect(Token.End);
            return operands.Peek();
        }

        private void ParseBinary()
        {// Parse binary operations
            ParsePrimary();
            while (Token.IsBinary(token))
            {
                PushOperator(token);
                NextToken();
                ParsePrimary();
            }
            while (operators.Peek() != Token.Sentinel) { PopOperator(); }
        }

        private void ParsePrimary()
        {// Parse primary tokens: digits, variables, parentheses
            if (Token.IsDigit(token)) { ParseDigit(); }
            else if (Token.IsUnary(token))
            {
                PushOperator(Token.ConvertOperator(token));
                NextToken();
                ParsePrimary();
            }
            else if (token == Token.PLeft) // parentheses
            {
                NextToken();
                operators.Push(Token.Sentinel); // add sentinel to operators stack
                ParseBinary();
                Expect(Token.PRight);
                operators.Pop();
            }
            else { ThrowException("Syntaxfehler"); }
        }

        private void ParseDigit()
        {
            StringBuilder tmpNumber = new();
            while (Token.IsDigit(token))
            {
                tmpNumber.Append(token);
                NextToken();
            }
            try { operands.Push(double.Parse(tmpNumber.ToString(), System.Globalization.CultureInfo.InvariantCulture)); }
            catch { ThrowException("Syntaxfehler: " + tmpNumber.ToString()); }
        }

        private void PushOperator(string op)
        {
            while (Token.Precedence(operators.Peek()) >= Token.Precedence(op)) { PopOperator(); }
            operators.Push(op);
        }

        private void PopOperator()
        {
            if (Token.IsBinary(operators.Peek()))
            {
                try
                {
                    double o2 = operands.Pop();
                    double o1 = operands.Pop();
                    Calculate(operators.Pop(), o1, o2);
                }
                catch { ThrowException("Stapelfehler"); }
            }
            else { Calculate(operators.Pop(), operands.Pop()); }  // unary operator
        }

        private void NextToken()
        {// Get next token from the expression
            if (token != Token.End) { token = expression[++tokenPos].ToString(); }
        }

        private void Expect(string token)
        {
            if (this.token == token) { NextToken(); }
            else { ThrowException("Syntaxfehler"); } //: " + Token.ToString(token) + "  erwartet");
        }

        private bool Normalize(ref string s)
        {// Normalizes expression
            s = s.Replace(" ", "").Replace("\t", " ") + Token.End;
            if (s.Length >= 2)
            {// Returns true, if expression is suitable for evaluating
                NextToken();
                return true;
            }
            return false;
        }

        private void ThrowException(string message) { throw new CalculateException(message, tokenPos); }

        private void Calculate(string op, double operand1, double operand2)
        {// Calculates binary expressions and pushes the result into the operands stack
            switch (op)
            {
                case Token.Add: operands.Push(operand1 + operand2); break;
                case Token.Subtract: operands.Push(operand1 - operand2); break;
                case Token.Multiply: operands.Push(operand1 * operand2); break;
                case Token.Divide: operands.Push(operand1 / operand2); break;
                case Token.Power: operands.Push(Math.Pow(operand1, operand2)); break;
            }
        }

        private void Calculate(string op, double token)
        {// Calculates unary expressions and pushes the result into the operands stack
            switch (op) { case Token.UnaryMinus: operands.Push(-token); break; } // param name="op" Unary operator
        }
    }

    public class CalculateException : Exception
    {
        int position;

        public CalculateException(string message, int position) : base(message) { this.position = position; } // "Syntaxfehler\r\n" + ..." + position.ToString() ...

        public int TokenPosition { get { return position; } }
    }
    internal static class Token
    {
        public const string PRight = ")", PLeft = "(", Power = "^", Divide = "/",
                           Multiply = "*", UnaryMinus = "_", Add = "+", Subtract = "-",
                           Sentinel = "#", End = ";", Operand = "@", None = " ";
        private static readonly string[] binaryOperators = [Multiply, Divide, Subtract, Add, Power];
        private static readonly string[] unaryOperators = [UnaryMinus];
        private static readonly string[] digitChars = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "."];

        public static int Precedence(string op)
        {// Operatorrangfolge, -wertigkeit, -priorität oder -präzedenz
            return op switch { Subtract => 4, Add => 4, Multiply => 8, Divide => 8, Power => 16, PLeft => 32, PRight => 32, UnaryMinus => 64, _ => 0, };
        }

        //#region IsFunctions
        public static bool IsBinary(string op) { return Contains(op, binaryOperators); }

        public static bool IsUnary(string op) { return Contains(op, unaryOperators); }

        public static bool IsDigit(string token) { return Contains(token, digitChars); }
        //#endregion

        public static string ConvertOperator(string op) { return op switch { UnaryMinus => "_", _ => op, }; } // Converts unary operator from expression to driver-comprehensible mode

        public static string ToString(string op) { return op switch { End => "END", _ => op.ToString(), }; }

        static bool Contains(string token, string[] array) { return Array.IndexOf(array, token) != -1; }
    }
}
