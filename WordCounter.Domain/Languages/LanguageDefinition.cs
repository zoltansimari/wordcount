using System.Linq;

namespace WordCounter.Domain.Languages
{
    public abstract class LanguageDefinition
    {
        public abstract char[] Punctuation { get; }

        public char[] WhiteSpaces
        {
            get { return new[] { ' ', '\r', '\t', '\n' }; }
        }

        public char[] Brackets
        {
            get { return new[] { '(', ')', '{', '}', '[', ']', '<', '>' }; }
        }

        public char[] Numbers
        {
            get { return new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; }
        }

        public char[] SpecialCharacters
        {
            get
            {
                return Brackets.Concat(Punctuation).Concat(WhiteSpaces).Concat(Numbers).ToArray();
            }
        }
    }
}
