namespace RstiNotifierBot.Dto
{
    internal class NewsDto
    {
        public NewsDto(string title, string preview, string date, string xref)
        {
            Title = title;
            Preview = preview;
            Date = date;
            Xref = xref;
        }

        public string Title { get; private set; }

        public string Preview { get; private set; }

        public string Date { get; private set; }

        public string Xref { get; private set; }
    }
}
