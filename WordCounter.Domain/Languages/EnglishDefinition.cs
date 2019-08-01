namespace WordCounter.Domain.Languages
{
    public class EnglishDefinition : LanguageDefinition
    {
        public override char[] Punctuation
        {
            get { return new[] { '.', ',', '?', '!', '-', ':', ';', '/', '"', '`', '\'' }; }
        }
    }
}
