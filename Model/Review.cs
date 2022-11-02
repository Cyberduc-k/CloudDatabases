namespace Model;

public class Review : EntityBase
{
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public Review()
    {
    }

    public Review(Guid id, string text, DateTime date)
    {
        Id = id;
        Text = text;
        Date = date;
    }
}
