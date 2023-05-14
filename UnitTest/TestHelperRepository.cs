using DocumentManager.Infrastructure.ContextDB;
using DocumentManager.Infrastructure.ModelDB;
using DocumentManager.Infrastructure.RepositoryDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestHelperRepository
    {
        private readonly DocManagerContext _context;
        public TestHelperRepository()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<DocManagerContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
                .Options;

            _context = new DocManagerContext(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var documentTest = new DocumentDataBase
            {
                Title = "A",
                Description = "B",
                Content = new List<string> { "A", "B" },
                Files = new List<FileLinkDataBase>(),
                Pictures = new List<PictureLinkDataBase>(),
                Videos = new List<VideoLinkDataBase>(),
            };

            _context.Add(documentTest);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();
        }

        public IDocumentRepositoryAsync GetDocumentRepository
        {
            get
            {
                return new DocumentRepositoryAsync(_context);
            }
        }
    }
}
