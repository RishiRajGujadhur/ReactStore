public class StoreEntry
{
    public int LastModifiedUserId
    {
        get;
        set;
    }

    public DateTime? LastModifiedTimestamp
    {
        get;
        set;
    }

    public DateTime CreatedAtTimestamp {get; set;}
    public int? CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; }
    public string LastModifiedUserName { get; set; }
}
