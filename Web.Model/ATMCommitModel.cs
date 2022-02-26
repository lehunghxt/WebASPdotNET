namespace Web.Model
{
    public class ATMCommitModel
    {
        public string access_key { get; set; }
        public string command { get; set; }
        public string trans_ref { get; set; }
        public string signature { get; set; }
    }
}