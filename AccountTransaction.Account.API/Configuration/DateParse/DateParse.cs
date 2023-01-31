namespace AccountTransaction.Account.API.Configuration.DateParse
{
    public class DateParse
    {
        public int DIA { get; set; }
        public int MES { get; set; }
        public int ANO { get; set; }

        public DateTime DataParseada => new DateTime(ANO,MES,DIA);

        public DateParse(string data)
        {
            var dateParser = data.Split("/");
            ANO = int.Parse($"20{dateParser.Last()}");
            MES = int.Parse(dateParser.First());
            DIA = DateTime.DaysInMonth(ANO, MES);
        }
    }
}
