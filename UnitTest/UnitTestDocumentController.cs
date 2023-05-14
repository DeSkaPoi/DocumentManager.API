using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocumentManager.Infrastructure.RepositoryDB;
using DocumentManager.Infrastructure.ModelDB;
using System.Linq;

namespace UnitTest
{
    public class UnitTestDocumentController
    {
        [Fact]
        //Тест, проверяющий, что база данных создалась
        public void VoidTest()
        {
            var testHelper = new TestHelperRepository();
            var documentRepository = testHelper.GetDocumentRepository;

            Assert.NotNull(documentRepository);
        }

        [Fact]
        //Тест, проверяющий добавление новых данных
        public async Task TestAdd()
        {
            // Arrange
            var testHelper = new TestHelperRepository();
            var documentRepository = testHelper.GetDocumentRepository;

            var documentTest = new DocumentDataBase
            {
                Id = Guid.NewGuid(),
                Title = "Test Document",
                Description = "Document for testing",
                Content = new List<string> { "A", "B" },
                Files = new List<FileLinkDataBase>(),
                Pictures = new List<PictureLinkDataBase>(),
                Videos = new List<VideoLinkDataBase>(),
            };

            await documentRepository.Add(documentTest);
            documentRepository.ChangeTrackerClear();

            // Act
            var documents = await documentRepository.GetAll();

            // Assert
            Assert.True(documents.Count == 2);
            Assert.Equal("A", documents.FirstOrDefault(d => d.Title == "A").Title);
            Assert.Equal("Test Document", documents.FirstOrDefault(d => d.Title == "Test Document").Title);
        }

        [Fact]
        public async Task TestUpdate()
        {
            // Arrange
            var testHelper = new TestHelperRepository();
            var documentRepository = testHelper.GetDocumentRepository;
            var documents = await documentRepository.GetAll();
            var doc = documents.FirstOrDefault();
            var guid = doc.Id;
            //personRepository.ChangeTrackerClear();
            doc.Pictures.Add(new PictureLinkDataBase());

            // Act
            await documentRepository.Update(doc);
            doc = await documentRepository.GetById(guid);

            // Assert
            Assert.Single(doc.Pictures);
        }

        [Fact]
        public async Task TestDelete()
        {
            // Arrange
            var testHelper = new TestHelperRepository();
            var documentRepository = testHelper.GetDocumentRepository;
            var documents = await documentRepository.GetAll();
            var doc = documents.FirstOrDefault();
            var guid = doc.Id;
            documentRepository.ChangeTrackerClear();

            // Act
            await documentRepository.Delete(guid);
            documents = await documentRepository.GetAll();

            // Assert
            Assert.NotNull(documents);
            Assert.Equal(0, documents.Count);
        }
    }
}
