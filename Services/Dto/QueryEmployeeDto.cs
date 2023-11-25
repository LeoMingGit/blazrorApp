namespace Services.Dto
{
    public class QueryEmployeeDto
    {

        public  string HomePhone { get; set; }


        public int PageIndex { get; set; } = 1;


        public int PageSize { get; set; } = 10;

        
    }
}
