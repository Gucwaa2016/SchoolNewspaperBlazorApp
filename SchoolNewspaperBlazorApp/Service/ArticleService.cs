using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolNewspaperBlazorApp.Data;
using SchoolNewspaperBlazorApp.Data.Enums;
using SchoolNewspaperBlazorApp.Interfaces.Repository;
using SchoolNewspaperBlazorApp.Interfaces.Service;

namespace SchoolNewspaperBlazorApp.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;
        public ArticleService(IArticleRepository articleRepository, IPermissionService permissionService,INotificationService notificationService)
        {
            _articleRepository = articleRepository;
            _permissionService = permissionService;
            _notificationService = notificationService;
        }
        public async Task AddArticleAsync(string title, string text, string author,int fileId)
        {
            bool hasPermission = await _permissionService.HasPermissionAsync(EnumPermissions.AddArticle);
            if (!hasPermission)
            {
                _notificationService.Show("Brak uprawnień!", EnumNotificationType.Failure);
                return;
            }
            var article = new Article
            {
                Title = title,
                Text = text,
                Author = author,
                PublishDate = DateTime.Now,
                FileId = fileId
            };
            var succces = await _articleRepository.AddArticleAsync(article);
            if(succces)
            {
                _notificationService.Show("Pomyślnie dodano artykuł!", EnumNotificationType.Success);
            }
            else
            {
                _notificationService.Show("Podczas dodawania artykułu wystąpił bład! Skontaktuj się z obłusga techniczna!", EnumNotificationType.Error);
            }
        }
        public async Task UpdateArticleByIdAsync(int id,string title,string text,string author, int fileId)
        {
            bool hasPermission = await _permissionService.HasPermissionAsync(EnumPermissions.EditArticle);
            if (!hasPermission)
            {
                _notificationService.Show("Brak uprawnień!", EnumNotificationType.Failure);
                return;
            }
            var article = new Article
            {
                Id = id,
                Title = title,
                Text = text,
                Author = author,
                FileId = fileId
            };
           var success =  await _articleRepository.UpdateArticleByIdAsync(article);
            if(success)
            {
                _notificationService.Show("Pomyślnie zaaktualizowano artykuł!", EnumNotificationType.Success);
            }
            else
            {
                _notificationService.Show("Podczas edytowania artykułu wystąpił bład! Skontaktuj się z obłusga techniczna!", EnumNotificationType.Error);
            }
        }
        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllArticlesAsync();
        }
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            Article article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                throw new Exception($"Article with ID {id} not found.");
            }
            return article;
        }
        public async Task<bool> RemoveArticleByIdAsync(int id)
        {
            bool hasPermission = await _permissionService.HasPermissionAsync(EnumPermissions.DeleteArticle);
            if (!hasPermission)
            {
                _notificationService.Show("Brak uprawnień!", EnumNotificationType.Failure);
                return false;
            }
            if (id > 0)
            {
                _notificationService.Show("Podczas usuwania artykułu wystąpił bład! Skontaktuj się z obłusga techniczna!", EnumNotificationType.Error);
                await _articleRepository.RemoveArticleByIdAsync(id);
                return false;

            }
            _notificationService.Show("Pomyślnie usunięto artykuł!", EnumNotificationType.Success);
            return true;
        }
    }
}
