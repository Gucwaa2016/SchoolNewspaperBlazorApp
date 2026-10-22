using System.ComponentModel.DataAnnotations;
using SchoolNewspaperBlazorApp.Data.Enums;

namespace SchoolNewspaperBlazorApp.Data
{
    public class Article
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        public required string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int MainImageId { get; set; }
        public EnumArticleCategory Category { get; set; }

    }

    
}
