using System;

public class Elevator
{
    private DateTime _date = DateTime.Now;

    public long Id { get; set; }
    public string model { get; set; } 
    public string type_of_building { get; set; }
    public string status { get; set; }
    //public DateTime commissioning_date { get; set; }
    public DateTime last_inspection_date { get; set; }
    public string inspection_certificate { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    // created_at   
    public DateTime updated_at { get {return _date; } set { _date = value; } }
    public long column_id { get; set; }
    public long customer_id { get; set; }

}