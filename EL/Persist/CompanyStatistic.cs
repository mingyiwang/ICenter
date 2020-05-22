namespace EL.Persist
{

    public class CompanyStatistic
    {
        public int Id            { get; set; }
        public int CompanyId     { get; set; }
        public int Year          { get; set; }
        public int NumOfPerson   { get; set; }
        public decimal TurnOver  { get; set; }
        public decimal Tax       { get; set; }
        public decimal RdCost    { get; set; }
        public string  Data      { get; set; }

    }

}