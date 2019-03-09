using System.Collections.Generic;
using System.Threading.Tasks;
using DatinApp.API.Helpers;
using DatinApp.API.Models;

namespace DatinApp.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);

         Task<User> GetUser(int id, bool withApproved);

         Task<Photo> GetPhoto(int id, bool withApproved);

         Task<PagedList<Photo>> GetPhotoForApprove(PaggingParam param);

         Task<Photo> GetMainPhotoForUser(int userId);

         Task<Like> GetLike(int userId, int recipientId);

         Task<Message> GetMessage(int id);

         Task<PagedList<Message>> GetMessagesForUser(MessageParams messagesParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}