namespace NapocaBike.Models
{
    public class BikeParking
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Capacity { get; set; }
        public int SecurityLevel { get; set; }
        
    }
}
