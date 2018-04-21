namespace Mobile.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Commentator { get; set; }
    }
}
