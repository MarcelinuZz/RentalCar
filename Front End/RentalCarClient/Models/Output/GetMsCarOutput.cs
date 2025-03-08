using System;

namespace RentalCarClient.Models.Output
{
    public class GetMsCarOutput
    {
        public string CarId {get; set;}
        public string name { get; set; }
        public int? number_of_car_seats { get; set; }
        public string transmission { get; set; }
        public decimal? price_per_day { get; set; }
        public bool? status { get; set; }
        public List<string> image_link { get; set; }
    }
}
