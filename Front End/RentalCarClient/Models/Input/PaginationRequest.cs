using System;

namespace RentalCarClient.Models.Input
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string OrderBy {get;set;}
        public DateTime? RentalDate {get;set;}
        public DateTime? ReturnDate {get;set;}
        public int? Year {get;set;}
    }
}
