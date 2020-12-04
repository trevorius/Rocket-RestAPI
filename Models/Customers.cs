using System;

public class Customer
{
    public long Id { get; set; }
    public DateTime? customer_creation_date { get; set; }
    public string company_name { get; set; }
    public string company_headquarter_address { get; set; }
    public string full_name_company_contact { get; set; }
    public string company_contact_phone { get; set; }
    public string email_company_contact { get; set; }
    public string company_description { get; set; }
    public string full_name_service_technical_authority { get; set; }
    public string technical_authority_phone { get; set; }
    public string technical_manager_email { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public long admin_user_id { get; set; }
    public long address_id { get; set; }
    public long employee_id { get; set; }

}